using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EC_button : MonoBehaviour
{
    public Image CreatureImage;
    public Text CreatureName;
    public Image ElementImage1;
    public Image ElementImage2;
    public Image ElementImage3;
    public Image ElementImage4;
    public Image ElementImage5;
    public Image ElementImage6;
    public creatureData cd;
    public int OneOrTwo;
    public void IClicked()
    {
        EC_Universal.current.Button(cd, OneOrTwo);
    }

    public void SetupButton(creatureData CreatureData)
    {
        cd = CreatureData;

        CreatureImage.sprite = cd.psd;
        CreatureName.text = cd.creatureName;
        setImages(null, null, null, null, null, null);
        if (cd.element.Length == 0)
        {
            Debug.LogError(cd.creatureName + " is missing it's elements");
        }
        if (cd.element.Length == 1)
        {
            setImages(goThroughECUniversalsElements(cd.element[0]), null, null, null, null, null);
        }
        if (cd.element.Length == 2)
        {
            setImages(goThroughECUniversalsElements(cd.element[0]), 
                goThroughECUniversalsElements(cd.element[1]), null, null, null, null);
        }
        if (cd.element.Length == 3)
        {
            setImages(goThroughECUniversalsElements(cd.element[0]),
                goThroughECUniversalsElements(cd.element[1]),
                goThroughECUniversalsElements(cd.element[2]), null, null, null);
        }
        if (cd.element.Length == 4)
        {
            setImages(goThroughECUniversalsElements(cd.element[0]),
                goThroughECUniversalsElements(cd.element[1]),
                goThroughECUniversalsElements(cd.element[2]),
                goThroughECUniversalsElements(cd.element[3]), null, null);
        }
        if (cd.element.Length == 5)
        {
            setImages(goThroughECUniversalsElements(cd.element[0]),
                goThroughECUniversalsElements(cd.element[1]),
                goThroughECUniversalsElements(cd.element[2]),
                goThroughECUniversalsElements(cd.element[3]),
                goThroughECUniversalsElements(cd.element[4]), null);
        }
        if (cd.element.Length == 6)
        {
            setImages(goThroughECUniversalsElements(cd.element[0]),
                goThroughECUniversalsElements(cd.element[1]),
                goThroughECUniversalsElements(cd.element[2]),
                goThroughECUniversalsElements(cd.element[3]),
                goThroughECUniversalsElements(cd.element[4]),
                goThroughECUniversalsElements(cd.element[5]));
        }
    }

    Sprite goThroughECUniversalsElements(string elementname)
    {
        bool found = false;
        foreach(EC_Universal.ECAvailableElements available in EC_Universal.current.AvailableElements)
        {
            if(available.element == elementname)
            {
                found = true;
                return available.elementSprite;
            }
        }
        if(found == false)
        {
            Debug.LogError("Your missing the " + elementname + " on this place.");
        }
        return null;
    }

    void setImages(Sprite image1, Sprite image2, Sprite image3, Sprite image4, Sprite image5, Sprite image6)
    {
        if (image1 != null)
        {
            ElementImage1.sprite = image1;
        }
        else
        {
            ElementImage1.sprite = interactionHandler.current.emptySprite;
        }
        if (image2 != null)
        {
            ElementImage2.sprite = image2;
        }
        else
        {
            ElementImage2.sprite = interactionHandler.current.emptySprite;
        }
        if (image3 != null)
        {
            ElementImage3.sprite = image3;
        }
        else
        {
            ElementImage3.sprite = interactionHandler.current.emptySprite;
        }
        if (image4 != null)
        {
            ElementImage4.sprite = image4;
        }
        else
        {
            ElementImage4.sprite = interactionHandler.current.emptySprite;
        }
        if (image5 != null)
        {
            ElementImage5.sprite = image5;
        }
        else
        {
            ElementImage5.sprite = interactionHandler.current.emptySprite;
        }
        if (image6 != null)
        {
            ElementImage6.sprite = image6;
        }
        else
        {
            ElementImage6.sprite = interactionHandler.current.emptySprite;
        }
    }
}
