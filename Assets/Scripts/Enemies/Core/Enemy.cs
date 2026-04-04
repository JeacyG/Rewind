using UnityEngine;

public class Enemy : MonoBehaviour
{
    public IMovement Movement { get; private set; }
    public IAttack Attack { get; private set; }

    private IEnemyBrain brain;

    private uint seed;
    private RandomContainer random;

    public void Init(IMovement movement, IAttack attack, IEnemyBrain brain, uint seed)
    {
        this.seed = seed;
        random = new RandomContainer();
        
        Movement = movement;
        Attack = attack;
        this.brain = brain;
        
        Movement.Init(random.GetStream("movement", seed));
        Attack.Init(random.GetStream("attack", seed));
        brain.Init(random.GetStream("brain", seed));
    }

    private void FixedUpdate()
    {
        Simulate(Time.fixedDeltaTime);
    }

    public void Simulate(float deltaTime)
    {
        brain?.Tick(this, deltaTime);
    }
}
