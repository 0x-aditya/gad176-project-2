using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthGame.Mission
{
    public class ExitZone : MonoBehaviour
    {
        [SerializeField] private string playerTag = "Player";

        private void OnTriggerEnter(Collider other)
        {
            if (other == null)
            {
                // if anything other than the player tag has entered the trigger display debug log warning
                Debug.LogWarning("no player has exited");
                return;
            }

            if (other.CompareTag(playerTag))
            {
                // if the player tag has entered the trigger, the player has won the game
                Debug.Log("the playe has exited");
                WinGame();
            }
        }

        private void WinGame()
        {
            // freeze time and display debug log
            Debug.Log("you actually won, nice job");
            Time.timeScale = 0f;
        }
    }
}
