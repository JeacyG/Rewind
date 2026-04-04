using UnityEngine;

public class RewindManager : MonoBehaviour
{ 
    public void PrepareRewind(int currentRewind)
    {
        foreach (PlayerRecorder recorder in FindObjectsByType<PlayerRecorder>())
        {
            recorder.TriggerRewind();
        }
    }
}
