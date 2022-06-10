using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject story;

        private bool _isStarted;

        private void Update()
        {
            if (_isStarted)
            {
                if (Input.GetMouseButtonDown(0))
                    SceneManager.LoadScene(1);
                
                if (Input.GetMouseButtonDown(1))
                    SceneManager.LoadScene(0);
            }
        }

        public void ShowStory()
        {
            mainMenu.SetActive(false);
            story.SetActive(true);
            _isStarted = true;
        }

        public void QuitGame()
        {
            Debug.Log("QUIT");
            Application.Quit();
        }
    }
}
