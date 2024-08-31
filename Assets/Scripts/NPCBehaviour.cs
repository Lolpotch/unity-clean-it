using System.Collections;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour
{
    [SerializeField] Sprite[] sprites = null;

    [SerializeField] GameObject lovePrefab = null;
    [SerializeField] Transform gameSprite = null;
    [SerializeField] GameObject dialogBox = null;
    [SerializeField] GameObject npcDialog = null;
    [SerializeField] GameObject virusPrefab = null;
    [SerializeField] float sneezeRate = 1f;

    [SerializeField] float radius = 0f;
    [SerializeField] LayerMask whatIsPlayer;

    bool masked = false;
    bool playerDetected = false;
    bool facingRight = true;
    float sneezeTime = 0f;

    Animator animator;
    Sound sneezeSound;

    void Awake()
    {
        animator = GetComponent<Animator>();
        sneezeSound = FindObjectOfType<AudioManager>().GetClip("Sneeze");
    }

    void Update()
    {
        Collider2D detect = Physics2D.OverlapCircle(transform.position, radius, whatIsPlayer);
        playerDetected = detect;
        if(playerDetected)
        {
            LookAtPlayer(detect.transform);
            if(!masked && Time.time >= sneezeTime)
            {
                sneezeTime = Time.time + sneezeRate;

                StartCoroutine(Sneeze());
            }
        }
    }

    void LookAtPlayer(Transform player)
    {
        if(player.position.x < transform.position.x && facingRight)
        {
            Flip();
        }
        else if(player.position.x > transform.position.x && !facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 scaler = gameSprite.localScale;
        scaler.x *= -1f;
        gameSprite.localScale = scaler;
    }

    IEnumerator Sneeze()
    {
        animator.SetBool("IsSneeze", true);
        yield return new WaitForSeconds(1.5f);

        if(!masked && !GameManager.gameFinished)
        {
            animator.SetBool("IsSneeze", false);
            sneezeSound.Play();
            Instantiate(virusPrefab, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !masked)
        {
            dialogBox.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !masked)
        {
            dialogBox.SetActive(false);
        }
    }

    public void GivenMask()
    {
        CounterManager counterManager = FindObjectOfType<CounterManager>();

        if (!masked && counterManager.maskAmount > 0)
        {
            //index 0 for false, 1 for true
            masked = true;
            dialogBox.SetActive(false);
            animator.SetBool("IsSneeze", false);
            animator.SetBool("IsMasked", true);

            FindObjectOfType<CounterManager>().AddGivenMask(1);

            FindObjectOfType<AudioManager>().GetClip("Mask").Play();

            counterManager.AddMask(-1);
            gameSprite.GetComponent<SpriteRenderer>().sprite = sprites[1];
            Instantiate(lovePrefab, transform);
        }
        else if(!masked && counterManager.maskAmount == 0)
        {
            Vector3 dialogPos = transform.position + Vector3.forward * -.1f;
            Instantiate(npcDialog, dialogPos, Quaternion.identity);
            print("NPC says: No mask left huh? (in pixel text)");
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
