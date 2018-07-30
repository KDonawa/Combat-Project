using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
NOTES:
change weapon inheritance to deal with shields and ranged weapons etc
setup blocking
try to add sprinting to ability bar
set it up so that if wielding, arms wont move as much when running
pass things by reference
set up blocking
evaluate how dual wielding will work
set up a 2h weapon
*/
[RequireComponent(typeof(Rigidbody), typeof(InputManager), typeof(StateManager))]
public class PlayerController : MonoBehaviour {

    #region SingletonPattern
    protected static PlayerController singleton;
    public static PlayerController Singleton
    {
        get
        {
            if (singleton == null) singleton = FindObjectOfType<PlayerController>();
            return singleton;
        }
    }
    #endregion

    #region References

    // Components
    InputManager inputManager;
    [HideInInspector] public StateManager stateManager;
    public Transform cameraTransform; // camera controller GameObject
    [HideInInspector] public Animator playerAnim; // think about attaching the animator to the controller instead
    [HideInInspector] public Rigidbody rigid;

    // Scriptable Objects
    public References references;
    public MovementData movementData;
    private AbilityManager abilityManager;

    // Variables
    [HideInInspector] public float moveAmount;
    private float currentSpeed;

    // Others
    Transform _transform;
    GameObject player; // note that player is the child of the Player Controller

    #endregion

    private void Awake()
    {       
        singleton = this;
        references.PlayerControllerReference = this; // set reference to this script
        _transform = transform;

        rigid = GetComponent<Rigidbody>();
        inputManager = GetComponent<InputManager>();
        stateManager = GetComponent<StateManager>();

        playerAnim = GetComponentInChildren<Animator>(); // ref to animator component on the player game object

        player = playerAnim.gameObject; // reference to player game gameobject itself
        player.AddComponent<RootMotionHelper>();
    }

    private void Start()
    {
        abilityManager = references.abilityManager;
        references.playerEqippedItems.UpdateWieldStateAndAbilitiesAndObserevdKeys(); // here for now
    }

    private void Update()
    {
        // Encapsulate all of this: ProcessUserInput
        KeyCode buttonPressed = inputManager.CheckForUserInput();

        if (buttonPressed == KeyCode.None)
        {
            stateManager.ResetActiveState();

            // reset anim bools

            return;
        }
        if(buttonPressed == KeyCode.LeftShift)
        {          
            stateManager.AttemptActiveStateChange(ActionType.sprint);
            return;
        }

        Action action = abilityManager.GetAvailableAction(buttonPressed);
        if (action != null)
        {          
            ActionType type = action.actionType;
            if (stateManager.AttemptActiveStateChange(type))
            {
                PerformAction(type, /*equippedWeapon.location,*/ action);
            }
        }
    }

    private void PerformAction(ActionType type, /*WeaponLocation location,*/ Action action = null)
    {
        switch (type)
        {
            case ActionType.attack:
                PerformAttackAction(action/*, location*/);
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

    private void PerformAttackAction(Action action/*, WeaponLocation location*/)
    {
        playerAnim.applyRootMotion = action.rootMotionAction;
        //playerAnim.SetBool("isMirrored", location == WeaponLocation.offHand);
        Attack attack = (Attack)action;

        playerAnim.Play(attack.animName);
        //playerAnim.CrossFade(attack.attackAnim.value, 0.15f); // cross fade is buggy
    }

    private void PerformBlockAction()
    {

    }

    private void FixedUpdate()
    {
        HandlePlayerMovement();
    }

    private void HandlePlayerMovement()
    {
        float h = inputManager.horizontal;
        float v = inputManager.vertical;

        moveAmount = Mathf.Clamp01(Mathf.Abs(h) + Mathf.Abs(v));

        if (moveAmount > 0f)
        {
            RotatePlayer(h, v);
            MovePlayer(h, v);         
        }
        else
        {
            currentSpeed = 0;
        }

        AnimatePlayerMovement(h, v);


    }
    private void MovePlayer(float h, float v)
    {
        float moveSpeed = stateManager.curActiveState == ActiveState.Sprinting ? movementData.sprintSpeed : movementData.runSpeed;
        currentSpeed = Mathf.Lerp(currentSpeed, moveSpeed, Time.deltaTime * 5);
        rigid.velocity = currentSpeed * moveAmount * _transform.forward;
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

