using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenTrunk : MonoBehaviour
{
    [SerializeField] GameObject trunk;
    [SerializeField] AudioClip woodTaken;

    private GameObject player;
    private WeaponSelector weaponSelector;
    bool logPicked = false;
    private AudioSource audioSource;

    private void Start() 
    {
        player = GameObject.Find("Player"); 
        weaponSelector = FindObjectOfType<WeaponSelector>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        LogPicker();
    }

    void LogPicker()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < 2.5f)
        {
            if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton0))&& !weaponSelector.carryingTrunk && !logPicked)
            {
                audioSource.PlayOneShot(woodTaken);
                logPicked = true;
                weaponSelector.ActivateLogItem();
                trunk.SetActive(false);
            } 
            
        }
    }

}
