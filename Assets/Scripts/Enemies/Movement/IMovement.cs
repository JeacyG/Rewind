public interface IMovement
{
    void Init(IRandom rng);
    void Tick(Enemy enemy, float deltaTime);
}
