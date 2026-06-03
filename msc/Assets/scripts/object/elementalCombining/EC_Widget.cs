using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EC_Widget : ObjectTimersBase
{
    [Header("visuals/animation")]
    public SpriteRenderer inputOne;
    public SpriteRenderer inputTwo;
    public SpriteRenderer output;
    public Animator animator;
    public int samples;

    [Header("audio things")]
    public AudioSource AS;
    public AudioClip A_Collect;
    public AudioClip A_Failure;
    public AudioClip A_Success;
    public AudioClip A_YouDidBad;

    [Header("save data things")]
    public int creatureDone;
    public int creature1_Egg;
    public int creature2_Egg;

    [Header("private varibles")]
    public bool isBreeding;
    public creatureData Creature1;
    public string creature1_E;
    public creatureData Creature2;
    public string creature2_E;

    void Start()
    {
        samples = 60;
        animator.SetFloat("s", timer.Tempo / samples);
    }

    void Update()
    {
        if (status == Status.idle)
        {
            animator.SetBool("working", false);
            animator.SetBool("waiting", false);
            inputOne.sprite = null;
            inputTwo.sprite = null;
            output.sprite = null;
        }
        else if (status == Status.working)
        {
            animator.SetBool("working", true);
            animator.SetBool("waiting", false);
            inputOne.sprite = creatureHandler.current.creatureObjects[creature1_Egg].egg;
            inputTwo.sprite = creatureHandler.current.creatureObjects[creature2_Egg].egg;
            output.sprite = null;
        }
        else if (status == Status.complete)
        {
            animator.SetBool("working", false);
            animator.SetBool("waiting", true);
            inputOne.sprite = null;
            inputTwo.sprite = null;
            output.sprite = creatureHandler.current.creatureObjects[creatureDone-1].egg;
        }
        SetUpTimer();
    }
}
