using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {


    public ActiveState curActiveState;
    public PassiveState curPassiveState;

    Animator playerAnim;

    private void Awake()
    {
        playerAnim = GetComponentInChildren<Animator>();

        ResetAllStates();
    }

    private void LateUpdate()
    {
        if (curActiveState != ActiveState.Default && playerAnim.GetBool("inDefaultState"))
        {
            ResetAllStates();
        }

    }

    

    void ResetAllStates()
    {
        curActiveState = ActiveState.Default;
        curPassiveState.ResetPassiveStates();
        playerAnim.applyRootMotion = false;
    }

    public bool AttemptActiveStateChange(ActionType type)
    {
        switch (type)
        {
            case ActionType.attack:
                return AttemptChangeToAttackState();
            case ActionType.block:
                //attempt to change to block state
                return false;
            case ActionType.parry:
                // attempt to change to parry state
                return false;
            case ActionType.spell:
                // attempt to change to interact state
                return false;
            default:
                return false;
        }
    }
    private bool AttemptChangeToAttackState()
    {
        // conditions which must be passed to be able to attack
        curPassiveState.inAction = playerAnim.GetBool("inAction");
        if (curPassiveState.inAction)
            return false;
        // if (!onGround) return false;

        // conditions have been passed, so we can now change appropriate states

        //****I THINK I SHOULD CALL RESET STATES HERE FIRST ****

        curActiveState = ActiveState.Attacking;
        curPassiveState.inAction = true;
        curPassiveState.moveAllowed = false;
        curPassiveState.rootMotionEnabled = true;
        // maybe just set root motion here too. will decide later
        return true;


    }
}

// these states we chosen as we can only be in one of them at a time
// subject to change as combat develops further
public enum ActiveState
{
    Default, Attacking, Blocking, Interacting
}

// multiple states in this class can be true at the same time
// these states can coexist with some of the Active States
[System.Serializable]
public class PassiveState
{
    //public bool inLockedAction; // to check if we are currently performing an action that cannot be cancelled or overwritten
    // consider setting this based on info from the player weapon animation (see if the anim has a locked animation)

    public bool inAction; // to check of we performing an action 
    public bool moveAllowed; // to check if we are able to walk while doing an action
    public bool rootMotionEnabled;

    public void ResetPassiveStates()
    {
        inAction = false;
        moveAllowed = true;
        rootMotionEnabled = false;
    }

}



