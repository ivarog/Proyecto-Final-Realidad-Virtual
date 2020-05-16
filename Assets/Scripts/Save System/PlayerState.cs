using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerState
{
    public static int actualLevel;
    public static bool introPlayed;
    public static bool tutorialPlayed;
    public static float axeDamage;
    public static float gunDamage;
    public static int numberUpgrades;
    public static int turrets;

    public static void SavePlayer()
    {
        SaveSystem.SavePlayer();
    }

    public static void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        if(data == null)
        {
            actualLevel = 1;
            introPlayed = false;
            tutorialPlayed = false;
            axeDamage = 25f;
            gunDamage = 25f;
            numberUpgrades = 0;
            turrets = 0;
            SavePlayer();
        }
        else
        {
            actualLevel = data.actualLevel;
            introPlayed = data.introPlayed;
            tutorialPlayed = data.tutorialPlayed;
            axeDamage = data.axeDamage;
            gunDamage = data.gunDamage;
            numberUpgrades = data.numberUpgrades;
            turrets = data.turrets;
        }
    }

    public static void ResetPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        actualLevel = 1;
        introPlayed = false;
        axeDamage = 25f;
        gunDamage = 25f;
        numberUpgrades = 0;
        turrets = 0;
        SavePlayer();
    }

}
