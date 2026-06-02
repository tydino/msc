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
    public MI_Widget MrsIncubatorWidget;

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
            string final;
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
        else if (ThisObjectType == ObjectTypes.MrsIncubator)
        {
            string final;
            if (MrsIncubatorWidget.inProgress)
            {
                final = "T";
            }
            else
            {
                final = "f";
            }
            final = final + MrsIncubatorWidget.status.ToString().Length.ToString();
            final = final + MrsIncubatorWidget.status.ToString();
            final = final + MrsIncubatorWidget.creatureDone.ToString().Length.ToString().Length.ToString();
            final = final + MrsIncubatorWidget.creatureDone.ToString().Length.ToString();
            final = final + MrsIncubatorWidget.creatureDone.ToString();
            final = final + MrsIncubatorWidget.TimerStart.ToString().Length.ToString().Length.ToString();
            final = final + MrsIncubatorWidget.TimerStart.ToString().Length.ToString();
            final = final + MrsIncubatorWidget.TimerStart.ToString();
            final = final + MrsIncubatorWidget.TimerEnd.ToString().Length.ToString().Length.ToString();
            final = final + MrsIncubatorWidget.TimerEnd.ToString().Length.ToString();
            final = final + MrsIncubatorWidget.TimerEnd.ToString();

            return final;
        }
        /*else if (ThisObjectType == ObjectTypes.Numster)
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
        if (Data != null || Data != "") 
        {
            if(ThisObjectType == ObjectTypes.MrsIncubator)
            {//save data for MI: InProgress Status creatureDone DateStart DateEnd
                int index = 0;

                if(Data[index].ToString() == "T")
                {
                    MrsIncubatorWidget.inProgress = true;
                }
                if(Data[index].ToString() == "F")
                {
                    MrsIncubatorWidget.inProgress = false;
                }

                index++;

                string final = "";
                int length;
                int.TryParse(Data[index].ToString(), out length);
                for (int i = 0; i < length; i++)
                {
                    index++;
                    final = final + Data[index].ToString();
                }

                if(final == ObjectTimersBase.Status.complete.ToString())
                {
                    MrsIncubatorWidget.status = ObjectTimersBase.Status.complete;
                }
                if (final == ObjectTimersBase.Status.idle.ToString())
                {
                    MrsIncubatorWidget.status = ObjectTimersBase.Status.idle;
                }
                if (final == ObjectTimersBase.Status.working.ToString())
                {
                    MrsIncubatorWidget.status = ObjectTimersBase.Status.working;
                }

                index++;

                final = "";
                int.TryParse(Data[index].ToString(), out length);
                for(int i = 0; i < length; i++)
                {
                    index++;
                    final = final + Data[index].ToString();
                }

                int.TryParse(final, out length);
                final = "";
                for(int i = 0; i < length; i++)
                {
                    index++;
                    final = final + Data[index].ToString();
                }
                int.TryParse(final, out  MrsIncubatorWidget.creatureDone);

                index++;

                final = "";
                int.TryParse(Data[index].ToString(), out length);
                for (int i = 0; i < length; i++)
                {
                    index++;
                    final = final + Data[index].ToString();
                }

                int.TryParse(final, out length);
                final = "";
                for (int i = 0; i < length; i++)
                {
                    index++;
                    final = final + Data[index].ToString();
                }
                MrsIncubatorWidget.TimerStart = Convert.ToDateTime(final);

                index++;

                final = "";
                int.TryParse(Data[index].ToString(), out length);
                for (int i = 0; i < length; i++)
                {
                    index++;
                    final = final + Data[index].ToString();
                }

                int.TryParse(final, out length);
                final = "";
                for (int i = 0; i < length; i++)
                {
                    index++;
                    final = final + Data[index].ToString();
                }
                MrsIncubatorWidget.TimerEnd = Convert.ToDateTime(final);
            }
            if (ThisObjectType == ObjectTypes.ElementalCombiner)
            {//save data for EC: InProgress DateStart DateEnd Status CreatureDone Creature1 Creature2
                Debug.Log("you need to reinstate the Elemental combiner Decompilation");
            }
        }
    }
}
