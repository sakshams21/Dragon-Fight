using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingMenu : MonoBehaviour
{
    public void PlayMethod()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
