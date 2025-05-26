using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creatureControler : MonoBehaviour
{
    public creatureData thisCreature;
    [Header("currency control")]
    public int HowMuchThisCreatureMakes;
    public bool random;
    public bool coins;
    public bool diamonds;
    public bool food;
    public float howLongInSecondsTillMoneyAddedToTotal;
    [Header("timestamps")]
    public int TimeInGame;
    public int[] playWhere;
    [Header("Animator")]
    public Animator animator;
    public float Tempo;
    public int samples = 60;
    public bool sleep = false;
    [Header("audio")]
    public AudioSource AS;
    public AudioClip[] Ac;

    public void Update() {
        TimeInGame = timer.Timer;
        animator.SetFloat("s", Tempo / samples);

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
    }
    void Start()
    {
        StartCoroutine(CurrencyUpdate());
    }
    public void currencyUpdate()
    {
        if (!random)
        {
            if (coins) { Currency.coins = Currency.coins + HowMuchThisCreatureMakes; }
            if (diamonds) { Currency.diamonds = Currency.diamonds + HowMuchThisCreatureMakes; }
            if (food) { Currency.food = Currency.food + HowMuchThisCreatureMakes; }
        }
    }
    IEnumerator CurrencyUpdate()
    {
        yield return new WaitForSeconds(howLongInSecondsTillMoneyAddedToTotal);
        currencyUpdate();
        StartCoroutine(CurrencyUpdate());
    }

    public void soundPlay(int WhichOne){
        AS.PlayOneShot(Ac[WhichOne - 1]);
    }
}
