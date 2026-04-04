public interface IAttack
{
    void Init(IRandom rng);
    void Tick(Enemy enemy, float deltaTime);
}
