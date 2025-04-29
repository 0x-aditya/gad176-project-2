using UnityEngine;

namespace StealthGame.Stealth
{
    public interface IDetectable
    {
        // returns how loud the player is currently
        float GetNoiseLevel();

        // any detectable object must provide a reference to its own transform
        Transform GetTransform();
    }
}