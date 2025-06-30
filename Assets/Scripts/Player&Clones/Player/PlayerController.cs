using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public struct HitData
{
    public HitData(int enemyID, int damage)
    {
        this.enemyID = enemyID;
        this.damage = damage;
    }
    
    public int enemyID;
    public int damage;
}

public struct ActionData
{
    public Vector2 moveAction;
    public HitData[] hitData;
}

public class PlayerController : MonoBehaviour
{
    [Header("Movement Subsystem")]
    [SerializeField] private BodyController bodyController;
    [Header("DamageZone Subsystem")]
    [SerializeField] private DamageZone damageZone;
    [SerializeField] private float hitCooldown;
    private bool bCanAttack = true;
    private Coroutine hitCooldownCoroutine;
    
    private GameManager gameManager;
    
    private InputAction moveAction;
    private InputAction rewindAction;
    private InputAction hitAction;

    private List<ActionData> actions = new List<ActionData>();
    private List<HitData> currentHitData = new List<HitData>();
    
    private void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        
        moveAction = InputSystem.actions.FindAction("Move");
        rewindAction = InputSystem.actions.FindAction("Rewind");
        hitAction = InputSystem.actions.FindAction("Hit");

        rewindAction.performed += RewindButton;
        hitAction.performed += HitButton;

        damageZone.OnEnemiesHit += OnEnemiesHitReveived;

        ResetPlayer();
    }

    private void OnDestroy()
    {
        rewindAction.performed -= RewindButton;
        hitAction.performed -= HitButton;
        
        damageZone.OnEnemiesHit -= OnEnemiesHitReveived;
    }

    private void FixedUpdate()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        bodyController.ChangeVelocity(moveInput);
        
        ActionData data = new ActionData();
        
        // Add actions here
        data.moveAction = moveAction.ReadValue<Vector2>();

        if (currentHitData.Count > 0)
        {
            data.hitData = currentHitData.ToArray();
            currentHitData.Clear();
        }
            
        actions.Add(data);
    }
    
    private void ResetPlayer()
    {
        transform.position = gameManager.GetSpawnPoint().position;
        transform.rotation = gameManager.GetSpawnPoint().rotation;
        
        StopAllCoroutines();
        hitCooldownCoroutine = null;
        bCanAttack = true;
        actions.Clear();
        currentHitData.Clear();
        damageZone.ResetDamageZone();
    }
    
    private void RewindButton(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        
        gameManager.Rewind(actions);
        ResetPlayer();
    }
    
    private void HitButton(InputAction.CallbackContext context)
    {
        if (!context.performed || !bCanAttack)
            return;

        StartAttackCooldown();

        int damage = 1;
        damageZone.Damage(damage);
    }
    
    private void OnEnemiesHitReveived(List<HitData> hitDatas)
    {
        currentHitData = hitDatas;
    }

    private void StartAttackCooldown()
    {
        bCanAttack = false;

        if (hitCooldownCoroutine != null)
        {
            StopCoroutine(hitCooldownCoroutine);
        }

        hitCooldownCoroutine = StartCoroutine(HitCooldownCoroutine());
    }

    private IEnumerator HitCooldownCoroutine()
    {
        yield return new WaitForSeconds(hitCooldown);
        bCanAttack = true;
        hitCooldownCoroutine = null;
    }
}
