using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;
        
        private void Update()
        {
            if (Input.GetKeyDown("escape"))
                PauseGame();
        }

        private void PauseGame()
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }

        public void Continue()
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }

        public void Exit()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
    }
}