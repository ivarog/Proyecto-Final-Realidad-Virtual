using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject enemy;

    int state = 0;

    private PlayerController playerController;
    private MouseLook mouseLook;
    private WeaponSelector weaponSelector;
    private Bonfire bonfire;
    public bool treeFallen = false;
    public bool enemyShoten = false;

    private void Start() 
    {
        playerController = FindObjectOfType<PlayerController>();
        mouseLook = FindObjectOfType<MouseLook>();
        weaponSelector = FindObjectOfType<WeaponSelector>();
        bonfire = FindObjectOfType<Bonfire>();
        FindObjectOfType<AudioManager>().Play("Ambient");
        Cursor.visible = false;
    }
    

    private void Update() 
    {
        switch(state)
        {
            case 0:
                TutMove();
                break;
            case 1:
                TutLook();
                break;
            case 2:
                TutHitAxe();
                break;
            case 3:
                TutCarryLog();
                break;
            case 4:
                TutBringLog();
                break;
            case 5:
                TutSwitchWeapon();
                break;
            case 6:
                TutKillEnemy();
                break;
        }

        
    }

    void TutMove()
    {
        if(playerController.iAmMoving)
        {
            state = 1;
            animator.SetInteger("State", 1);
        }
    }

    void TutLook()
    {
        if(true)
        {
            state = 2;
            animator.SetInteger("State", 2);
        }
    }

    void TutHitAxe()
    {
        if(treeFallen)
        {
            state = 3;
            animator.SetInteger("State", 3);
        }
    }

    void TutCarryLog()
    {
        if(weaponSelector.carryingTrunk)
        {
            state = 4;
            animator.SetInteger("State", 4);
        }
    }

    void TutBringLog()
    {
        if(bonfire.fireReinforced)
        {
            state = 5;
            animator.SetInteger("State", 5);
        }
    }

    void TutSwitchWeapon()
    {
        if(weaponSelector.weaponSwitched)
        {
            state = 6;
            animator.SetInteger("State", 6);
            Instantiate(enemy, new Vector3(20f, 0f, 0.3f), Quaternion.identity);
        }
    }

    void TutKillEnemy()
    {
        if(enemyShoten)
        {
            state = 7;
            animator.SetInteger("State", 7);
        }
    }
}
