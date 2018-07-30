using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Utility/Equipped Items")]
public class EquippedItems : ScriptableObject {


    #region References

    //Scriptable Objects
    //public Inventory inventory;
    public References references;

    // Equipped references
    public Weapon mainHandWeap;
    GameObject mainHandWeapInstance;

    public Weapon offHandWeap;
    GameObject offHandWeapInstance;

    #endregion



    // Since i may have difficult getting equippable gear for my charcaters, maybe have slots that store powerups
    // which give cool effects (visual and stat based) instead of having gear


    // consider changing to bool
    public void EquipWeaponSlot(Weapon weaponToEquip, WeaponLocation location)
    {
        if (location == WeaponLocation.offHand)
        {         
            if (mainHandWeapInstance != null && mainHandWeap.weaponHandType == WeaponHandType.TwoHanded)
            {
                UnequipWeaponSlot(WeaponLocation.mainHand, false); // unequip 2h
            }
                
            
            UnequipWeaponSlot(WeaponLocation.offHand, false); // unequip offhand

            offHandWeap = weaponToEquip;
            SetWeaponTransform(WeaponLocation.offHand);
        }
        else 
        {
            if (location == WeaponLocation.bothHands)
            {
                UnequipWeaponSlot(WeaponLocation.offHand, false);
            }
            UnequipWeaponSlot(WeaponLocation.mainHand, false);

            mainHandWeap = weaponToEquip;
            SetWeaponTransform(WeaponLocation.mainHand);   // for now, I'll assume that the 2h hand position is the same as the main hand
        }

        UpdateWieldStateAndAbilitiesAndObserevdKeys();
    }
    public void UnequipWeaponSlot(WeaponLocation location, bool updateNeeded = true)
    {
        if (location == WeaponLocation.offHand)
        {
            if(offHandWeap != null)
            {
                references.playerInvetory.AddItem(offHandWeap);
                offHandWeap = null;
                Destroy(offHandWeapInstance);
                offHandWeapInstance = null;
            }                   
        }
        else if(location == WeaponLocation.mainHand)
        {
            if(mainHandWeap != null)
            {
                references.playerInvetory.AddItem(mainHandWeap);
                mainHandWeap = null;
                Destroy(mainHandWeapInstance);
                mainHandWeapInstance = null;
            }          
        }
        if(updateNeeded)
            UpdateWieldStateAndAbilitiesAndObserevdKeys();       
    }
    public void UnequipAll()
    {
        UnequipWeaponSlot(WeaponLocation.mainHand);
        UnequipWeaponSlot(WeaponLocation.offHand);
    }

    public void UpdateWieldStateAndAbilitiesAndObserevdKeys()
    {
        UpdateWieldState();
        UpdateAbilities();
        UpdateObservedKeys();
    }

    private void UpdateObservedKeys()
    {
        references.abilityManager.UpdateObservedKeys();
    }
    private void UpdateAbilities()
    {
        references.abilityManager.UpdateAvailableActions(references.StateManagerReference.curWieldState);
    }
    private void UpdateWieldState()
    {
        WieldState wieldState;
        if (mainHandWeap == null)
        {
            if (offHandWeap == null)
                wieldState = WieldState.Unarmed;
            else
                wieldState = WieldState.OffHandOnly;
        }
        else
        {
            if (offHandWeap == null)
                wieldState = WieldState.MainHandOnly;
            else
                wieldState = WieldState.DualWielding;
        }

        references.StateManagerReference.UpdateWieldState(wieldState);
    }

    private void SetWeaponTransform(WeaponLocation location)
    {
        PlayerController player = references.PlayerControllerReference;

        if (location == WeaponLocation.offHand)
        {
            Transform parent = player.playerAnim.GetBoneTransform(HumanBodyBones.LeftHand);
            offHandWeapInstance = Instantiate(offHandWeap.weaponPrefab, parent);
            offHandWeapInstance.transform.localPosition = offHandWeap.offHandData.position;
            offHandWeapInstance.transform.localEulerAngles = offHandWeap.offHandData.rotation;
            offHandWeapInstance.transform.localScale = Vector3.one;
        }
        else
        {
            Transform parent = player.playerAnim.GetBoneTransform(HumanBodyBones.RightHand);
            mainHandWeapInstance = Instantiate(mainHandWeap.weaponPrefab, parent);
            mainHandWeapInstance.transform.localPosition = Vector3.zero;
            mainHandWeapInstance.transform.localEulerAngles = Vector3.zero;
            mainHandWeapInstance.transform.localScale = Vector3.one;
        }

    }
}
