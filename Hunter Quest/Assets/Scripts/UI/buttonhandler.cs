using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonhandler : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void GoMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
