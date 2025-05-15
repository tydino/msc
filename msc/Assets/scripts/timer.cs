using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timer : MonoBehaviour
{
    public int limit;
    public Animator a;
    public static int Timer;
    void Start()
    {
        Timer = -2;
    }

    void Update()
    {
        if(limit < Timer)
        {
            Timer = 0;
        }
    }

    public void Tick()
    {
        Timer++;
    }
}
