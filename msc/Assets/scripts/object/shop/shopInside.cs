using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopInside : MonoBehaviour
{
    public List<GameObject> Icons;
    public Slider slider;
    void Start()
    {
        slider.maxValue = Icons.Count - 1;
    }
    void Update()
    {
        for (int i = 0; i < Icons.Count; i++)
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
