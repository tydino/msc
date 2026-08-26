using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creatureControler : MonoBehaviour
{
    public creatureData thisCreature;
    public CurrencyValues Currency;
    [Header("timestamps")]
    public int TimeInGame;
    public int[] playWhere;
    [Header("Animator")]
    public Animator animator;
    public int samples = 60;
    public bool sleep = false;
    [Header("audio")]
    public AudioSource AS;
    public AudioClip[] Ac;

    public void Update() {
        TimeInGame = timer.Timer;
        animator.SetFloat("s", timer.Tempo / samples);

        if (animator != null)
        {
            for (int i = 0; i < playWhere.Length; i++)
            {
                if (i == TimeInGame)
                {
                    animator.SetInteger("p", playWhere[i]);
                    if (playWhere[i] == 0)
                    {
                        AS.enabled = false;
                    }
                    else
                    {
                        AS.enabled = true;
                    }
                }
            }

            if (sleep == false)
            {
                animator.SetBool("z", false);
                AS.enabled = true;

            }
            else
            {
                AS.enabled = false;
                animator.SetBool("z", true);
            }
        }

        if (Currency.Amount >= Currency.CollectMin) {
            interactionHandler.current.CurrencyReload(false, this.gameObject.transform);
        }
    }

    public void soundPlay(int WhichOne)
    {
        AS.PlayOneShot(Ac[WhichOne - 1]);
    }

    [System.Serializable]
    public struct CurrencyValues
    {
        public CurrencyIdentifier WhatCurrency;
        public int RatePerSecond;
        public int RatePerMinute;
        public int RatePerHour;
        public int RatePerDay;
        public int Amount;
        public int Max;//amount of collectMins before you cannot collect anymore
        public int CollectMin;
        public DateTime LastCollectTime;
    }

    public enum CurrencyIdentifier{
        coins,
        diamonds,
        food,
        random
    }

    public void DecompileCurrency(string Data)
    {
        int index = 0;
        int length;
        string final = "";
        int.TryParse(Data[index].ToString(), out length);
        for (int i = 0; i < length; i++)
        {
            index++;
            final = final + Data[index].ToString();
        }

        int.TryParse(final, out length);
        final = "";
        for (int i = 0; i < length; i++)
        {
            index++;
            final = final + Data[index].ToString();
        }
        Currency.LastCollectTime = Convert.ToDateTime(final);

    }
    public string CompileCurrency()
    {
        string final;
        final = Currency.LastCollectTime.ToString().Length.ToString().Length.ToString();
        final = final + Currency.LastCollectTime.ToString().Length.ToString();
        final = final + Currency.LastCollectTime.ToString();
        return final;
    }
}
