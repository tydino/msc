using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mi_mainWidget : ObjectTimersBase
{
    public static mi_mainWidget current;
    [Header("animation")]
    public SpriteRenderer input;
    public Animator animator;
    public float Tempo;
    int samples = 60;

    //save data things
    public static int creatureDone;

    #region activation
    void OnMouseDown()
    {
        if (status == Status.complete)
        {
            GridBuildingSystem.current.InitializeWithBuilding(creaturePrefabs[creatureDone-1]);
            interactionHandler.current.OpenUI(true);
            status = Status.idle;
            creatureDone = 0;
            inProgress = false;
            SaveData.current.save();
        }
    }
    #endregion


    #region unity voids
    void Awake()
    {
        current = this;
    }

    void Start()
    {
        animator.SetFloat("s", Tempo / samples);
        foreach (creatureData cd in creatureHandler.current.creatureObjects)
        {
            creaturePrefabs.Add(cd.PrefabObj);
            creatureDatas.Add(cd);
        }
        interactionHandler.current.TimeLeftObj1.SetActive(false);
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
    #endregion

}
