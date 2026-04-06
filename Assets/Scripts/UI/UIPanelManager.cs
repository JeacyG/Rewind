using UnityEngine;

public class UIPanelManager : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject optionsPanel;
    
    public void ShowOptions()
    {
        HideAll();
        optionsPanel.SetActive(true);
    }

    public void ShowMain()
    {
        HideAll();
        mainPanel.SetActive(true);
    }

    private void HideAll()
    {
        mainPanel.SetActive(false);
        optionsPanel.SetActive(false);
    }
}
