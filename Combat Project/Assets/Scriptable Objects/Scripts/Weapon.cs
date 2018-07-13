using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Weapon")]
public class Weapon : ScriptableObject {

    // container which holds all actions associated with a specific weapon
    // actions are assigned in the inspector
    public ActionContainer[] actions; // consider making it a serialized field

    //public String oh_idle;
    //public String th_idle;
    //public GameObject weaponPrefab;

    public Action GetAction(InputButton buttonPressed)
    {
        for (int k = 0; k < actions.Length; k++)
        {
            if (actions[k].inputButton == buttonPressed)
                return actions[k].action;
        }
        return null;
    }

}

[System.Serializable]
public class ActionContainer
{
    public InputButton inputButton;
    public Action action;
}


