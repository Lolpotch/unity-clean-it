using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{
    [SerializeField] Transform look = null;
    [SerializeField] Transform moveDirection = null;
    [SerializeField] GameObject virusParticle = null;
    [SerializeField] [Range(0f, 1f)] float ratio = 0f;
    [SerializeField] float speed = 0f;
    [SerializeField] float lifeTime = 0f;
    bool lifeDepleted = false;
    bool canAttack = true;

    Transform player;
    Rigidbody2D rb;
    Animator anim;

    float time = 0f;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        Vector3 dir = player.position - moveDirection.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        moveDirection.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void Update()
    {
        LookAt();
        smoothLook();
        CheckLifeTime();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void CheckLifeTime()
    {
        time += Time.deltaTime;
        if(time > lifeTime && !lifeDepleted)
        {
            canAttack = false;
            lifeDepleted = true;
            anim.SetBool("LifeDepleted", lifeDepleted);
        }
        //Play fade out animation after lifeDepleted is true
    }

    

    void LookAt()
    {
        Vector3 dir = player.position - look.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        look.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void smoothLook()
    {
        Quaternion rotSmooth = Quaternion.Lerp(moveDirection.rotation, look.rotation, ratio);
        moveDirection.rotation = rotSmooth;
    }

    void Move()
    {
        rb.velocity = moveDirection.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform == player && canAttack)
        {
            OnPlayerHit(collision);
        }
    }

    void OnPlayerHit(Collider2D collision)
    {
        bool fromRight = false;
        if (transform.position.x > collision.transform.position.x)
        {
            fromRight = true;
        }

        collision.GetComponent<PlayerController>().PlayerHit(fromRight);

        Instantiate(virusParticle, transform.position, Quaternion.identity);
        FindObjectOfType<Life>().AddLife(-1);
        DestroyItself();
    }

    //Animation Events
    public void DestroyItself()
    {
        Destroy(gameObject);
    }
}
