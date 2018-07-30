using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Action/Attack")]
public class Attack : Action {

    public string animName;

    Attack()
    {
        actionType = ActionType.attack;
    }
}
