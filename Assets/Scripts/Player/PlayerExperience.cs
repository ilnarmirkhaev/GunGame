using System;
using Core;
using UnityEngine;
using UnityEngine.Timeline;

namespace Player
{
    public class PlayerExperience : MonoBehaviour
    {
        [SerializeField] private float xpGain = 10f;

        private int _level = 1;
        private float _xpForLevelUp = 100f;
        private float _xp;

        public static event Action OnLevelUp;

        private float XP
        {
            get => _xp;
            set
            {
                _xp = value;

                if (_xp < _xpForLevelUp) return;
                _xp = 0;
                _level++;
                _xpForLevelUp = _level * 100;
                Debug.Log($"New Level: {_level}");
                OnLevelUp?.Invoke();
            }
        }

        private void GainXp()
        {
            XP += xpGain;
        }
        
        private void OnEnable()
        {
            Experience.OnXpGained += GainXp;
        }

        private void OnDisable()
        {
            Experience.OnXpGained -= GainXp;
        }
    }
}