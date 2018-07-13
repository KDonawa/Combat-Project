using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ** WILL NEED TO BE IMPROVED **
public class CameraController : MonoBehaviour {

    public Transform targetToFollow;
    public Transform pivot; // used to rotate camera up and down (around x-axis)

    //public bool lockedOn;
    public float followSpeed = 6;
    public float mouseSpeed = 7;
    public float turnSmoothing = 0.1f; // the time the smoothing takes    
    public float minTiltAngle = -35;
    public float maxTiltAngle = 35;

    [HideInInspector] public Transform _transform;

    float lookAngle; // contols y-axis rotation
    float tiltAngle; // controls x-axis rotation
    float smoothX;
    float smoothY;
    float smoothXvelocity;
    float smoothYvelocity;


    private void Awake()
    {
        _transform = transform;
    }

    private void Start()
    {
        _transform.position = targetToFollow.position;
    }

    private void FixedUpdate()
    {
        FollowTarget();
    }

    private void LateUpdate()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");
       
        HandleRotations(x, y);
    }

    private void HandleRotations(float h, float v)
    {
        if (turnSmoothing > 0f)
        {
            smoothX = Mathf.SmoothDamp(smoothX, h, ref smoothXvelocity, turnSmoothing);
            smoothY = Mathf.SmoothDamp(smoothY, v, ref smoothYvelocity, turnSmoothing);
        }
        else
        {
            smoothX = h;
            smoothY = v;
        }

        // rotate pivot about x-axis
        tiltAngle -= smoothY * mouseSpeed;
        tiltAngle = Mathf.Clamp(tiltAngle, minTiltAngle, maxTiltAngle);
        pivot.localRotation = Quaternion.Euler(tiltAngle, 0, 0);

        // rotate camera holder about y-axis
        lookAngle += smoothX * mouseSpeed;
        _transform.rotation = Quaternion.Euler(0, lookAngle, 0);
    }

    private void FollowTarget()
    {
        //TODO: if target stops moving followSpeed changes to zero, so transition is slower

        _transform.position = Vector3.Lerp(_transform.position, targetToFollow.position, followSpeed * Time.deltaTime);
    }
}
