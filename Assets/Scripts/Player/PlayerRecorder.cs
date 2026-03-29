using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInputSource))]
public class PlayerRecorder : MonoBehaviour
{
    [SerializeField] private PlayerInputSource inputSource;
    [SerializeField] private GameObject clonePrefab;

    private List<GhostPlayer> clones = new();
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

    public void TriggerRewind()
    {
        ResetSelf();
        
        foreach (GhostPlayer clone in clones)
        {
            clone.ResetClone();
        }
        
        SpawnClone();
        frames = new List<FrameSnapshot>();
    }

    private void ResetSelf()
    {
        transform.position = Vector2.zero;
    }

    private void SpawnClone()
    {
        GhostPlayer clone = Instantiate(clonePrefab).GetComponent<GhostPlayer>();
        clone.Initialize(frames);
        clones.Add(clone);
    }
}
