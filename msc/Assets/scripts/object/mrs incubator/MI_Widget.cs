using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MI_Widget : ObjectTimersBase
{
    [Header("Animation")]
    public SpriteRenderer input;
    public Animator animator;
    int samples = 60;
    [Header("MSC")]
    public int creatureDone;

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
            input.sprite = creatureHandler.current.creatureObjects[creatureDone - 1].egg;
        }
        else if (status == Status.complete)
        {
            animator.SetBool("working", false);
            animator.SetBool("waiting", true);
            input.sprite = creatureHandler.current.creatureObjects[creatureDone - 1].egg;
        }
    }
}
