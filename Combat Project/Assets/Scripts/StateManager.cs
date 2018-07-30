using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {

    #region SingletonPattern
    protected static StateManager singleton;
    public static StateManager Singleton
    {
        get
        {
            if (singleton == null) singleton = FindObjectOfType<StateManager>();
            return singleton;
        }
    }
    #endregion

    #region References

    // Components
    Animator playerAnim;
    PlayerController playerController;

    // Scriptable Objects
    public References references;

    #endregion

    #region Player States
    public WieldState curWieldState;
    public ActiveState curActiveState;
    public ActiveStateBools curActiveStateBools;
    #endregion
    
    private void Awake()
    {
        singleton = this;
        references.StateManagerReference = this; // set reference to this script
        playerAnim = GetComponentInChildren<Animator>();

        InitializeActiveState();
        curWieldState = WieldState.Unarmed;
    }

    private void Start()
    {
        playerController = references.PlayerControllerReference;
    }


    public bool AttemptActiveStateChange(ActionType type)
    {
        switch (type)
        {
            case ActionType.attack:
                return AttemptChangeToAttackState();
            case ActionType.block:
                return AttemptToChangeToBlockState();
            case ActionType.parry:
                // attempt to change to parry state
                return false;
            case ActionType.sprint:
                return AttemptChangeToSprintState();
            default:
                return false;
        }
    }

    private bool AttemptChangeToSprintState()
    {
        if (curActiveState == ActiveState.Sprinting) return false;
        if (InAction()) return false;
        if (playerController.moveAmount < 0.5f) return false;
        // if (!onGround) return false;

        //ResetActiveState(); // may not need this here
        //Disable lockOn 
        curActiveState = ActiveState.Sprinting;
        return true;
    }
    private bool AttemptChangeToAttackState()
    {
        if (InAction()) return false;        

        //ResetActiveState();
        curActiveState = ActiveState.Attacking;
        curActiveStateBools.moveAllowed = false;
        curActiveStateBools.rootMotionEnabled = true;
        return true;
    }

    private bool AttemptToChangeToBlockState()
    {
        return false;
    }

    public void UpdateWieldState(WieldState wieldState)
    {
        curWieldState = wieldState;
    }
    public void ResetActiveState()
    {
        if (curActiveState != ActiveState.Default && !InAction()) { InitializeActiveState(); }
    }
    private void InitializeActiveState()
    {
        curActiveState = ActiveState.Default;
        curActiveStateBools.ResetActiveStateBools();
        playerAnim.applyRootMotion = false;
    }
    public bool InAction()
    {
        return playerAnim.GetBool("inAction");
    }
    
}

public enum WieldState
{
    Unarmed, MainHandOnly, OffHandOnly, DualWielding, TwoHanded
}

// these states we chosen as we can only be in one of them at a time
// subject to change as combat develops further
public enum ActiveState
{
    Default, Attacking, Blocking, Interacting, Sprinting
}

// multiple states in this class can be true at the same time
// these states can coexist with some of the Active States
[System.Serializable]
public class ActiveStateBools
{
    //public bool inLockedAction; // to check if we are currently performing an action that cannot be cancelled or overwritten
    // consider setting this based on info from the player weapon animation (see if the anim has a locked animation)

    public bool moveAllowed; // to check if we are able to walk while doing an action
    public bool rootMotionEnabled;

    public void ResetActiveStateBools()
    {
        moveAllowed = true;
        rootMotionEnabled = false;
    }

}





