using System.Collections.Generic;
using UnityEngine;


public class GhostPlayer : MonoBehaviour
{
    private List<FrameSnapshot> frames = new();

    private int currentFrame = -1;

    private void FixedUpdate()
    {
        if (currentFrame < 0)
            return;

        FrameSnapshot snapshot = frames[currentFrame++];


    }

    public void Initialize(List<FrameSnapshot> record)
    {
        frames = record;
        currentFrame = 0;
    }
}
