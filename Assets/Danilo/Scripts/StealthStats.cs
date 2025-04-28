using UnityEngine;

namespace StealthGame.Stealth
{
    [CreateAssetMenu(fileName = "StealthStats", menuName = "Stealth/StealthStats")]
    public class StealthStats : ScriptableObject
    {
        // sets the noise level
        public float baseNoiseLevel = 10f;
    }
}
