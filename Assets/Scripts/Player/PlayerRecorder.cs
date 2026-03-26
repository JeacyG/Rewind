using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInputSource))]
public class PlayerRecorder : MonoBehaviour
{
    [SerializeField] private PlayerInputSource inputSource;

    private List<FrameSnapshot> frames = new();

    private void Awake()
    {
        if (inputSource == null)
            inputSource = GetComponent<PlayerInputSource>();
    }

    private void FixedUpdate()
    {
        frames.Add(Capture());
    }

    private FrameSnapshot Capture()
    {
        return new FrameSnapshot(transform.position, inputSource.GetInput());
    }

    public List<FrameSnapshot> ConsumeFrames()
    {
        List<FrameSnapshot> copy = frames;
        frames = new List<FrameSnapshot>();
        return copy;
    }
}
