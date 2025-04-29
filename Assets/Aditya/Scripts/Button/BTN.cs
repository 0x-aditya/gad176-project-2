using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Aditya.Scripts.Button
{
    public class BTN : MonoBehaviour
    {
        public void StartGame()
        //function for starting game button
        {
            SceneManager.LoadScene("Aditya/Scenes/Start");
        }
    }
}
