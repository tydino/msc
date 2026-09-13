using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotel_widget : MonoBehaviour
{//LEVELING UP THE HOTEL HAS NOT BEEN IMPLEMENTED
    public int bedsTaken;
    public int maxBeds;

    public int levelOfHotel;

    public void UpgradeHotel()
    {
        levelOfHotel++;
        setMaxBeds();
    }

    public void setMaxBeds()
    {
        if(levelOfHotel >= 7)
        {
            levelOfHotel = 6;
        }
        //Levels:bed amount; 0:4 1:8 2:16 3:32 4;64 5;128 6:256
        if (levelOfHotel == 0)
        {
            maxBeds = 4;
        }
        if (levelOfHotel == 1)
        {
            maxBeds = 8;
        }
        if (levelOfHotel == 2)
        {
            maxBeds = 16;
        }
        if (levelOfHotel == 3)
        {
            maxBeds = 32;
        }
        if (levelOfHotel == 4)
        {
            maxBeds = 64;
        }
        if (levelOfHotel == 5)
        {
            maxBeds = 128;
        }
        if (levelOfHotel == 6)
        {
            maxBeds = 256;
        }
    }
}
