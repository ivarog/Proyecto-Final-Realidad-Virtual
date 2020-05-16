using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
    [Range(0f, 3f)][SerializeField] float intensity;
    [SerializeField] GameObject lightBonfire;
    [SerializeField] float timeBonfireLife;
    [SerializeField] ParticleSystem fire;
    [SerializeField] ParticleSystem smoke;
    [SerializeField] float logForce;
    [SerializeField] ParticleSystem smokeExplosion;
    [SerializeField] AudioClip bonfireSound;
    [SerializeField] AudioClip rekindleSound;
    [SerializeField] AudioClip woodFalling;
    [SerializeField] public bool canIReduce = true;

    private float speed; 
    private Light lightBonfireComponent;
    private bool canReduceLigth;
    private const float maxIntensity = 3;
    private float startSizeFire;
    private float startSizeSmoke;
    private GameObject player;
    private WeaponSelector weaponSelector;
    private AudioSource audioSource;
    public bool fireReinforced = false;

    private void Start() 
    {
        lightBonfireComponent = lightBonfire.GetComponent<Light>();  
        startSizeFire = fire.main.startSize.constant;
        startSizeSmoke = smoke.main.startSize.constant;
        speed = intensity / timeBonfireLife;
        player = GameObject.Find("Player"); 
        weaponSelector = FindObjectOfType<WeaponSelector>();
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(bonfireSound);
    }

    private void Update() 
    {
        if(canIReduce) ReduceIntensityBonfire(speed);
        RekindleFlame();
        FlameGameOver();
    }

    private void ReduceIntensityBonfire(float speed)
    {
        intensity -= speed * Time.deltaTime;
        ChangeBonfireIntensity(intensity);
    }

    private void ChangeBonfireIntensity(float intensity)
    {
        lightBonfire.transform.position = Vector3.up * intensity;
        lightBonfireComponent.intensity = intensity;
        ParticleSystem.MainModule main = fire.main;
        main.startSize = new ParticleSystem.MinMaxCurve(startSizeFire * intensity / maxIntensity); 
        main = smoke.main;
        main.startSize = new ParticleSystem.MinMaxCurve(startSizeSmoke * intensity / maxIntensity); 
        audioSource.volume = intensity / maxIntensity;
    }

    private void RekindleFlame()
    {
        if((Vector3.Distance(transform.position, player.transform.position)) < 2f && weaponSelector.carryingTrunk)
        {
            fireReinforced = true;
            audioSource.PlayOneShot(rekindleSound);
            audioSource.PlayOneShot(woodFalling);
            intensity += logForce;
            ChangeBonfireIntensity(intensity);
            weaponSelector.DesactivateLogItem();
            smokeExplosion.Play();
        }
    }

    public void ReduceFlame(float mount)
    {
        intensity -= mount;
        ChangeBonfireIntensity(intensity);
    }

    public void FlameGameOver()
    {
        if(intensity <= 0)
        {
            FindObjectOfType<LeveManager>().GameOver();
        }
    }

}
