using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureButtonStore : MonoBehaviour
{
    public creatureData ThisCreature;
    public bool coins;
    public bool diamonds;
    public bool food;

    public void IClicked(int howMuch)
    {
        if (MI_Universal.current.FindOneInProgress())
        {
            if (howMuch <= Currency.coins && coins == true)
            {
                if (diamonds == false && food == false)
                {
                    Currency.coins = Currency.coins - howMuch;
                    MI_Universal.current.StartTimer(ThisCreature.creatureInIslandID-1);
                }
                else
                {
                    if (diamonds == true && howMuch <= Currency.diamonds && food == false)
                    {
                        Currency.diamonds = Currency.diamonds - howMuch;
                        Currency.coins = Currency.coins - howMuch;
                        MI_Universal.current.StartTimer(ThisCreature.creatureInIslandID - 1);
                    }
                    if (diamonds == false && howMuch <= Currency.food && food == true)
                    {
                        Currency.coins = Currency.coins - howMuch;
                        Currency.food = Currency.food - howMuch;
                        MI_Universal.current.StartTimer(ThisCreature.creatureInIslandID - 1);
                    }
                    if (diamonds == true && howMuch <= Currency.diamonds && food == true && howMuch <= Currency.food)
                    {
                        Currency.diamonds = Currency.diamonds - howMuch;
                        Currency.coins = Currency.coins - howMuch;
                        Currency.food = Currency.food - howMuch;
                        MI_Universal.current.StartTimer(ThisCreature.creatureInIslandID - 1);
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
                        MI_Universal.current.StartTimer(ThisCreature.creatureInIslandID - 1);
                    }
                    else
                    {
                        if (coins == true && howMuch <= Currency.coins && food == false)
                        {
                            Currency.diamonds = Currency.diamonds - howMuch;
                            Currency.coins = Currency.coins - howMuch;
                            MI_Universal.current.StartTimer(ThisCreature.creatureInIslandID - 1);
                        }
                        if (coins == false && howMuch <= Currency.food && food == true)
                        {
                            Currency.coins = Currency.coins - howMuch;
                            Currency.food = Currency.food - howMuch;
                            MI_Universal.current.StartTimer(ThisCreature.creatureInIslandID - 1);
                        }
                        if (coins == true && howMuch <= Currency.coins && food == true && howMuch <= Currency.food)
                        {
                            Currency.diamonds = Currency.diamonds - howMuch;
                            Currency.coins = Currency.coins - howMuch;
                            Currency.food = Currency.food - howMuch;
                            MI_Universal.current.StartTimer(ThisCreature.creatureInIslandID - 1);
                        }
                    }
                }
                else
                {
                    if (howMuch <= Currency.food && food == true)
                    {
                        Currency.food = Currency.food - howMuch;
                        MI_Universal.current.StartTimer(ThisCreature.creatureInIslandID - 1);
                    }
                }
            }
        }
    }
}
