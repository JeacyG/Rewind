using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonActions : MonoBehaviour
{
    [SerializeField] private UIPanelManager panelManager;
    
    public void PlayGame()
    {
        SceneManager.LoadScene(GameUtils.SCENE_GAME);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(GameUtils.SCENE_MAIN_MENU);
    }
    
    public void OpenOptionsPanel()
    {
        panelManager.ShowOptions();
    }

    public void OpenMainMenuPanel()
    {
        panelManager.ShowMain();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
