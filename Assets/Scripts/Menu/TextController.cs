using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextController : MonoBehaviour
{

    [SerializeField] ParticleSystem particlesText1;
    [SerializeField] ParticleSystem particlesText2;
    [SerializeField] ParticleSystem particlesText3;
    [SerializeField] AudioClip backgroudSound;
    [SerializeField] AudioClip heartBit;

    AudioSource audioSource;

    private void Start() 
    {
        audioSource = GetComponent<AudioSource>();
        AudioManager.instance.Play("Ambient");
    }

    void PlayParticle1()
    {
        particlesText1.Play();
    }

    void PlayParticle2()
    {
        particlesText2.Play();
    }

    void PlayParticle3()
    {
        particlesText3.Play();
    }

    void PlayHeart()
    {
        audioSource.PlayOneShot(heartBit);
    }

    void PlayGame()
    {
        PlayerState.introPlayed = true;
        PlayerState.SavePlayer();
        SceneManager.LoadScene("Level1");
    }

}
