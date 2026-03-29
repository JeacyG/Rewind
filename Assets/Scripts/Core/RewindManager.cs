using UnityEngine;
using UnityEngine.InputSystem;

public class RewindManager : MonoBehaviour
{
    private InputAction rewindAction;
    
    private void Awake()
    {
        rewindAction = InputSystem.actions.FindAction("Rewind");
        rewindAction.performed += OnRewind;
    }

    private void OnDestroy()
    {
        rewindAction.performed -= OnRewind;
    }

    private void OnRewind(InputAction.CallbackContext context)
    {
        TriggerRewindAll();
    }

    public void TriggerRewindAll()
    {
        foreach (PlayerRecorder recorder in FindObjectsByType<PlayerRecorder>())
        {
            recorder.TriggerRewind();
        }
    }
}
