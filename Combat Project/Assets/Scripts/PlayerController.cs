using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
NOTES:

*/
[RequireComponent(typeof(Rigidbody), typeof(InputManager), typeof(StateManager))]
public class PlayerController : MonoBehaviour {


    public MovementData movementData;
    public Transform cameraTransform; 
    public Weapon equippedWeapon; // will need a function to reset this is if weapon is changed in game

    #region References
    InputManager inputManager;
    StateManager stateManager;
    Animator playerAnim; // an animator must be attached to the child of the game object this script is attached to
    [HideInInspector] public Rigidbody rigid;
    GameObject player; // note that player is the child of the Player Controller
    #endregion

    Transform _transform;
    float moveAmount;

    private void Awake()
    {
        _transform = transform;

        rigid = GetComponent<Rigidbody>();
        inputManager = GetComponent<InputManager>();
        stateManager = GetComponent<StateManager>();

        playerAnim = GetComponentInChildren<Animator>(); // ref to animator component on the player game object

        player = playerAnim.gameObject; // reference to player game gameobject itself
        player.AddComponent<RootMotionHelper>();

    }

    
    private void Update()
    {
        InputButton buttonPressed = inputManager.GetActionInput();

        if (buttonPressed == InputButton.None)
            return;

        Action action = equippedWeapon.GetAction(buttonPressed);
        if (action != null)
        {
            ActionType type = action.actionType;
            if (stateManager.AttemptActiveStateChange(type))
            {
                PerformAction(type, action);
            }
        }
    }


    private void PerformAction(ActionType type, Action action = null)
    {
        switch (type)
        {
            case ActionType.attack:
                PerformAttackAction(action);
                break;
            case ActionType.block:
                break;
            case ActionType.spell:
                break;
            case ActionType.parry:
                break;
            default:
                break;
        }      
    }

    private void PerformSprint()
    {
        playerAnim.SetBool("isSprinting", true);
    }

    private void PerformAttackAction(Action action)
    {
        playerAnim.applyRootMotion = action.rootMotionAction; // root motion messes up the camera
        Attack attack = (Attack)action.actionToPerform;

        //playerAnim.SetBool("mirror", action.isMirrored);
        //if (attack.changeSpeed)
            //playerAnim.SetFloat("speed", attack.animSpeed);
        playerAnim.Play(attack.attackAnim.value);
        //playerAnim.CrossFade(attack.attackAnim.value, 0.15f); // cross fade is buggy
    }

    private void FixedUpdate()
    {
        inputManager.GetMovementInput();

        float h = inputManager.horizontal;
        float v = inputManager.vertical;

        moveAmount = Mathf.Clamp01(Mathf.Abs(h) + Mathf.Abs(v));


        if (moveAmount > 0f)
        {
            RotatePlayer(h, v);
            MovePlayer(h, v);
        }
               
        AnimatePlayerMovement(h, v);
    }

    
    private void MovePlayer(float h, float v)
    {
        //float moveSpeed = stateManager.curActiveState == isSprinting ? movementData.sprintSpeed : movementData.runSpeed;
        rigid.velocity = movementData.runSpeed * moveAmount * _transform.forward;
    }

    private void RotatePlayer(float h, float v)
    {
        Vector3 targetDir = cameraTransform.forward * v + cameraTransform.right * h;
        targetDir.y = 0f;

        Quaternion targetRot = Quaternion.LookRotation(targetDir);
        Quaternion currentRot = Quaternion.Slerp(_transform.rotation, targetRot, Time.deltaTime * moveAmount * movementData.turnSpeed);
        _transform.rotation = currentRot;
    }

    private void AnimatePlayerMovement(float h, float v)
    {     
        playerAnim.SetFloat("vertical", moveAmount, 0.15f, Time.deltaTime);
    }
}

