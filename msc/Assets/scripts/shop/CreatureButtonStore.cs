using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureButtonStore : MonoBehaviour
{
    public GameObject ThisCreature;
    public bool coins;
    public bool diamonds;
    public bool food;

    public void IClicked(int howMuch)
    {
        if (howMuch <= Currency.coins && coins == true)
        {
            if (diamonds == false && food == false)
            {
                Currency.coins = Currency.coins - howMuch;
                Instantiate(ThisCreature);
            }
            else
            {
                if(diamonds == true && howMuch <= Currency.diamonds && food == false)
                {
                    Currency.diamonds = Currency.diamonds - howMuch;
                    Currency.coins = Currency.coins - howMuch;
                    Instantiate(ThisCreature);
                }
                if (diamonds == false && howMuch <= Currency.food && food == true)
                {
                    Currency.coins = Currency.coins - howMuch;
                    Currency.food = Currency.food - howMuch;
                    Instantiate(ThisCreature);
                }
                if (diamonds == true && howMuch <= Currency.diamonds && food == true && howMuch <= Currency.food)
                {
                    Currency.diamonds = Currency.diamonds - howMuch;
                    Currency.coins = Currency.coins - howMuch;
                    Currency.food = Currency.food - howMuch;
                    Instantiate(ThisCreature);
                }
            }
        }
        else
        {
            if (howMuch <= Currency.diamonds && diamonds == true)
            {
                if (coins == false && food == false)
                {
                    Currency.diamonds = Currency.diamonds - howMuch;
                    Instantiate(ThisCreature);
                }
                else
                {
                    if (coins == true && howMuch <= Currency.coins && food == false)
                    {
                        Currency.diamonds = Currency.diamonds - howMuch;
                        Currency.coins = Currency.coins - howMuch;
                        Instantiate(ThisCreature);
                    }
                    if (coins == false && howMuch <= Currency.food && food == true)
                    {
                        Currency.coins = Currency.coins - howMuch;
                        Currency.food = Currency.food - howMuch;
                        Instantiate(ThisCreature);
                    }
                    if (coins == true && howMuch <= Currency.coins && food == true && howMuch <= Currency.food)
                    {
                        Currency.diamonds = Currency.diamonds - howMuch;
                        Currency.coins = Currency.coins - howMuch;
                        Currency.food = Currency.food - howMuch;
                        Instantiate(ThisCreature);
                    }
                }
            }
            else
            {
                if (howMuch <= Currency.food && food == true)
                {
                    Currency.food = Currency.food - howMuch;
                    Instantiate(ThisCreature);
                }
            }
        }
    }
}
