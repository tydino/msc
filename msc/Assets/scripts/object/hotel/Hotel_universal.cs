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
}
