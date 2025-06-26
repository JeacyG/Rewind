using System.Collections.Generic;
using UnityEngine;

public class CloneManager : MonoBehaviour
{
    [SerializeField] private GameObject clonePrefab;
    
    private List<CloneController> clones = new List<CloneController>();

    public void CreateNewClone(List<ActionData> actions, Transform spawn)
    {
        CloneController clone = Instantiate(clonePrefab, this.transform).GetComponent<CloneController>();
        clone.Initialize(actions, spawn);
        
        clones.Add(clone);
    }

    public void ResetAllClones()
    {
        foreach (CloneController clone in clones)
        {
            clone.ResetClone();
        }
    }

    public void DestroyAllClones()
    {
        foreach (CloneController clone in clones)
        {
            Destroy(clone.gameObject);
        }
        clones.Clear();
    }
}
