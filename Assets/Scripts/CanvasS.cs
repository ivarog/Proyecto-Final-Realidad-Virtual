using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasS : MonoBehaviour
{
    public void PlayClock()
    {
        AudioManager.instance.Play("Clock");
    }
}
