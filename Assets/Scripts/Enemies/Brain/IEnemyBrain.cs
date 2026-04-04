public interface IEnemyBrain
{
    void Init(IRandom rng);
    void Tick(Enemy enemy, float deltaTime);
}
