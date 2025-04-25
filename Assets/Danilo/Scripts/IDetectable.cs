using UnityEngine;

namespace StealthGame.Stealth
{
    public interface IDetectable
    {
        float GetNoiseLevel();
        float GetVisibilityLevel();
        Transform GetTransform();
    }
}

