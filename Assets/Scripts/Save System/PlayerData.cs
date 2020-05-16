using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public bool introPlayed;
    public bool tutorialPlayed;
    public int actualLevel;
    public float axeDamage;
    public float gunDamage;
    public int numberUpgrades;
    public int turrets;

    public PlayerData()
    {
        introPlayed = PlayerState.introPlayed;
        tutorialPlayed = PlayerState.tutorialPlayed;
        actualLevel = PlayerState.actualLevel;
        axeDamage = PlayerState.axeDamage;
        gunDamage = PlayerState.gunDamage;
        numberUpgrades = PlayerState.numberUpgrades;
        turrets = PlayerState.turrets;
    }

}
