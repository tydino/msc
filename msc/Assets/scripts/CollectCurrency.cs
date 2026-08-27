using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCurrency : MonoBehaviour
{
    public GameObject creature;

    public void collectCurrency()
    {
        creatureControler cc = creature.GetComponent<creatureControler>();
        if(cc.Currency.WhatCurrency == creatureControler.CurrencyIdentifier.coins)
        {
            Currency.coins = Currency.coins + cc.Currency.Amount;
        }
        if (cc.Currency.WhatCurrency == creatureControler.CurrencyIdentifier.diamonds)
        {
            Currency.diamonds = Currency.diamonds + cc.Currency.Amount;
        }
        if (cc.Currency.WhatCurrency == creatureControler.CurrencyIdentifier.food)
        {
            Currency.food = Currency.food + cc.Currency.Amount;
        }
        cc.Currency.Amount = 0;
        cc.Currency.LastCollectTime = DateTime.Now;
        Destroy(gameObject);
    }
}
