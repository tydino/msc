using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotel_universal : MonoBehaviour
{
    public static Hotel_universal current;
    public GameObject Hotel;

    public void Awake()
    {
        current = this;
    }

    public bool AvailableSpace(int amountNeeded)
    {
        Hotel_widget hotel_w = Hotel.GetComponent<Hotel_widget>();
        int availableBeds = hotel_w.maxBeds - hotel_w.bedsTaken;
        if (availableBeds >= amountNeeded)
        {
            return true;
        }

        return false;
    }

    public void RemoveSpace(int amountRemoved)
    {
        Hotel_widget hotel_w = Hotel.GetComponent<Hotel_widget>();
        int availableBeds = hotel_w.maxBeds - hotel_w.bedsTaken;
        if (availableBeds >= amountRemoved)
        {
            hotel_w.bedsTaken = hotel_w.bedsTaken + amountRemoved;
        }
    }

    public void AddSpace(int amountAdded)
    {
        Hotel_widget hotel_w = Hotel.GetComponent<Hotel_widget>();
        hotel_w.bedsTaken = hotel_w.bedsTaken - amountAdded;
    }

    public void UpgradeHotel()
    {
        Hotel_widget hotel_w = Hotel.GetComponent<Hotel_widget>();
        if (objectHandler.current.HotelCosts.Count > hotel_w.levelOfHotel && Currency.coins >= objectHandler.current.HotelCosts[hotel_w.levelOfHotel])
        {
            Currency.coins = Currency.coins - objectHandler.current.HotelCosts[hotel_w.levelOfHotel];
            hotel_w.levelOfHotel++;
            hotel_w.setMaxBeds();
            Hotel_UI ui = interactionHandler.current.OpumUI.GetComponent<Hotel_UI>();
            ui.abeds = hotel_w.maxBeds;
            ui.tbeds = hotel_w.bedsTaken;
            ui.level = hotel_w.levelOfHotel;
            if (hotel_w.levelOfHotel < objectHandler.current.HotelCosts.Count)
            {
                ui.amountNeededToUpgrade = objectHandler.current.HotelCosts[hotel_w.levelOfHotel];
            }
            else
            {
                ui.amountNeededToUpgrade = -3;
            }
            ui.reload();
        }
    }
}
