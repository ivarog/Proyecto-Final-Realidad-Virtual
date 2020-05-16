using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{
    [SerializeField] Button continueButton;
    [SerializeField] GameObject panelNewGame;
    [SerializeField] Button[] buttonArray;

    AudioSource audioSource;
    private int selectedButton = 0;
    private bool buttonInUse = false;

    private void Start() 
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();    
        Cursor.visible = true;
        PlayerState.LoadPlayer();
        
        if(!PlayerState.introPlayed)
        {
            continueButton.interactable = false;
        }

        buttonArray[0].transform.GetChild(0).gameObject.SetActive(true);

    }
    
    private void Update() 
    {
        if (Input.GetAxisRaw("Vertical") > 0f && !buttonInUse)
        {
            buttonInUse = true;
            selectedButton--;
        }
        if (Input.GetAxisRaw("Vertical") < 0f && !buttonInUse)
        {
            buttonInUse = true;
            selectedButton++;
        }
        if (Input.GetAxisRaw("Vertical") == 0f) buttonInUse = false;

        Mathf.Clamp(selectedButton, 0, 3);
        buttonArray[selectedButton].transform.GetChild(0).gameObject.SetActive(true);   
        
        for(int i=0; i<=3; i++)
        {
            if(i != selectedButton)
            {
                buttonArray[i].transform.GetChild(0).gameObject.SetActive(false);   
            }
        } 

        if((Input.GetKeyUp(KeyCode.Joystick1Button0) || Input.GetKeyUp(KeyCode.Return)) && buttonArray[selectedButton].interactable)
        {
            buttonArray[selectedButton].onClick.Invoke();
        }
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void NewGame()
    {

        SceneManager.LoadScene("Story");

    }

    public void NewGameConfirm()
    {
        PlayerState.ResetPlayer();
        SceneManager.LoadScene("Story");

    }

    public void ContinueGame()
    {        
        SceneManager.LoadScene("Level" + PlayerState.actualLevel);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
