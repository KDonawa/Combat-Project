using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Combat Actions/Attack")]
public class Attack : ScriptableObject {

    public String attackAnim; // string is a scriptable object that hols a string variable
    public bool canBeParried = true;
    public bool changeSpeed = false;
    public float animSpeed = 1;
    public bool canParry = false;
    public bool canBackstab = false;
}
