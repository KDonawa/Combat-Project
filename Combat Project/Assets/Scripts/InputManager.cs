using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    [HideInInspector] public float vertical;
    [HideInInspector] public float horizontal;

    StateManager stateManager;

    private void Awake()
    {
        stateManager = GetComponent<StateManager>();
    }

    // I might need to order these based on the precedence that I want to inputs to have (since there is a side effect)
    // getkey should probably have higher precedence than getkeydown
    public InputButton GetActionInput()
    {
        if (Input.GetKey(KeyCode.LeftShift)) { return InputButton.Left_Shift; }
        else if (Input.GetKey(KeyCode.Space)) { return InputButton.Space; }
        else if (Input.GetKeyDown(KeyCode.Mouse0)) { return InputButton.Mouse_Left; }
        else if (Input.GetKeyDown(KeyCode.Mouse1)) { return InputButton.Mouse_Right; }
        else return InputButton.None;
    }

    public void GetMovementInput()
    {
        if (!stateManager.curPassiveState.moveAllowed)
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


public enum InputButton
{
    None,
    Mouse_Left, Mouse_Right,
    Left_Shift, Left_Alt, Tab, Space,
    Alpha_1, Alpha_2, Alpha_3, Alpha_4, Alpha_5,
    Q, E, R, T, F, G, Z, X, C, V
}
