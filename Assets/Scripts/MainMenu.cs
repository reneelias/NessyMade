using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    public Button playButton;
    void Start()
    {
        Screen.SetResolution(1024, 1024, true);
        if (playButton)
        {
            playButton.Select();
        }
    }

    public void MoveToGame()
    {
        SceneManager.LoadScene("InstructScreen");
    }

    public void MoveToCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
