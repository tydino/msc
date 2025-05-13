using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creatureControler : MonoBehaviour
{
    [Header("timestamps")]
    public int TimeInGame;
    public int[] playWhere;
    [Header("Animator")]
    public Animator animator;
    public string AnimatorIntName;
    public float TempoBeforeCal;
    public int samples;
    public bool sleep = false;
    [Header("audio")]
    public AudioSource AS;
    public AudioClip[] Ac;

    public void Update() {
        TimeInGame = timer.Timer;
        animator.SetFloat("s", TempoBeforeCal / samples);

        for (int i = 0; i < playWhere.Length; i++)
        {
            if (i == TimeInGame)
            {
                animator.SetInteger(AnimatorIntName, playWhere[i]);
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

    public void soundPlay(int WhichOne){
        AS.PlayOneShot(Ac[WhichOne - 1]);
    }
}
