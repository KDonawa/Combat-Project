using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Utility/References")]
public class References : ScriptableObject {

    // Scripts
    private PlayerController playerController;
    private InputManager inputManager;
    private StateManager stateManager;

    public PlayerController PlayerControllerReference
    {
        get { return playerController; }
        set { playerController = value; }
    }

    public InputManager InputManagerReference
    {
        get { return inputManager; }
        set { inputManager = value; }
    }

    public StateManager StateManagerReference
    {
        get { return stateManager; }
        set { stateManager = value; }
    }

    // Scriptable Objects
    [Header("Scriptable Objects")]
    public EquippedItems playerEqippedItems;
    public Inventory playerInvetory;
    public AbilityContainer playerAbilities;


}
