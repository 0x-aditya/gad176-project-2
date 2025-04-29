using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Aditya.Scripts.PlayerController
{
    
    public class Objective : MonoBehaviour
    {
        // the game will be won once you touch this objective
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                SceneManager.LoadScene("Aditya/Scenes/Start");
            }
        }

    }
}