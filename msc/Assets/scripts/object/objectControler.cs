using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectControler : MonoBehaviour
{
    //Universal
    public GameObject ThisObject;
    public ObjectTypes ThisObjectType;

    //Elemental Combiner
    public EC_Widget ElementalCombinerWidget;

    //Mrs Incubator

    //Numster

    //Stan

    public enum ObjectTypes
    {
        ElementalCombiner,//Elemental combiner
        MrsIncubator,//incubator INCOMPLETE
        Market,//shop INCOMPLETE
        Maps,//map INCOMPLETE
        Numster,//food INCOMPLETE
        Stan,//news INCOMPLETE
        Decoration//No UI INCOMPLETE
    }

    private void Start()
    {
        ThisObject = gameObject;
        if(ThisObjectType == ObjectTypes.ElementalCombiner)
        {
            EC_Universal.current.ECs.Add(gameObject);
        }else if (ThisObjectType == ObjectTypes.MrsIncubator)
        {
            MI_Universal.current.MIs.Add(gameObject);
        }
    }

    public string CompileData()///   Save    ///
    {
        #region Elemental Combiner
        if (ThisObjectType == ObjectTypes.ElementalCombiner)
        {
            string final = "";
            if (ElementalCombinerWidget.inProgress)
            {
                final = "T";
            }
            else
            {
                final = "F";
            }
            final = final + ElementalCombinerWidget.TimerStart.ToString().Length.ToString().Length.ToString();
            final = final + ElementalCombinerWidget.TimerStart.ToString().Length.ToString();
            final = final + ElementalCombinerWidget.TimerStart.ToString();
            final = final + ElementalCombinerWidget.TimerEnd.ToString().Length.ToString().Length.ToString();
            final = final + ElementalCombinerWidget.TimerEnd.ToString().Length.ToString();
            final = final + ElementalCombinerWidget.TimerEnd.ToString();
            final = final + ElementalCombinerWidget.status.ToString().Length.ToString().Length.ToString();
            final = final + ElementalCombinerWidget.status.ToString().Length.ToString();
            final = final + ElementalCombinerWidget.status.ToString();
            final = final + ElementalCombinerWidget.creatureDone.ToString().Length.ToString().Length.ToString();
            final = final + ElementalCombinerWidget.creatureDone.ToString().Length.ToString();
            final = final + ElementalCombinerWidget.creatureDone.ToString();
            final = final + ElementalCombinerWidget.creature1_Egg.ToString().Length.ToString().Length.ToString();
            final = final + ElementalCombinerWidget.creature1_Egg.ToString().Length.ToString();
            final = final + ElementalCombinerWidget.creature1_Egg.ToString();
            final = final + ElementalCombinerWidget.creature2_Egg.ToString().Length.ToString().Length.ToString();
            final = final + ElementalCombinerWidget.creature2_Egg.ToString().Length.ToString();
            final = final + ElementalCombinerWidget.creature2_Egg.ToString();
            return final;
        }
        #endregion
        /*else if (ThisObjectType == ObjectTypes.MrsIncubator)
        {
            //empty
        }
        else if (ThisObjectType == ObjectTypes.Numster)
        {
            //empty
        }*/
        else
        {
            return null;
        }
    }

    public void DecompileData(string Data)///   Load    ///
    {
        if (Data != null || Data == "") 
        {
            #region Elemental Combiner
            /*if (ThisObjectType == ObjectTypes.ElementalCombiner)
            {//save data for EC: InProgress DateStart DateEnd Status CreatureDone Creature1 Creature2
                int index = 0;

                //gets inprogress bool
                string character = Data[index].ToString();///this gets individual character as a string
                if (character == "T")
                {
                    ElementalCombinerWidget.inProgress = true;
                }
                if (character == "F")
                {
                    ElementalCombinerWidget.inProgress = false;
                }

                index++;

                //gets a int of amount of characters for date length's amount of characters for DateStart
                int Length = int.Parse(Data[index].ToString());
                string LengthString = string.Empty;

                for (int i = 0; i < Length; i++)
                {
                    index++;
                    character = Data[index].ToString();
                    LengthString = LengthString + character;
                }

                string final = string.Empty;

                for (int i = 0; i < int.Parse(LengthString); i++)
                {
                    index++;
                    character = Data[index].ToString();
                    final = final + character;
                }

                ElementalCombinerWidget.TimerStart = Convert.ToDateTime(final);

                index++;

                //gets DateEnd
                Length = int.Parse(Data[index].ToString());
                LengthString = string.Empty;

                for (int i = 0; Length < i; i++)
                {
                    index++;
                    character = Data[index].ToString();
                    LengthString = LengthString + character;
                }

                Debug.Log(LengthString);

                final = string.Empty;

                for (int i = 0; i < int.Parse(LengthString); i++)
                {
                    index++;
                    character = Data[index].ToString();
                    final = final + character;
                }

                ElementalCombinerWidget.TimerEnd = Convert.ToDateTime(final);

                index++;

                ///     Status      ///
                Length = int.Parse(Data[index].ToString());
                LengthString = string.Empty;

                for (int i = 0; Length < i; i++)
                {
                    index++;
                    character = Data[index].ToString();
                    LengthString = LengthString + character;
                }

                final = string.Empty;

                for (int i = 0; i < int.Parse(LengthString); i++)
                {
                    index++;
                    character = Data[index].ToString();
                    final = final + character;
                }

                if (final == ObjectTimersBase.Status.complete.ToString())
                {
                    ElementalCombinerWidget.status = ObjectTimersBase.Status.complete;
                } else if (final == ObjectTimersBase.Status.idle.ToString())
                {
                    ElementalCombinerWidget.status = ObjectTimersBase.Status.idle;
                }
                if (final == ObjectTimersBase.Status.working.ToString())
                {
                    ElementalCombinerWidget.status = ObjectTimersBase.Status.working;
                }

                index++;

                //creature done
                Length = int.Parse(Data[index].ToString());
                LengthString = string.Empty;

                for (int i = 0; Length < i; i++)
                {
                    index++;
                    character = Data[index].ToString();
                    LengthString = LengthString + character;
                }
                ElementalCombinerWidget.creatureDone = int.Parse(LengthString);

                index++;

                //creature1
                Length = int.Parse(Data[index].ToString());
                LengthString = string.Empty;

                for (int i = 0; Length < i; i++)
                {
                    index++;
                    character = Data[index].ToString();
                    LengthString = LengthString + character;
                }
                ElementalCombinerWidget.creature1_Egg = int.Parse(LengthString);

                index++;

                //creature2
                Length = int.Parse(Data[index].ToString());
                LengthString = string.Empty;

                for (int i = 0; Length < i; i++)
                {
                    index++;
                    character = Data[index].ToString();
                    LengthString = LengthString + character;
                }
                ElementalCombinerWidget.creature2_Egg = int.Parse(LengthString);
            }*/
            Debug.Log("you need to reinstate the Elemental combiner Decompilation");
            #endregion
        }
    }
}
