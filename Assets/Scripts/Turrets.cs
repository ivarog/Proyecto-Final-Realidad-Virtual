using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turrets : MonoBehaviour
{
    [SerializeField] GameObject turret1;
    [SerializeField] GameObject turret2;

    private void Start() 
    {
        if(PlayerState.turrets == 1)
        {
            turret1.SetActive(true);
        }        
        else if(PlayerState.turrets == 2)
        {
            turret1.SetActive(true);
            turret2.SetActive(true);
        }        
    }
}