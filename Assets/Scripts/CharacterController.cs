using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    // Extensive set of Variables
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

    private bool canDash = true;
    private bool canRun = true;
    private bool playingHitSound = false;

    private Animator animator;


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

        animator = GetComponentInChildren<Animator>();
    }

    // Handle Controller Input for every (configured) button
    public void GetInput()
    {
        forwardInput = Input.GetAxis(inputSetting.FORWARD_AXIS);
        animator.SetBool("Run", !Mathf.Approximately(0, Input.GetAxisRaw(inputSetting.FORWARD_AXIS)));
        
        turnInput = Input.GetAxis(inputSetting.TURN_AXIS);
        dashInput = Input.GetAxis("left Trigger");

        if (Input.GetButtonDown("Attack"))
        {
            animator.SetTrigger("Slash");

            if (!playingHitSound)
            {
                StartCoroutine(HitSound());
            }
        }

        if (Input.GetButtonDown("Cancel"))
        {
            PlayerDamage playerDamage = GetComponent<PlayerDamage>();
            playerDamage.TakeDamage(playerDamage.health);
        }
    }

    private IEnumerator HitSound()
    {
        playingHitSound = true;
        yield return new WaitForSeconds(0.5f);
        GetComponents<AudioSource>()[1].Play();
        yield return new WaitForSeconds(0.57f);
        playingHitSound = false;
    }

    public void Update()
    {
        GetInput();
        Turn();

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        canRun = !stateInfo.IsName("Slash") && !stateInfo.IsName("Die");

        // Move the player
        if (forwardInput > 0.2 && canRun)
        {
            forwardInputAssurnce = 3;
        } else
        {
            forwardInputAssurnce = 0;
        }

        // Dash
        if (dashInput >= 0.6 && canRun && canDash)
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
        // Either move at constant speed or stop moving immediately
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
        // Math-stuff to calculate turning
        if (Mathf.Abs(turnInput) > inputSetting.inputDelay && canRun)
        {
            targetRotation *= Quaternion.AngleAxis(moveSetting.rotateVel * turnInput * Time.deltaTime, Vector3.up);
        }
        transform.rotation = targetRotation;
    }

    public IEnumerator KillPlayer()
    {
        canDash = false;
        canRun = false;
        animator.SetBool("Dead", true);
        yield return new WaitForSeconds(3);
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);

        yield return null;
    }

}
