using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// TODO: See timer value on screen
public class InstructionsScreen : MonoBehaviour
{
    public float TimeOnScreen;
    public TextMeshProUGUI MinutesUi;
    private float timer = 0f;
    void Start()
    {
        MinutesUi.text = GlobalHolder.GameLengthMinutes.ToString();
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