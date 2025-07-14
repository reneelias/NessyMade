using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class CreditsScreen : MonoBehaviour
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
                SceneManager.LoadScene("Menu");
            }
        }
    }
}