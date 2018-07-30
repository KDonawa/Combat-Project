using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Utility/Ability Container")]
public class AbilityContainer : ScriptableObject {

    #region References

    //Scriptable Objects
    public References references;

    // Data Containers
    [SerializeField] List<Ability> mainHandAbilities = new List<Ability>();
    [SerializeField] List<Ability> offHandAbilities = new List<Ability>();
    //[SerializeField] List<Ability> passiveAbilities = new List<Ability>();
    Dictionary<KeyCode, Action> availableAbilities = new Dictionary<KeyCode, Action>();

    #endregion

    public void UpdateAvailableActions(WieldState wieldState)
    {
        availableAbilities.Clear();

        switch (wieldState)
        {
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

    private void AddActions(ref List<Ability> source)
    {
        for (int k = 0; k < source.Count; k++)
        {
            Ability ability = source[k];
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


    [System.Serializable]
    public class Ability
    {
        public Action action;
        public KeyCode inputButton;
    }

}
