﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Movement Data")]
public class MovementData : ScriptableObject {

    public float runSpeed;
    public float sprintSpeed;
    public float turnSpeed;
}