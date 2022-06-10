using UnityEngine;

namespace PowerUps.ScriptableObjects
{
    public class PowerUpData : ScriptableObject
    {
        public PowerUp powerUp;
        public Texture2D icon;
        public string description;

        public void Apply()
        {
            
        }
    }
}