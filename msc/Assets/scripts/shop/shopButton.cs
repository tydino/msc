using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopButton : MonoBehaviour
{
    public GameObject ShopUI;
    private int change = 0;

    void Start()
    {
        ShopUI.SetActive(false);
    }

    public void IClicked()
    {
        if (change == 0)
        {
            change = 1;
            ShopUI.SetActive(true);
        }
        else
        {
            if (change == 1)
            {
                change = 0;
                ShopUI.SetActive(false);
            }
        }
    }
}
