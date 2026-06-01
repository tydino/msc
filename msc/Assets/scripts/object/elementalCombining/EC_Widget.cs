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
    int samples = 60;

    [Header("audio things")]
    public AudioSource AS;
    public AudioClip[] Ac;

    [Header("save data things")]
    public int creatureDone;
    public int creature1_Egg;
    public int creature2_Egg;

    [Header("private varibles")]
    [SerializeField] bool isBreeding;
    [SerializeField] creatureData Creature1;
    [SerializeField] string creature1_E;
    [SerializeField] creatureData Creature2;
    [SerializeField] string creature2_E;
}
