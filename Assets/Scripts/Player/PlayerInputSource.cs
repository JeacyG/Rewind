using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerController))]
public class PlayerInputSource : MonoBehaviour
{
    [SerializeField] private PlayerController controller;

    private PlayerInput currentInput;

    private InputAction moveAction;
    private InputAction aimAction;
    private InputAction shootAction;

    private void Awake()
    {
        if (controller == null)
            controller = GetComponent<PlayerController>();

        moveAction = InputSystem.actions.FindAction("Move");
        aimAction = InputSystem.actions.FindAction("Aim");
        shootAction = InputSystem.actions.FindAction("Shoot");
    }

    public PlayerInput GetInput()
    {
        return currentInput;
    }

    private void FixedUpdate()
    {
        Vector2 move = moveAction.ReadValue<Vector2>();
        Vector2 aim = aimAction.ReadValue<Vector2>();
        bool shoot = shootAction.ReadValue<bool>();

        currentInput = new PlayerInput(move, aim, shoot);

        controller.Simulate(currentInput);
    }
}
