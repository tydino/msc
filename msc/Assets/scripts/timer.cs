using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timer : MonoBehaviour
{
    public List<t_creaturesNeeded> creaturesNeededForEachTime;
    [Header("limit is 1 less then song's true end")]
    public int limit;
    public Animator a;
    public static int Timer;
    void Start()
    {
        Timer = 0;
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
        bool contains = false;
        while (contains == false)
        {
            foreach (int ID in creaturesNeededForEachTime[Timer].creatureIdNeeded)
            {
                foreach (sd_CreatureHandler sdh in creatureHandler.current.creatureInformation)
                {
                    if (sdh.CreatureID == ID && sdh.asleep == false)
                    {
                        contains = true;
                    }
                }
            }
            if (contains == false)
            {
                Timer++;
            }
            if (limit < Timer)
            {
                Timer = 0;
                break;
            }
        }

    }
}

[System.Serializable]
public class t_creaturesNeeded
{
    public List<int> creatureIdNeeded;
}
