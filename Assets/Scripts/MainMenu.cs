using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
    {
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
