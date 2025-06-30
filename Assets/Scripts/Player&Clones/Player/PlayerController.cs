using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] private BodyController bodyController;
    [SerializeField] private DamageZone damageZone;
    
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

        ResetPlayer();
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
    
    private void RewindButton(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        
        gameManager.Rewind(actions);
        ResetPlayer();
    }
    
    private void HitButton(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        int damage = 1;

        damageZone.Damage(damage, out List<HitData> hitData);
        
        currentHitData = hitData;
    }

    private void ResetPlayer()
    {
        ResetActions();
        transform.position = gameManager.GetSpawnPoint().position;
        transform.rotation = gameManager.GetSpawnPoint().rotation;
        damageZone.ResetDamageZone();
    }

    public void ResetActions()
    {
        actions.Clear();
    }
}
