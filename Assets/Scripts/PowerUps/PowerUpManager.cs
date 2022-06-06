using System.Collections.Generic;
using Player;
using UnityEngine;

namespace PowerUps
{
    public class PowerUpManager : MonoBehaviour
    {
        [SerializeField] private List<PowerUp> powerUpPool;
        private Dictionary<PowerUp.Type, int> activePowerUps;

        private void Awake()
        {
            activePowerUps = new Dictionary<PowerUp.Type, int>();
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
            
        }
    }
}