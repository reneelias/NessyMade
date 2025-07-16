using UnityEngine;
using UnityEngine.SceneManagement;

// TODO: See timer value on screen
public class InstructionsScreen : MonoBehaviour
{
    public float TimeOnScreen;
    private float timer = 0f;
    void Start()
    {
        
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= TimeOnScreen)
        {
            SceneManager.LoadScene("Game");
        }
    }
}