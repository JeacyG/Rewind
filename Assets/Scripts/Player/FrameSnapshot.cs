using UnityEngine;

public struct FrameSnapshot
{
    public FrameSnapshot(Vector2 pos, PlayerInput input)
    {
        Position = pos;
        Input = input;
    }

    public Vector2 Position;
    public PlayerInput Input;
}
