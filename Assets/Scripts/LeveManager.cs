using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeveManager : MonoBehaviour
{
    [SerializeField] float gameTime;
    [SerializeField] Animator animator;
    [SerializeField] GameObject countdown;

    private AudioManager audioManager;
    private Text textCountdown;
    bool canPlayClock = false;

    private void Start() 
    {
        AudioManager.instance.Play("Ambient");
        textCountdown = countdown.GetComponent<Text>();
        animator.Play("Clock");
        Cursor.visible = false;
    }

    private void Update() 
    {
        Counter();    
        Exit();
    }

    private void Counter()
    {
        gameTime -= Time.deltaTime;
        if(gameTime < 0) gameTime = 0;
        int minutes = Mathf.FloorToInt(gameTime / 60F);
        int seconds = Mathf.FloorToInt(gameTime - minutes * 60);
        textCountdown.text = minutes + ":" + seconds;


        if(gameTime <= 0)
        {
            animator.Play("Win");
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject enemy in enemies)
            {
                enemy.GetComponent<EnemyController>().DamageEnemy(1000f);
            }
            Spawner[] spawners = FindObjectsOfType<Spawner>();
            foreach(Spawner s in spawners)
            {
                s.spawnActive = false;
            }
            StartCoroutine(NextLevel());
        }
    }

    public void GameOver()
    {
        animator.Play("Fail");
        StartCoroutine(ReturnToMenu());
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(5f);
        if(SceneManager.GetActiveScene().name == "Level1")
        {
            SceneManager.LoadScene("Upgrade1");
        }
        else if(SceneManager.GetActiveScene().name == "Level2")
        {
            SceneManager.LoadScene("Upgrade2");
        }
        else if(SceneManager.GetActiveScene().name == "Level3")
        {
            SceneManager.LoadScene("Menu");
        }
    }

    IEnumerator ReturnToMenu()
    {
        yield return new WaitForSeconds(5f);
        AudioManager.instance.Stop("Ambient");    
        SceneManager.LoadScene("Menu");
    }

    private void Exit()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");   
           FindObjectOfType<AudioManager>().Stop("Ambient");
        }
    }

    
}
