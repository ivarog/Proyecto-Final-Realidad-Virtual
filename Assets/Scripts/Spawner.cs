using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] float timeBetweenNextSpawn;
    
    float timeNextSpawn;
    public bool spawnActive;

    private void Start() 
    {
        timeNextSpawn = 0f;    
    }


    private void Update() 
    {
        if(spawnActive && (Time.time > timeNextSpawn))
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        timeNextSpawn = Time.time + timeBetweenNextSpawn;

        //Primer coordenada

        int firstCor = Random.Range(1, 5);
        Vector3 position = Vector3.zero;

        switch(firstCor)
        {
            case 1:
                position = new Vector3(-30f, 0f, Random.Range(-30f, 30f));
                break;
            case 2:
                position = new Vector3(Random.Range(-30f, 30f), 0f, 30f);
                break;
            case 3:
                position = new Vector3(30f, 0f, Random.Range(-30f, 30f));
                break;
            case 4:
                position = new Vector3(Random.Range(-30f, 30f), 0f, -30f);
                break;
        }

        GameObject actualEnemy = Instantiate(enemy, position, Quaternion.identity, gameObject.transform);

        //actualEnemy.gameObject.transform.localScale = Vector3.one * Random.Range(0.4f, 0.6f);

    }


}
