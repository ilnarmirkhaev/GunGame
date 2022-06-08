using System.Collections.Generic;
using Player;
using UnityEngine;

namespace PowerUps
{
    public class PowerUpManager : MonoBehaviour
    {
        [SerializeField] private GameObject powerUpPanel;
        
        [SerializeField] private List<PowerUp> powerUpPool;
        private Dictionary<PowerUp, int> activePowerUps;

        private void Awake()
        {
            activePowerUps = new Dictionary<PowerUp, int>();
        }

        private void OnEnable()
        {
            PlayerExperience.OnLevelUp += GivePowerUps;
        }
        
        private void OnDisable()
        {
            PlayerExperience.OnLevelUp -= GivePowerUps;
        }

        private void GivePowerUps()
        {
            powerUpPanel.SetActive(true);
        }
    }
}