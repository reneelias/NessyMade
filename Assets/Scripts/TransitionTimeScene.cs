using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class TransitionTimeScene : MonoBehaviour
    {
        public float TimeOnScreen;
        private float timer = 0f;
        public string sceneName = "GameOver";
        void Start()
        {
            
        }

        void Update()
        {
            timer += Time.deltaTime;
            if (timer >= TimeOnScreen)
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}