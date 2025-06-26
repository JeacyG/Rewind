using System.Collections.Generic;
using UnityEngine;

public class CloneController : MonoBehaviour
{
    [SerializeField] private BodyController bodyController;
    
    private List<ActionData> cloneActionData;
    private Transform spawnTransform;
    
    private int currentFrame = -1;

    public void Initialize(List<ActionData> cloneData, Transform spawn)
    {
        this.cloneActionData = new List<ActionData>(cloneData);
        this.spawnTransform = spawn;
    }

    public void ActivateClone(bool activate)
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
        }
        else if (currentFrame == cloneActionData.Count)
        {
            ActivateClone(false);
        }
    }
}
