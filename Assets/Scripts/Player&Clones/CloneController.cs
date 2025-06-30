using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CloneController : MonoBehaviour
{
    [SerializeField] private BodyController bodyController;

    private EnemySpawner enemySpawner;
    
    private List<ActionData> cloneActionData;
    private Transform spawnTransform;
    
    private int currentFrame = -1;

    public void Initialize(List<ActionData> cloneData, Transform spawn)
    {
        enemySpawner = FindFirstObjectByType<EnemySpawner>();
        
        this.cloneActionData = new List<ActionData>(cloneData);
        this.spawnTransform = spawn;
    }

    private void ActivateClone(bool activate)
    {
        currentFrame = activate ? 0 : -1;
        gameObject.SetActive(activate);
    }

    public void ResetClone()
    {
        transform.position = spawnTransform.position;
        transform.rotation = spawnTransform.rotation;
        bodyController.ResetVelocity();
        ActivateClone(true);
    }

    private void FixedUpdate()
    {
        if (currentFrame >= 0 && currentFrame < cloneActionData.Count)
        {
            ActionData currentData = cloneActionData[currentFrame];
            currentFrame++;
            
            bodyController.ChangeVelocity(currentData.moveAction);

            if (!currentData.hitData.IsUnityNull() && currentData.hitData.Length > 0)
            {
                HitAction(currentData.hitData);
            }
        }
        else if (currentFrame == cloneActionData.Count)
        {
            ActivateClone(false);
        }
    }

    private void HitAction(HitData[] hitData)
    {
        foreach (HitData data in hitData)
        {
            EnemyController enemyController = enemySpawner.GetEnemy(data.enemyID);
            if (!enemyController.IsUnityNull())
            {
                enemyController.TakeDamage(data.damage);
            }
        }
    }
}
