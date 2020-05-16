using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    [SerializeField] float range;
    [SerializeField] Transform partToRotate;
    [SerializeField] GameObject bullet;
    [SerializeField] AudioClip shoot;
    [SerializeField] float fireRate = 1;
    [SerializeField] float speed = 30;
    [SerializeField] Transform gun;
    [SerializeField] ParticleSystem gunSmoke;
    [SerializeField] ParticleSystem gunSparks;
    [SerializeField] AudioClip turretSound;

    private Transform target;
    private float nextFire = 0.0F;
    private AudioSource audioSource;
    
    private void Start() 
    {
        InvokeRepeating("UpdateTarget", 2f, 0.3f);  
        audioSource = GetComponent<AudioSource>();  
    }

    private void Update() 
    {
        if(target == null)
            return;

        // Vector3 dir = target.position - partToRotate.position;
        // partToRotate.rotation = Quaternion.LookRotation(dir);

        Vector3 dir = target.position - partToRotate.position;
        //Lo que falta al Angulo al que debo ver
        Quaternion toRotation = Quaternion.FromToRotation(gun.transform.forward, dir);
        toRotation = new Quaternion(0f, toRotation.y, 0f, toRotation.w);
        partToRotate.rotation = Quaternion.RotateTowards(partToRotate.rotation, toRotation * partToRotate.rotation, 200f * Time.deltaTime);
        
        if(Time.time > nextFire)
        {
            Shoot(dir);
        }

    }

    private void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            // if(distanceToEnemy < shortestDistance)
            if(distanceToEnemy < shortestDistance && !enemy.GetComponent<EnemyController>().iAmDead)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform.Find("Target").transform;
        }
        else
        {
            target = null;
        }
    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;    
        Gizmos.DrawWireSphere(transform.position, range);
    }


    private void Shoot(Vector3 direction)
    {
        nextFire = Time.time + fireRate;
        gunSmoke.Play();
        gunSparks.Play();
        audioSource.PlayOneShot(turretSound);
        GameObject projectile = Instantiate(bullet, gun.position, Quaternion.identity);
        projectile.transform.rotation = Quaternion.FromToRotation(projectile.transform.up, direction);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = Vector3.Normalize(direction) * speed;
        // if(shoot != null) AudioSource.PlayClipAtPoint(shoot, projectile.transform.position, 1.0F);
        Destroy(projectile, 5f);
    }
    
}
