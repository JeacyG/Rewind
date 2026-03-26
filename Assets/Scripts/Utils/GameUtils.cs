using UnityEngine;

public static class GameUtils
{
    public const string TAG_PLAYER = "Player";
    public const string TAG_CLONE = "Clone";
    public const string TAG_ENEMY = "Enemy";
    public const string TAG_TARGET = "Target";

    public static Quaternion GetRotationFromDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        return Quaternion.Euler(0, 0, angle);
    }
}
