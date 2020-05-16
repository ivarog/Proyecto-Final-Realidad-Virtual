using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : MonoBehaviour
{
    [SerializeField] ParticleSystem treeParticles;
    [SerializeField] float damage;
    [SerializeField] AudioClip axeSound;


    private Animator animator;
    bool isCutting;
    private AudioSource audioSource;

    private void Start() 
    {
        animator = GetComponent<Animator>();   
        audioSource = GetComponent<AudioSource>(); 
        damage = PlayerState.axeDamage;
    }

    void Update()
    {
        InputController();
    }

    void InputController()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            isCutting = true;
            animator.SetInteger("AxeState", 1);
        }    
        else if(Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.JoystickButton7))
        {
            isCutting = false;
            animator.SetInteger("AxeState", 0);
        }
    }

    private void OnCollisionEnter(Collision other) 
    {

        if(other.gameObject.tag == "Tree" && isCutting)  
        {
            ParticleSystem actualParticle = Instantiate(treeParticles, other.contacts[0].point, Quaternion.identity, transform);
            Destroy(actualParticle, 3f);
            other.gameObject.GetComponent<Tree>().HitTree(damage);
            audioSource.PlayOneShot(axeSound);
        }  
    }
}
