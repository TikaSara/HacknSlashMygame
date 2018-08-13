using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Follows the player around in 3rd person perspective
public class CameraController : MonoBehaviour {

    public Transform target;
    public float lookSmooth = 0.9f;
    public Vector3 offsetFromTarget = new Vector3(0, 6, -8);
    public float xTilt = 10;

    Vector3 destination = Vector3.zero;
    CharacterController charController;
    float rotateVel = 0;

    void Start()
    {
        SetCameraTarget(target);
    }

    void SetCameraTarget(Transform t)
    {
        // Tell the camera what gameobject to follow
        target = t;
        if (target != null)
        {
            if (target.GetComponent<CharacterController>())
            {
                charController = target.GetComponent<CharacterController>();
            }
            else
                Debug.LogError("the camera target needs character controller");
              
        }
        else
            Debug.LogError("Your chamera needs a target");
    }

    void LateUpdate()
    {
        // moving
        MoveToTarget();
        //rotating
        LookAtTarget();
    }

    void MoveToTarget()
    {
        // Move the camera behind the player
        destination = charController.TargetRotation * offsetFromTarget;
        destination += target.position;
        transform.position = destination;
    }

    void LookAtTarget()
    {
        // Turn the camera towards the player
        float eulerYAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target.eulerAngles.y, ref rotateVel, lookSmooth);
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, eulerYAngle,0);
    }
}
