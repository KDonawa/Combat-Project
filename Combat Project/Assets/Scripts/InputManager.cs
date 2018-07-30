using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    #region SingletonPattern
    protected static InputManager singleton;
    public static InputManager Singleton
    {
        get
        {
            if (singleton == null) singleton = FindObjectOfType<InputManager>();
            return singleton;
        }
    }
    #endregion //useless
      

    #region References
    // Components
    StateManager stateManager;

    // Scriptable Objects
    public References references;

    // Data Containers
    [SerializeField] private List<KeyCode> observedKeysHeld;
    [SerializeField] private List<KeyCode> observedKeysOnDownPress;

    // Variables
    [HideInInspector] public float vertical;
    [HideInInspector] public float horizontal;

    #endregion

    private void Awake()
    {
        singleton = this;
        references.InputManagerReference = this; // set reference to this script
        stateManager = GetComponent<StateManager>();
        //abilityContainer.SetUpInputManagerReference(this);

        observedKeysHeld = new List<KeyCode>();
        observedKeysOnDownPress = new List<KeyCode>();    
    }

    private void Update()
    {
        SetMovementInput();
    }

    public KeyCode CheckForUserInput()
    {
        // potential issues: we clear in the middle of iterating
        // maybe we can only call this function when we're in a certain state and put weapon swap in its own state (Busy or SwappingWeapon)

        if (Input.GetKey(KeyCode.LeftShift))
            return KeyCode.LeftShift;

        for (int k = 0; k < observedKeysHeld.Count; k++)
        {
            KeyCode code = observedKeysHeld[k];
            if (Input.GetKey(code))
                return code;
        }

        for (int k = 0; k < observedKeysOnDownPress.Count; k++)
        {
            KeyCode code = observedKeysOnDownPress[k];
            if (Input.GetKeyDown(code))
                return code;
        }

        return KeyCode.None;
    }

  
    public void ObserveKey(KeyCode keyCode, ButtonPressType pressType)
    {
        if (keyCode == KeyCode.None) return;

        switch (pressType)
        {
            case ButtonPressType.ButtonDown:
                observedKeysOnDownPress.Add(keyCode);
                break;
            case ButtonPressType.ButtonHold:
                observedKeysHeld.Add(keyCode);
                break;
            case ButtonPressType.ButtonUp:
                break;
            default:
                break;
        }
    }

    public void ClearObservedKeys()
    {
        observedKeysHeld.Clear();
        observedKeysOnDownPress.Clear();
    }

    public void SetMovementInput()
    {
        if (!stateManager.curActiveStateBools.moveAllowed)
        {
            vertical = 0;
            horizontal = 0;
        }
        else
        {
            vertical = Input.GetAxis("Vertical");
            horizontal = Input.GetAxis("Horizontal");
        }
    }


}


//public enum InputButton
//{
//    None,
//    Mouse_Left, Mouse_Right,
//    Left_Shift, Left_Alt, Tab, Space,
//    Alpha_1, Alpha_2, Alpha_3, Alpha_4, Alpha_5,
//    Q, E, R, T, F, G, Z, X, C, V
//}
