using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextFromTutorial : MonoBehaviour
{

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");   
           FindObjectOfType<AudioManager>().Stop("Ambient");
        }
    }

    private void TutorialEnded()
    {
       SceneManager.LoadScene("Menu");   
       FindObjectOfType<AudioManager>().Stop("Ambient");
    }
}
