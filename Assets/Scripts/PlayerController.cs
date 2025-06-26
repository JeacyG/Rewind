using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public struct ActionData
{
    public Vector2 moveAction;
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] private BodyController bodyController;
    
    private GameManager gameManager;
    
    private InputAction moveAction;
    private InputAction rewindAction;

    private List<ActionData> actions = new List<ActionData>();
    
    private void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        
        moveAction = InputSystem.actions.FindAction("Move");
        rewindAction = InputSystem.actions.FindAction("Rewind");

        rewindAction.performed += RewindButton;

        ResetPlayer();
    }

    private void FixedUpdate()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        bodyController.ChangeVelocity(moveInput);
        
        ActionData data = new ActionData();
        
        // Add actions here
        data.moveAction = moveAction.ReadValue<Vector2>();
            
        actions.Add(data);
    }
    
    private void RewindButton(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        
        gameManager.Rewind(actions);
        ResetPlayer();
    }

    private void ResetPlayer()
    {
        ResetActions();
        transform.position = gameManager.GetSpawnPoint().position;
        transform.rotation = gameManager.GetSpawnPoint().rotation;
    }

    public void ResetActions()
    {
        actions.Clear();
    }
}
