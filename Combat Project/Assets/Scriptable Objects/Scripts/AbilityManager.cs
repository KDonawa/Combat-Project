using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Ability Manager")]
public class AbilityManager : ScriptableObject {

    #region References

    //Scriptable Objects
    public References references;

    // Data Containers
    [SerializeField] private AbilityContainer passiveAbilities; // put sprinting in here
    [SerializeField] private AbilityContainer unarmedAbilities;
    [SerializeField] private AbilityContainer mainHandAbilities;
    [SerializeField] private AbilityContainer offHandAbilities;

    Dictionary<KeyCode, Action> availableAbilities = new Dictionary<KeyCode, Action>();

    #endregion

    public void UpdateAvailableActions(WieldState wieldState)
    {
        availableAbilities.Clear();

        switch (wieldState)
        {
            case WieldState.Unarmed:
                AddActions(ref unarmedAbilities);
                break;
            case WieldState.MainHandOnly:
                AddActions(ref mainHandAbilities);
                break;
            case WieldState.OffHandOnly:
                AddActions(ref offHandAbilities);
                break;
            case WieldState.DualWielding:
                AddActions(ref mainHandAbilities);
                AddActions(ref offHandAbilities);
                break;
        }
    }

    private void AddActions(ref AbilityContainer source)
    {
        int count = source.abilities.Count;
        for (int k = 0; k < count; k++)
        {
            Ability ability = source.abilities[k];
            availableAbilities.Add(ability.inputButton, ability.action);           
        }
    }

    public void UpdateObservedKeys()
    {
        InputManager inputManager = references.InputManagerReference;

        inputManager.ClearObservedKeys();

        foreach(KeyValuePair<KeyCode,Action> entry in availableAbilities)
        {
            inputManager.ObserveKey(entry.Key, entry.Value.pressType);
        }
    }

    public Action GetAvailableAction(KeyCode keyCode)
    {
        Action action = null;
        availableAbilities.TryGetValue(keyCode, out action);
        return action;
    }

}

[System.Serializable]
public class Ability
{
    public Action action;
    public KeyCode inputButton;
}
