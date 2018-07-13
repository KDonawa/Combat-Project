using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// consider only updating the state manager here
[RequireComponent(typeof(Animator))]
public class RootMotionHelper : MonoBehaviour {

    Animator playerAnim;
    StateManager stateManager;   
    PlayerController player;

    PassiveState curState;

    private void Awake()
    {
        playerAnim = GetComponent<Animator>();
        stateManager = GetComponentInParent<StateManager>();
        player = GetComponentInParent<PlayerController>();
    }

    private void Start()
    {
        curState = stateManager.curPassiveState;
    }

    private void OnAnimatorMove()
    {
        if (!curState.rootMotionEnabled)
            return;

        //float multiplier = 1f;

        Vector3 delta = playerAnim.deltaPosition;
        delta.y = 0;
        Vector3 velocity = delta  / Time.deltaTime;
        player.rigid.velocity = velocity;

    }
}
