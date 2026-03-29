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
        shootAction = InputSystem.actions.FindAction("Hit");
    }

    public PlayerInput GetInput()
    {
        return currentInput;
    }

    private void FixedUpdate()
    {
        Vector2 move = moveAction.ReadValue<Vector2>();
        Vector2 aimRaw = aimAction.ReadValue<Vector2>();
        bool shoot = shootAction.IsPressed();
        
        //Debug.Log(aimRaw);

        Vector2 aim = transform.eulerAngles;
        if (aimAction.activeControl is not null)
        {
            if (aimAction.activeControl.device is Mouse)
            {
                Vector3 mouseScreen = new Vector3(aimRaw.x, aimRaw.y, 0f);
                Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mouseScreen);

                Vector3 dir = mouseWorld - transform.position;
                aim = new Vector2(dir.x, dir.y).normalized;
            }
            else
            {
                aim = aimRaw.normalized;
            }
        }

        currentInput = new PlayerInput(move, aim, shoot);

        controller.Simulate(currentInput);
    }
}
