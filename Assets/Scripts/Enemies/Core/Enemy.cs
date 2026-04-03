using UnityEngine;

public class Enemy : MonoBehaviour
{
    public IMovement Movement { get; private set; }
    public IAttack Attack { get; private set; }

    private IEnemyBrain brain;

    public void Init(IMovement movement, IAttack attack, IEnemyBrain brain)
    {
        Movement = movement;
        Attack = attack;
        this.brain = brain;
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
