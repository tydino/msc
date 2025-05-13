using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureButtonStore : MonoBehaviour
{
    public GameObject ThisCreature;
    public bool coins;

    public void IClicked(int howMuch)
    {
        if (howMuch <= coinsAndDiamonds.coins && coins == true)
        {
            coinsAndDiamonds.coins = coinsAndDiamonds.coins - howMuch;
            Instantiate(ThisCreature);
        }
        else
        {
            if (howMuch <= coinsAndDiamonds.diamonds && coins == false)
            {
                coinsAndDiamonds.diamonds = coinsAndDiamonds.diamonds - howMuch;
                Instantiate(ThisCreature);
            }
            else
            {
                //nothing
            }
        }
    }
}
