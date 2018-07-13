using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// rename to comabt action
[System.Serializable]
public class Action {

    public ActionType actionType;
    public Object actionToPerform; // store action scriptable (eg. attack, spell, ...)
    public bool isMirrored; // can the action be mirrored?
    public bool rootMotionAction; // can the player move while performing this action?
    //bool isAnimationLocked; // what does it mean to be animation locked??
    // does it mean we cant move
}

public enum ActionType
{
    attack, block, spell, parry
    // consider renaming to meleeAttack, rangedAtt, magicAtt, etc
}
