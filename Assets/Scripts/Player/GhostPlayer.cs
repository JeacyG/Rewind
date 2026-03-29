using System.Collections.Generic;
using UnityEngine;

public class GhostPlayer : MonoBehaviour
{
    [SerializeField] private PlayerController controller;
    [SerializeField] private bool bDieWhenDone = true;
    
    private List<FrameSnapshot> frames = new();

    private int currentFrame = -1;

    private void Awake()
    {
        if (controller == null)
            controller = GetComponent<PlayerController>();
    }

    private void FixedUpdate()
    {
        if (currentFrame < 0)
            return;

        if (currentFrame >= frames.Count)
        {
            currentFrame = 0;

            if (bDieWhenDone)
            {
                Destroy(gameObject);
            }
            
            return;
        }
            

        FrameSnapshot snapshot = frames[currentFrame++];
        controller.Simulate(snapshot.Input);
    }

    public void Initialize(List<FrameSnapshot> record)
    {
        frames = record;
        ResetClone();
    }

    public void ResetClone()
    {
        currentFrame = 0;
        transform.position = frames[0].Position;
    }
}
