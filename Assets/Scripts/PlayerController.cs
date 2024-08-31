using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Basic Movements\
    float x;
    [SerializeField] float speed = 0f;
    [SerializeField] float jumpForce = 0f;

    //Detect ground
    bool isGrounded;
    [SerializeField] Transform feet;
    [SerializeField] LayerMask whatIsGround;

    //Flip Player
    [HideInInspector] public bool isFacingRight = true;

    //Hurt Player
    [SerializeField] Vector3 forceDirection = Vector3.zero;
    bool canBeControlled = true;
    bool controlledOverGround = true;
    bool canFlip = true;

    //Shooting stuffs
    [SerializeField] Transform gunTip;
    [SerializeField] GameObject spray;
    SprayBar sprayBar;

    //Joystick
    [SerializeField] FixedJoystick joystick;

    //Animation parameter
    bool isRunning = false;
    bool setTo = false;

    Animator anim;
    Rigidbody2D rb;
    Sound spraySound;
    Sound virusHitSound;
    Sound jumpSound;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprayBar = FindObjectOfType<SprayBar>();

        AudioManager audioManager = FindObjectOfType<AudioManager>();
        spraySound = audioManager.GetClip("Spray");
        virusHitSound = audioManager.GetClip("Virus Hit");
        jumpSound = audioManager.GetClip("Jump");
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            Spray();
        }

        if (Input.GetButtonDown("Jump"))
        {
            PlayerJump();
        }
        if(canFlip)
        {
            FlipTrigger();
        }
        DetectGround();

        TriggerAnimation();
    }

    private void FixedUpdate()
    {
        if(canBeControlled)
        {
            PlayerMovement();
        }
    }

    void PlayerMovement()
    {
        //x = Input.GetAxis("Horizontal");
        x = joystick.Horizontal;
        rb.velocity = new Vector3(x * speed, rb.velocity.y, 0f);
    }

    public void PlayerJump()
    {
        if(isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0f);

            jumpSound.Play();
        }
    }

    void DetectGround()
    {
        isGrounded = Physics2D.OverlapCircle(feet.position, .1f, whatIsGround);

        if(controlledOverGround)
        {
            canBeControlled = isGrounded;
            if (isGrounded && rb.velocity.x < .4f && rb.velocity.x > -.4f)
            {
                controlledOverGround = false;
                canFlip = true;
            }
        }
    }

    void FlipTrigger()
    {
        if(rb.velocity.x < 0f && isFacingRight)
        {
            FlipPlayer();
        }
        else if(rb.velocity.x > 0f && !isFacingRight)
        {
            FlipPlayer();
        }
    }

    void FlipPlayer()
    {
        isFacingRight = !isFacingRight;

        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    public void PlayerHit(bool fromRight)
    {
        StartCoroutine(PlayerHurtEffect(fromRight));
    }

    IEnumerator PlayerHurtEffect(bool fromRight)
    {
        //Add force to the player when get hit
        Vector3 dir = forceDirection;
        if(fromRight)
        {
            dir.x *= -1;
        }
        rb.velocity = dir;

        anim.Play("Player_hurt");

        virusHitSound.Play();

        canBeControlled = false;
        controlledOverGround = false;
        canFlip = false;
        while (isGrounded) { yield return null; };
        controlledOverGround = true;
    }

    public void Spray()
    {
        if(sprayBar.isReady)
        {
            sprayBar.UseEnergy();
            spraySound.Play();
            GameObject a = Instantiate(spray, gunTip.position, Quaternion.identity);

            if(!isFacingRight)
            {
                Vector3 scaler = a.transform.localScale;
                scaler.x *= -1f;
                a.transform.localScale = scaler;
            }
            
        }
    }

    void TriggerAnimation()
    {
        if(x != 0f && !isRunning)
        {
            isRunning = true;
            anim.SetBool("IsRunning", true);
        }else if(x == 0f && isRunning)
        {
            isRunning = false;
            anim.SetBool("IsRunning", false);
        }

        if(!setTo && isGrounded)
        {
            setTo = true;
            anim.SetBool("CanBeControlled", true);
        }else if(setTo && !isGrounded)
        {
            setTo = false;
            anim.SetBool("CanBeControlled", false);
        }
    }
}
