using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopInside : MonoBehaviour
{
    public GameObject[] Icons;
    public Slider slider;
    void Start()
    {
        slider.maxValue = Icons.Length - 1;
    }
    void Update()
    {
        for (int i = 0; i < Icons.Length; i++)
        {
            if (slider.value == i)
            {
                Icons[i].SetActive(true);
            }
            else
            {
                Icons[i].SetActive(false);
            }
        }
    }
}
