using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public float health;
    [SerializeField] float speed = 2.5f;
    [SerializeField] GameObject reward;
    
    bool canFall;
    Vector3 directionFall;
    Quaternion toRotation;
    Quaternion myRotationBeforeFall;

    private AudioManager audioManager;

    private void Start() 
    {
        canFall = false;   
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update() 
    {
        if(canFall)
        {
            SmoothFall();
            AudioManager.instance.Play("TreeFalling");
        }    
    }

    public void HitTree(float damage)
    {
        health -= damage;

        if(health <= 0.0f)
        {
            GetComponent<Animator>().SetBool("TreeDead", true); 
            GameObject player = GameObject.Find("Player");
            directionFall = player.transform.forward;
            //Restamos para quitar offset inicial
            toRotation = Quaternion.FromToRotation(transform.up, directionFall) * transform.rotation;
            GetComponent<MeshCollider>().enabled = false;
            canFall = true;
            myRotationBeforeFall = transform.rotation;

            try
            {
                FindObjectOfType<TutorialManager>().treeFallen = true;
            }
            catch{}
        }
    }

    private void SmoothFall()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, speed * Time.deltaTime);
        //Simulacion de gravedad
        speed = Mathf.Pow(speed, 1.01f);
        
        
        if(Vector3.Angle( transform.up, directionFall) < 1f)
        {
            canFall = false;
            GameObject actualReward = Instantiate(reward, transform.position, Quaternion.identity);
            actualReward.transform.localScale = transform.localScale;
            actualReward.transform.rotation = myRotationBeforeFall;
            DisappearTree();
        }
    }
    

    private void DisappearTree()
    {
        Destroy(gameObject);
    }

}
