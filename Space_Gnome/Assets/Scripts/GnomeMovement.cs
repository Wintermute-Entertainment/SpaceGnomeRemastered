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

    //Dancing anim variables:
    public bool isDanceReady;

    public bool isBreakDance1990;
    public bool isHeadspin;
    public bool isUprock;
    public bool isRockDancing;

    public bool isFlair;
    public bool isHipHopDancing;
    public bool isSillyDancing;
    public bool isBreakDanceEnding1;

    private bool hasTransitionedToDanceReady = false;

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

    //Combos
    public int comboCount;

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

        //DANCE CONTROLS:

        player1Controls.Player.DanceReady.performed += ctx => DanceReady();
        player1Controls.Player.BreakDance1990.performed += ctx => BreakDance1990();
        player1Controls.Player.Headspin.performed += ctx => Headspin();
        player1Controls.Player.Uprock.performed += ctx => Uprock();
        player1Controls.Player.RockDancing.performed += ctx => RockDancing();

        player1Controls.Player.Flair.performed += ctx => Flair();
        player1Controls.Player.HipHopDancing.performed += ctx => HipHopDancing();
        player1Controls.Player.SillyDancing.performed += ctx => SillyDancing();
        player1Controls.Player.BreakDanceEnding1.performed += ctx => BreakDanceEnding1();

        isFallingIdle = true;
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

        //DANCING

        StopBreakDance1990();
        StopHeadspin();
        StopUprock();
        StopRockDancing();
        StopDanceReady();

        StopFlair();
        StopHipHopDancing();
        StopSillyDancing();
        StopBreakDanceEnding1();

        //player1Animator.ResetTrigger("Flair");
        //player1Animator.ResetTrigger("HipHopDancing");
        //player1Animator.ResetTrigger("SillyDancing");
        //player1Animator.ResetTrigger("BreakDanceEnding1");

        hasTransitionedToDanceReady= false;
    }

    // ANIMATION STATES:

    void DanceReady()
    {
        ResetStates();
        if (!hasTransitionedToDanceReady) { hasTransitionedToDanceReady= true; }
       // player1Animator.SetBool("isDanceReady", true); isDanceReady = true;
        Debug.Log("DanceReady() called.");

       
        
            if (player1Controls.Player.BreakDance1990.triggered)
            {
                
                if (player1Controls.Player.Flair.triggered) { Flair(); }
                else if (player1Controls.Player.HipHopDancing.triggered) { HipHopDancing(); }
                else if (player1Controls.Player.SillyDancing.triggered) { SillyDancing(); }
                else if (player1Controls.Player.BreakDanceEnding1.triggered) { BreakDanceEnding1(); }
                else { BreakDance1990(); }
        }
            else if (player1Controls.Player.Headspin.triggered)
            {
                
                if (player1Controls.Player.Flair.triggered) { Flair(); }
                else if (player1Controls.Player.HipHopDancing.triggered) { HipHopDancing(); }
                else if (player1Controls.Player.SillyDancing.triggered) { SillyDancing(); }
                else if (player1Controls.Player.BreakDanceEnding1.triggered) { BreakDanceEnding1(); }
                else { Headspin(); }
        }
            else if (player1Controls.Player.Uprock.triggered)
            {
                
                if (player1Controls.Player.Flair.triggered) { Flair(); }
                else if (player1Controls.Player.HipHopDancing.triggered) { HipHopDancing(); }
                else if (player1Controls.Player.SillyDancing.triggered) { SillyDancing(); }
                else if (player1Controls.Player.BreakDanceEnding1.triggered) { BreakDanceEnding1(); }
                else { Uprock(); }
        }
            else if (player1Controls.Player.RockDancing.triggered)
            {
                
                if (player1Controls.Player.Flair.triggered) { Flair(); }
                else if (player1Controls.Player.HipHopDancing.triggered) { HipHopDancing(); }
                else if (player1Controls.Player.SillyDancing.triggered) { SillyDancing(); }
                else if (player1Controls.Player.BreakDanceEnding1.triggered) { BreakDanceEnding1(); }
                else { RockDancing(); }
        }
            else if (player1Controls.Player.Flair.triggered) { Flair(); }
            else if (player1Controls.Player.HipHopDancing.triggered) { HipHopDancing(); }
            else if (player1Controls.Player.SillyDancing.triggered) { SillyDancing(); }
            else if (player1Controls.Player.BreakDanceEnding1.triggered) { BreakDanceEnding1(); }

            else if (isBreakDance1990) { BreakDance1990(); }
            else if (isHeadspin) { Headspin(); }
            else if (isUprock) { Uprock(); }
            else if (isRockDancing) { RockDancing(); }

            else if (isFlair) { Flair(); }
            else if (isHipHopDancing) { HipHopDancing(); }
            else if (isSillyDancing) { SillyDancing(); }
            else if (isBreakDanceEnding1) { BreakDanceEnding1(); }

            else if (isDancing) { Dance(); }
            else if (!isDanceReady) { player1Animator.SetBool("isDanceReady", true); isDanceReady = true; Debug.Log("Started DanceReady."); }
        
        else { FallingIdle(); }

    }
    void StopDanceReady()
    {
        player1Animator.SetBool("isDanceReady", false);
        isDanceReady = false;
    }
    void BreakDance1990()
    {
        ResetStates();

        player1Animator.SetBool("BreakDance1990", true);
        isBreakDance1990 = true;

    }
    void StopBreakDance1990()
    {
        player1Animator.SetBool("BreakDance1990", false);
        isBreakDance1990 = false;
    }
    void StopHeadspin()
    {
        player1Animator.SetBool("Headspin", false);
        isHeadspin = false;
    }
    void StopUprock()
    {
        player1Animator.SetBool("Uprock", false);
        isUprock = false;
    }
    void StopRockDancing()
    {
        player1Animator.SetBool("RockDancing", false);
        isRockDancing = false;
    }
    void Headspin()
    {
        ResetStates();

        player1Animator.SetBool("Headspin", true);
        isHeadspin = true;

    }
    void Uprock()
    {
        ResetStates();

        player1Animator.SetBool("Uprock", true);
        isUprock = true;

    }
    void RockDancing()
    {

        ResetStates();
        player1Animator.SetBool("RockDancing", true);
        isRockDancing = true;
    }

    void Flair()
    {
        ResetStates();

        player1Animator.SetBool("isFlair", true);
        isFlair = true;
        Debug.Log("Flair triggered.");
    }
    void HipHopDancing()
    {
        ResetStates();

        player1Animator.SetBool("isHipHopDancing", true);
        isHipHopDancing = true;
        Debug.Log("HipHopDancing triggered.");
    }
    void SillyDancing()
    {
        ResetStates();

        player1Animator.SetBool("isSillyDancing", true);
        isSillyDancing = true;
        Debug.Log("SillyDancing triggered.");
    }
    void BreakDanceEnding1()
    {
        ResetStates();

        player1Animator.SetBool("isBreakDanceEnding1", true);
        isBreakDanceEnding1 = true;
        Debug.Log("BreakDanceEnding1 triggered.");
    }

    void StopFlair()
    {
        player1Animator.SetBool("isFlair", false);
        isFlair = false;
    }
    void StopHipHopDancing()
    {
        player1Animator.SetBool("isHipHopDancing", false);
        isHipHopDancing = false;
    }
    void StopSillyDancing()
    {
        player1Animator.SetBool("isSillyDancing", false);
        isSillyDancing = false;
    }
    void StopBreakDanceEnding1()
    {

        player1Animator.SetBool("isBreakDanceEnding1", false);
        isBreakDanceEnding1 = false;
    }
    public IEnumerator WaitOneSecond(float timeToWait)
    {
        // timeToWait = 1;
        yield return new WaitForSeconds(timeToWait);
    }
    public void Dance()
    {
        if (!isFallingIdle)
        {
            player1Animator.SetBool("isDancing", true);
            isDancing = true;
            Toolbox.instance.m_playerManager.boost += boostDanceBonus;
        }
        StartCoroutine(WaitOneSecond(1));

        if (player1Controls.Player.DanceReady.triggered) { DanceReady(); }

    }
    void FixedUpdate()
    {

        // SET VELOCITY TO 0 IF NO PLAYER INPUT

        if (!player1Controls.Player.Move.inProgress && !player1Controls.Player.Jump.inProgress && !player1Controls.Player.FirePlatform.inProgress
             && !player1Controls.Player.Run.inProgress && !player1Controls.Player.Dance.inProgress && !player1Controls.Player.DanceReady.inProgress
             && !player1Controls.Player.BreakDance1990.inProgress && !player1Controls.Player.Headspin.inProgress && !player1Controls.Player.Uprock.inProgress
             && !player1Controls.Player.RockDancing.inProgress && !player1Controls.Player.Flair.inProgress && !player1Controls.Player.HipHopDancing.inProgress
             && !player1Controls.Player.SillyDancing.inProgress && !player1Controls.Player.BreakDanceEnding1.inProgress) { playerRB.velocity.Set(0, 0, 0); playerRB.useGravity = false; }

    }
    void StopJumping()
    {
        player1Animator.SetBool("isJumping", false);
        isJumping = false;
    }
    //UPDATE
    void Update()
    {

        HandleMovement();
        HandleRotation();
        //Move transform down when in falling state.

        if (isFallingIdle)
        {
            transform.Translate(fallSpeed * gravity * Time.deltaTime * Vector3.down);
        }

        else if (isWalking) { Walk(); }
        else if (isSprinting) { Run(); }
        //else if (isJumping) { Jump(); }
       else if (isStanding) { ResetStates(); }

        //DANCE MOVE PREREQUISITE STATES
        else if (player1Controls.Player.Dance.triggered) { Dance(); }
        else if (player1Controls.Player.DanceReady.triggered) { DanceReady(); }
        else if (player1Controls.Player.BreakDance1990.triggered)
        {

            if (player1Controls.Player.Flair.triggered) { ResetStates(); Flair(); }
            else if (player1Controls.Player.HipHopDancing.triggered) { ResetStates(); HipHopDancing(); }
            else if (player1Controls.Player.SillyDancing.triggered) { ResetStates(); SillyDancing(); }
            else if (player1Controls.Player.BreakDanceEnding1.triggered) { ResetStates(); BreakDanceEnding1(); }
            else { BreakDance1990(); }
        }
        else if (player1Controls.Player.Headspin.triggered)
        {

            if (player1Controls.Player.Flair.triggered) { ResetStates(); Flair(); }
            else if (player1Controls.Player.HipHopDancing.triggered) { ResetStates(); HipHopDancing(); }
            else if (player1Controls.Player.SillyDancing.triggered) { ResetStates(); SillyDancing(); }
            else if (player1Controls.Player.BreakDanceEnding1.triggered) { ResetStates(); BreakDanceEnding1(); }
            else { Headspin(); }

        }
        else if (player1Controls.Player.Uprock.triggered)
        {

            if (player1Controls.Player.Flair.triggered) { ResetStates(); Flair(); }
            else if (player1Controls.Player.HipHopDancing.triggered) { ResetStates(); HipHopDancing(); }
            else if (player1Controls.Player.SillyDancing.triggered) { ResetStates(); SillyDancing(); }
            else if (player1Controls.Player.BreakDanceEnding1.triggered) { ResetStates(); BreakDanceEnding1(); }
            else { Uprock(); }
        }
        else if (player1Controls.Player.RockDancing.triggered)
        {
            if (player1Controls.Player.Flair.triggered) { ResetStates(); Flair(); }
            else if (player1Controls.Player.HipHopDancing.triggered) { ResetStates(); HipHopDancing(); }
            else if (player1Controls.Player.SillyDancing.triggered) { ResetStates(); SillyDancing(); }
            else if (player1Controls.Player.BreakDanceEnding1.triggered) { ResetStates(); BreakDanceEnding1(); }
            else { RockDancing(); }

        }
        
        //LOOPING STATES

        else if (isFlair) { Flair(); }
        else if (isHipHopDancing) { HipHopDancing(); }
        else if (isSillyDancing) { SillyDancing(); }
        else if (isBreakDanceEnding1) { BreakDanceEnding1(); }

        else if (isBreakDance1990) { BreakDanceEnding1(); }
        else if (isHeadspin) { Headspin(); }
        else if (isUprock) { Uprock(); }
        else if (isRockDancing) { RockDancing(); }

        else if (isDancing) { Dance(); }
        else if (isDanceReady) { DanceReady(); }
        
        

        if (player1Controls.Player.BreakDance1990.triggered)
        {
            //StopBreakDancing();
            BreakDance1990();

            Toolbox.instance.m_playerManager.boost += boostDanceBonus;

            //isDanceReady = false;
            //isFallingIdle = false;
            //isIdle = false;
            //isWalking = false;
            //isSprinting = false;
            //isDancing = false;
            //isJumping = false;
            //isHeadspin = false;
            //isUprock = false;
            //isRockDancing = false;
            Debug.Log("BreakDance1990 triggered.");

            if (comboCount == 0) { comboCount = 1; }
            else if (comboCount == 1) { comboCount = 2; }
            else if (comboCount == 2) { comboCount = 3; }
            else if (comboCount == 3) { comboCount = 4; }
            else if (comboCount == 4) { comboCount = 5; }
            else if (comboCount == 5) { return; }
            Debug.Log("Combo count is " + comboCount + ".");
        }
        if (player1Controls.Player.Headspin.triggered)
        {
            //StopBreakDancing();
            Headspin();

            Toolbox.instance.m_playerManager.boost += boostDanceBonus;

            //isBreakDance1990 = false;
            //isDanceReady = false;
            //isFallingIdle = false;
            //isIdle = false;
            //isWalking = false;
            //isSprinting = false;
            //isDancing = false;
            //isJumping = false;
            //isUprock = false;
            //isRockDancing = false;
            Debug.Log("Headspin triggered.");

            if (comboCount == 0) { comboCount = 1; }
            else if (comboCount == 1) { comboCount = 2; }
            else if (comboCount == 2) { comboCount = 3; }
            else if (comboCount == 3) { comboCount = 4; }
            else if (comboCount == 4) { comboCount = 5; }
            else if (comboCount == 5) { return; }
            Debug.Log("Combo count is " + comboCount + ".");
        }
        if (player1Controls.Player.Uprock.triggered)
        {
            //StopBreakDancing();
            Uprock();

            Toolbox.instance.m_playerManager.boost += boostDanceBonus;

            //isBreakDance1990 = false;
            //isDanceReady = false;
            //isFallingIdle = false;
            //isIdle = false;
            //isWalking = false;
            //isSprinting = false;
            //isDancing = false;
            //isJumping = false;
            //isHeadspin = false;
            //isRockDancing = false;
            Debug.Log("UpRock triggered.");

            if (comboCount == 0) { comboCount = 1; }
            else if (comboCount == 1) { comboCount = 2; }
            else if (comboCount == 2) { comboCount = 3; }
            else if (comboCount == 3) { comboCount = 4; }
            else if (comboCount == 4) { comboCount = 5; }
            else if (comboCount == 5) { return; }
            Debug.Log("Combo count is " + comboCount + ".");
        }
        if (player1Controls.Player.RockDancing.triggered)
        {
            // StopBreakDancing();
            RockDancing();
            Toolbox.instance.m_playerManager.boost += boostDanceBonus;
            //isBreakDance1990 = false;
            //isDanceReady = false;
            //isFallingIdle = false;
            //isIdle = false;
            //isWalking = false;
            //isSprinting = false;
            //isDancing = false;
            //isJumping = false;
            //isHeadspin = false;
            //isUprock = false;
            Debug.Log("RockDancing triggered.");

            if (comboCount == 0) { comboCount = 1; }
            else if (comboCount == 1) { comboCount = 2; }
            else if (comboCount == 2) { comboCount = 3; }
            else if (comboCount == 3) { comboCount = 4; }
            else if (comboCount == 4) { return; }
            Debug.Log("Combo count is " + comboCount + ".");
        }
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
            if (
                
            !player1Controls.Player.Move.inProgress && !player1Controls.Player.Look.inProgress && !isJumping && !isFallingIdle && !isDancing && !isSprinting
             && !isDanceReady && !player1Controls.Player.BreakDance1990.inProgress && !player1Controls.Player.Headspin.inProgress && !player1Controls.Player.Uprock.inProgress
             && !player1Controls.Player.RockDancing.inProgress && !player1Controls.Player.Flair.inProgress && !player1Controls.Player.HipHopDancing.inProgress
             && !player1Controls.Player.SillyDancing.inProgress && !player1Controls.Player.BreakDanceEnding1.inProgress && !isFlair && !isHipHopDancing && !isSillyDancing
             && !isBreakDanceEnding1)
            {
                ResetStates();
                Idle();

                Debug.Log("Started Idling.");


            }
            //JUMPING
            if (!isFallingIdle && !isJumping && player1Controls.Player.Jump.triggered)
            {
                ResetStates();
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
                ResetStates();
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
                ResetStates();
                Run();
                isSprinting = true;
                isDancing = false;
                isJumping = false;
                isFallingIdle = false;
                isIdle = false;
                isWalking = false;
                Debug.Log("Started Running");
            }
            if (player1Controls.Player.DanceReady.triggered && player1Controls.Player.Dance.inProgress)
            {
                
                DanceReady();
                isDanceReady = true;
                isJumping = false;
                isFallingIdle = false;
                isIdle = false;
                isWalking = false;
                isDancing = false;
                isRockDancing = false;
                isBreakDance1990 = false;
                isHeadspin = false;
                isUprock = false;
            }

        }
        //FALLING IDLE
        else if (!m_floorCollider.isStanding && !player1Controls.Player.Jump.inProgress)
        {
            ResetStates();
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
    void Jump()
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
    //Need to change this to subtract boost only once at beginning of jump, right now works for duration of button press I think.



    void OnEnable()
    {
        player1Controls.Player.Enable();

    }
    void OnDisable()
    {
        player1Controls.Player.Disable();
    }
}
    


