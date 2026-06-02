using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MI_Widget : ObjectTimersBase
{
    [Header("Animation")]
    public SpriteRenderer input;
    public Animator animator;
    int samples = 60;
    [Header("Function")]
    public int creatureDone;
    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip A_MI_sell;
    public AudioClip A_MI_place;
    public AudioClip A_MI_patience;
    public AudioClip A_MI_nothing;
    public enum Sounds { sell, place, patience, nothing}

    void Start()
    {
        animator.SetFloat("s", timer.Tempo / samples);
        SetUpTimer();
    }
    void Update()
    {
        SetUpTimer();
        if (status == Status.idle)
        {
            animator.SetBool("working", false);
            animator.SetBool("waiting", false);
            input.sprite = null;
        }
        else if (status == Status.working)
        {
            animator.SetBool("working", true);
            animator.SetBool("waiting", false);
            input.sprite = creatureHandler.current.creatureObjects[creatureDone].egg;
        }
        else if (status == Status.complete)
        {
            animator.SetBool("working", false);
            animator.SetBool("waiting", true);
            input.sprite = creatureHandler.current.creatureObjects[creatureDone].egg;
        }
    }

    public void PlaySound(Sounds sound)
    {
        if(sound == Sounds.place)
        {
            audioSource.PlayOneShot(A_MI_place);
        }else if (sound == Sounds.sell)
        {
            audioSource.PlayOneShot(A_MI_sell);
        }
        else if(sound == Sounds.patience)
        {
            audioSource.PlayOneShot(A_MI_patience);
        }
        else if(sound == Sounds.nothing)
        {
            audioSource.PlayOneShot(A_MI_nothing);
        }
    }
}
