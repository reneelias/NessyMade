using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float gameLength;
    private float startTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        gameLength = GlobalHolder.GameLengthMinutes * 60f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime >= gameLength)
        {
            GameWin();
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void GameWin()
    {
        SceneManager.LoadScene("GameWin");
    }
}
