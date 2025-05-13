using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coinsAndDiamonds : MonoBehaviour
{
    public static int coins = 0;
    public static int diamonds;
    public Text Coins;
    public Text Diamonds;

    void Update()
    {
        Coins.text = coins.ToString();
        Diamonds.text = diamonds.ToString();
    }
}
