using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpgradeSystem1 : MonoBehaviour
{
    [SerializeField] Animator canvasAnimator;

    private void Start() {
        Cursor.visible = true;
    }

    private void Update() {
        if(Input.GetKeyUp(KeyCode.Joystick1Button2))
        {
            TurretUpdgrade();
        }
        else if(Input.GetKeyUp(KeyCode.Joystick1Button3))
        {
            GunUpdgrade();
        }
        else if(Input.GetKeyUp(KeyCode.Joystick1Button1))
        {
            AxeUpdgrade();
        }
    }

    public void GunUpdgrade()
    {
        PlayerState.gunDamage = 50f;
        DesactivateButtons();
        StartCoroutine(LoadNextScene());
    }

    public void AxeUpdgrade()
    {
        PlayerState.axeDamage = 150f;
        DesactivateButtons();
        StartCoroutine(LoadNextScene());
    }

    public void TurretUpdgrade()
    {
        PlayerState.turrets = PlayerState.turrets + 1;
        DesactivateButtons();
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        canvasAnimator.Play("UpgradeOut");
        yield return new WaitForSeconds(1f);
        PlayerState.actualLevel = 2;
        PlayerState.SavePlayer();
        SceneManager.LoadScene("Level2");
    }

    void DesactivateButtons()
    {
        Button[] allButtons = FindObjectsOfType<Button>();

        foreach(Button b in allButtons)
        {
            b.interactable = false;
        }
    }
}
