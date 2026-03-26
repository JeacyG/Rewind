using UnityEngine;

public struct PlayerInput
{
    public PlayerInput(Vector2 move, Vector2 aim, bool shoot)
    {
        Move = move;
        Aim = aim;
        Shoot = shoot;
    }

    public Vector2 Move;
    public Vector2 Aim;
    public bool Shoot;
}
