using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creatureControler : MonoBehaviour
{
    public creatureData thisCreature;
    public CurrencyControl currencyControl;
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
        if (currencyControl.currencyIdentifier != CurrencyIdentifier.random)
        {
            if (currencyControl.currencyIdentifier == CurrencyIdentifier.coins) { Currency.coins = Currency.coins + currencyControl.HowMuchThisCreatureMakes; }
            if (currencyControl.currencyIdentifier == CurrencyIdentifier.diamonds) { Currency.diamonds = Currency.diamonds + currencyControl.HowMuchThisCreatureMakes; }
            if (currencyControl.currencyIdentifier == CurrencyIdentifier.food) { Currency.food = Currency.food + currencyControl.HowMuchThisCreatureMakes; }
        }
        else
        {
            ///TODO: randomize what currency will be used.
        }
    }
    IEnumerator CurrencyUpdate()
    {
        if (currencyControl.howLongInSecondsTillMoneyAddedToTotal! < 1)
        {
            yield return new WaitForSeconds(currencyControl.howLongInSecondsTillMoneyAddedToTotal);
            currencyUpdate();
            StartCoroutine(CurrencyUpdate());
        }
    }

    public void soundPlay(int WhichOne){
        AS.PlayOneShot(Ac[WhichOne - 1]);
    }

    [System.Serializable]
    public struct CurrencyControl
    {
        public int HowMuchThisCreatureMakes;
        public CurrencyIdentifier currencyIdentifier;
        public float howLongInSecondsTillMoneyAddedToTotal;
    }

    public enum CurrencyIdentifier{
        coins,
        diamonds,
        food,
        random
    }
}
