using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using DG.Tweening;
[System.Serializable]


public class GnomeMovement : MonoBehaviour
{
    [Header("Animation Variables")]

    public Animator player1Animator;
    [SerializeField] Transform modelTransform;
    [SerializeField] bool isStanding;

    public bool isFallingIdle;
    public bool isIdle;
    public bool isWalking;
    public bool isSprinting;
    public bool isDancing;
    public bool isJumping;

    //Fall Damage Booleans

    public bool startedStanding;
    public bool startedFalling;

    [Header("Input")]
    //INPUT:
    public SGInput player1Controls;

    [Header("Basic Variables")]
    //BASIC VARIABLES:

    [SerializeField] GameObject player1GO;
    [SerializeField] Rigidbody playerRB;

    public float defaultGravity = -9.81f;
    public float gravity = -9.81f;
    
    [Header("Movement Variables")]

    //MOVEMENT VARIABLES:

    [SerializeField] BoxCollider floorCollider;
    public FloorCollider m_floorCollider;

    public Vector3 move;
    public Vector3 look;

    public float playerSpeed;
    [SerializeField] int defaultPlayerSpeed;
    [SerializeField] int runSpeed;

    [Header("Jumping Variables")]

    public float jumpSpeed;
    public float fallSpeed;
    public Vector3 jumpHeight;
    [SerializeField] int boostJumpCost;
    [SerializeField] int boostDanceBonus;

    [Header("Cameras")]

    //CAMERA:
    public Cinemachine.CinemachineVirtualCamera vCam1;
    public Cinemachine.CinemachineVirtualCamera vCam2;

    public Cinemachine.CinemachineBrain cineBrain;

    //AUDIO

    private bool landingSoundPlayed;

    //START SINGLETON:

    public static GnomeMovement instance;
    public static GnomeMovement Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GnomeMovement>();
                if (instance == null)
                {
                    GameObject singleton = new GameObject();
                    singleton.AddComponent<GnomeMovement>();
                    singleton.name = "(Singleton) GnomeMovement";
                }
            }
            return instance;
        }
    }

    //END SINGLETON +"instance=this;" in Awake();

    private void Awake()
    {
        instance = this;

        player1Controls = new SGInput();

        gravity = defaultGravity;

        player1Controls.Player.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        player1Controls.Player.Look.performed += ctx => look = ctx.ReadValue<Vector2>();
        player1Controls.Player.Jump.performed += ctx => Jump();
        player1Controls.Player.Run.performed += ctx => Run();
        player1Controls.Player.Run.canceled += ctx => CancelRun();
        player1Controls.Player.Dance.performed += ctx => Dance();
    }

  
    void HandleMovement() //Move player transform based on Move Vector.
    {
        if (isWalking || isJumping || isSprinting || isFallingIdle)
        {
            Vector3 m = playerSpeed * Time.deltaTime * new Vector3(move.x, move.z, move.y).normalized;
            transform.Translate(m, Space.Self);
        }
    }
    void HandleRotation() //Have player look at current Position + Look Vector.
    {
        Vector3 currentPosition = transform.position;
        Vector3 newPosition = new Vector3(look.x, look.z, look.y).normalized * Time.deltaTime;
        Vector3 lookAtPosition = currentPosition + newPosition;

        transform.LookAt(lookAtPosition);
    }
    public void ResetStates()  //Used to turn off all animations.
    {
        StopIdle();
        StopWalk();
        StopFallingIdle();
        StopJumping();
        CancelRun();
        StopDancing();
    }

    // ANIMATION STATES:
    void Dance()
    {
        if (!isFallingIdle)
        {
            ResetStates();
            player1Animator.SetBool("isDancing", true);
            isDancing = true;
            Toolbox.instance.m_playerManager.boost += boostDanceBonus;
        }
    }
    void StopDancing()
    {
        player1Animator.SetBool("isDancing", false);
        isDancing = false;
    }
    void Run()
    {
        ResetStates();
        playerSpeed = runSpeed;
        player1Animator.SetBool("isRunning", true);
        isSprinting = true;
    }
    void CancelRun()
    {
        playerSpeed = defaultPlayerSpeed;
        player1Animator.SetBool("isRunning", false);
        isSprinting = false;
    }
    public void FallingIdle()
    {
        ResetStates();
        isFallingIdle = true;
        player1Animator.SetBool("isFallingIdle", true);

    }
    void StopFallingIdle()
    {
        player1Animator.SetBool("isFallingIdle", false);
        isFallingIdle = false;
    }

    void Walk()
    {
        ResetStates();
        player1Animator.SetBool("isWalking", true);
        isWalking = true;
    }
    void StopWalk()
    {
        player1Animator.SetBool("isWalking", false);
        isWalking = false;
    }
    void Idle()
    {
        ResetStates();
        player1Animator.SetBool("isIdle", true);
        isIdle = true;
    }
    void StopIdle()
    {
        player1Animator.SetBool("isIdle", false);
        isIdle = false;
    }
    public void Jump()
    {
        ResetStates();
        
        if (Toolbox.instance.m_playerManager.boost >= boostJumpCost && !isFallingIdle)
        {
            
            isJumping = true;
            Toolbox.instance.m_audio.jumping.Play();
            player1Animator.SetBool("isJumping", true);
            Debug.Log("isJumping");

            //  Debug.Log("Player Rigidbody Velocity at JUMP: " + playerRB.velocity);

            transform.Translate(jumpHeight.y * jumpSpeed * Time.deltaTime * Vector3.up); //Move player transform up when Jump called.
        }
       
    }
    void StopJumping()
    {
        player1Animator.SetBool("isJumping", false);
        isJumping = false;
    }
    private void FixedUpdate()
    {
        // SET VELOCITY TO 0 IF NO PLAYER INPUT
        
        if (!player1Controls.Player.Move.inProgress && !player1Controls.Player.Jump.inProgress && !player1Controls.Player.FirePlatform.inProgress
             && !player1Controls.Player.Run.inProgress && !player1Controls.Player.Dance.inProgress) { playerRB.velocity.Set(0, 0, 0); playerRB.useGravity = false; }

        //Add downforce to RB when player Y velocity is above threshold and in falling state.

        //if (playerRB.velocity.y >= velocityThreshhold && isFallingIdle)

        //{
        //    playerRB.AddForce(fallSpeed * gravity * Time.deltaTime * Vector3.down);
        //}

        //Move transform down when in falling state.

        if (isFallingIdle)
        {
            transform.Translate(fallSpeed * gravity * Time.deltaTime * Vector3.down);
        }

        else if (isWalking) { Walk(); }
        else if (isSprinting) { Run(); }
        else if (isJumping ) { Jump(); } 
        else if (isDancing) { Dance(); }
    }

    //UPDATE
    void Update()
    {
        HandleMovement();
        HandleRotation();

        if (isJumping)
        {
            Toolbox.instance.m_playerManager.boost -= boostJumpCost;
        }
       

        if (m_floorCollider.isStanding) //IF FLOOR COLLIDER OBJECT IS COLLIDING WITH OBJECT TAGGED "Floor"...
        {
            
            if (!landingSoundPlayed)
            {
                Toolbox.instance.m_audio.landing.PlayOneShot(Toolbox.instance.m_audio.audioClip1);
                landingSoundPlayed = true;
            }
            
            if (isFallingIdle) { StopFallingIdle(); }
            if (isJumping) { StopJumping(); }
            Debug.Log("isStanding = true;");
            //jumpingAllowed = true;


            //WALKING
            if (player1Controls.Player.Move.triggered)
            {
                Walk();
                isWalking = true;
                isIdle = false;
                isFallingIdle = false;
                isJumping = false;
                isSprinting = false;
                isDancing = false;
                Debug.Log("Started Walking.");
            }
            //IDLE
            if (!player1Controls.Player.Move.inProgress && !player1Controls.Player.Look.inProgress && !isJumping && !isFallingIdle && !isDancing && !isSprinting)
            {
                Idle();
                isIdle = true;
                isWalking = false;
                isFallingIdle = false;
                isJumping = false;
                isSprinting = false;
                isDancing = false;
                Debug.Log("Started Idling.");
            }
            //JUMPING
            if (!isFallingIdle && !isJumping && player1Controls.Player.Jump.triggered /*&& jumpingAllowed*/)
            {
                Jump();
                isJumping = true;
                isFallingIdle = false;
                isIdle = false;
                isWalking = false;
                isSprinting = false;
                isDancing = false;
                Debug.Log("Jumped");
                landingSoundPlayed = false;

            }
            //DANCING
            if (player1Controls.Player.Dance.triggered)
            {
                Dance();
                isDancing = true;
                isJumping = false;
                isFallingIdle = false;
                isIdle = false;
                isWalking = false;
                isSprinting = false;
                Debug.Log("Started Dancing");
            }
            //RUNNING
            if (isWalking && player1Controls.Player.Run.triggered)
            {
                Run();
                isSprinting = true;
                isDancing = false;
                isJumping = false;
                isFallingIdle = false;
                isIdle = false;
                isWalking = false;
                Debug.Log("Started Running");
            }
            
        }
        //FALLING IDLE
        else if (!m_floorCollider.isStanding && !player1Controls.Player.Jump.inProgress)
        {
            FallingIdle();
            isFallingIdle = true;
            isIdle = false;
            isWalking = false;
            isJumping = false;
            isSprinting = false;
            isDancing = false;
            Debug.Log("Started Falling Idle.");
            landingSoundPlayed = false;
        }       
    }
    void OnEnable()
    {
        player1Controls.Player.Enable();

    }
    void OnDisable()
    {
        player1Controls.Player.Disable();
    }
}

