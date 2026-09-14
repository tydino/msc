using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hotel_UI : MonoBehaviour
{
    public int level;
    public Text levelOfHotel;
    public int abeds;
    public Text amountOfBeds;
    public int tbeds;
    public Text takenBeds;

    public int amountNeededToUpgrade;
    public Text upgrade;

    public void reload()
    {
        levelOfHotel.text = level.ToString();
        amountOfBeds.text = abeds.ToString();
        takenBeds.text = tbeds.ToString();
        if (amountNeededToUpgrade != -3)
        {
            upgrade.text = amountNeededToUpgrade.ToString();
        }
        else
        {
            upgrade.text = "Max Level hit";
        }
    }

    public void up()
    {
        Hotel_universal.current.UpgradeHotel();
    }
}
