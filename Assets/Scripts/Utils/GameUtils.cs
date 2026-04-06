using UnityEngine;

public static class GameUtils
{
    public const int SCENE_MAIN_MENU = 0;
    public const int SCENE_OPTIONS = 1;
    public const int SCENE_GAME = 2;
    
    public const string TAG_PLAYER = "Player";
    public const string TAG_CLONE = "Clone";
    public const string TAG_ENEMY = "Enemy";
    public const string TAG_TARGET = "Target";

    public static Quaternion GetRotationFromDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        return Quaternion.Euler(0, 0, angle);
    }

    public static uint Hash(uint a, int b)
    {
        unchecked
        {
            return a * 31u + (uint)b;
        }
    }
}
