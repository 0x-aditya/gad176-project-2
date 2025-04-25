using UnityEngine;

namespace StealthGame.Stealth
{
    [CreateAssetMenu(fileName = "StealthStats", menuName = "Stealth/StealthStats")]
    public class StealthStats : ScriptableObject
    {
        [Header("Stealth Values")]
        public float baseVisibility = 5f;
        public float baseNoiseLevel = 10f;
    }
}
