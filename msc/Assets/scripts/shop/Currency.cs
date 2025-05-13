using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Currency : MonoBehaviour
{
    public static int coins;
    public static int diamonds;
    public static int food;
    public Text Coins;
    public Text Diamonds;
    public Text Food;

    void Update()
    {
        Coins.text = coins.ToString();
        Diamonds.text = diamonds.ToString();
        Food.text = food.ToString();
    }
}
