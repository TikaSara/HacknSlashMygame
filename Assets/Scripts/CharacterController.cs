using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    [System.Serializable]
    public class MoveSettings
    {
        public float forwardVel = 12;
        public float rotateVel = 100;
        public float distToGrounded = 0.1f;
        public LayerMask ground;
    }

    [System.Serializable]
    public class PhysSettings
    {
        public float downAccel = 0.75f;
    }

    [System.Serializable]
    public class InputSettings
    {
        public float inputDelay = 0.1f;
        public string FORWARD_AXIS = "Vertical";
        public string TURN_AXIS = "Horizontal";
        
    }

    public MoveSettings moveSetting = new MoveSettings();
    public PhysSettings physSetting = new PhysSettings();
    public InputSettings inputSetting = new InputSettings();

    public float inputDelay = 0.1f;
    public float forwardVel = 12;
    public float rotateVel = 100;
    public float forwardInputAssurnce = 0; 


    public Vector3 velocity = Vector3.zero;
    public  Quaternion targetRotation;
    public Rigidbody rBody;
    float forwardInput, turnInput;
    float dashInput;
    public float dashSpeed;

    bool canDash = true;

    public Quaternion TargetRotation
    {
        get { return targetRotation; }
    }

   public bool Grounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, moveSetting.distToGrounded, moveSetting.ground);
    }

   public void Start()
    {
        targetRotation = transform.rotation;
        if (GetComponent<Rigidbody>())
            rBody = GetComponent<Rigidbody>();
        else
            Debug.LogError("Character needs a rigibody");
       
    }

    public void GetInput()
    {
        forwardInput = Input.GetAxis(inputSetting.FORWARD_AXIS);
        turnInput = Input.GetAxis(inputSetting.TURN_AXIS);
        dashInput = Input.GetAxis("left Trigger");
        
    }

    public void Update()
    {
        GetInput();
        Turn();
        if (forwardInput > 0.2)
        {
            forwardInputAssurnce = 3;
        } else
        {
            forwardInputAssurnce = 0;
        }

        if (dashInput >= 0.6 && canDash)
        {
            
            rBody.AddForce(transform.forward * dashSpeed, ForceMode.Impulse);
            StartCoroutine(DashCooldown());
        }
    }

    private IEnumerator DashCooldown()
    {
        canDash = false;
        yield return new WaitForSeconds(0.5f);
        canDash = true;
    }

    public void FixedUpdate()
    {

        Run();
        rBody.velocity = transform.TransformDirection(velocity);
    }

    public void Run()
    {
        if (Mathf.Abs(forwardInput) > inputSetting.inputDelay)
        {
            //move
            velocity.z = moveSetting.forwardVel * forwardInputAssurnce;
        }
        else
            //zero velocity
            velocity.z = 0;
    }

   public  void Turn()
    {
        if (Mathf.Abs(turnInput) > inputSetting.inputDelay)
        {
            targetRotation *= Quaternion.AngleAxis(moveSetting.rotateVel * turnInput * Time.deltaTime, Vector3.up);
        }
        transform.rotation = targetRotation;
    }

}
