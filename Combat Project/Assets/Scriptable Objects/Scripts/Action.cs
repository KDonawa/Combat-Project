using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// rename to comabt action

public abstract class Action : ScriptableObject{

    [HideInInspector] public ActionType actionType;
    public bool rootMotionAction; // can the player move while performing this action?
    //bool isAnimationLocked; // what does it mean to be animation locked?? does it mean we can't move?
    public ButtonPressType pressType;
     
}

public enum ActionType
{
    attack, block, spell, parry, sprint
}

public enum ButtonPressType
{
    ButtonDown, ButtonHold, ButtonUp
}

