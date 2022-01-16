using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D player;
    Animator animator;
    [SerializeField] Transform groundCheckCollider;
    [SerializeField] LayerMask groundLayer;

    public const float groundCheckRadiuss = 0.2f;
    public float speed;
    public float jumpPower;
    float hValue;

    bool isGrounded = false;
    bool jumping = false;
    bool facingRight = true;


    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        hValue = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            jumping = true;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            animator.SetBool("jumping", true);
            jumping = false;
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GroundCheck();
        Move(hValue, jumping);
    }


    void GroundCheck()
    {
        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadiuss, groundLayer);
        if (colliders.Length > 0)
        {
            isGrounded = true;
            animator.SetBool("jumping", !isGrounded);
        }
    }


    void Move(float horizontalValue, bool isJumping)
    {
        if (isGrounded && isJumping)
        {
            player.AddForce(new Vector2(0f, jumpPower));
            isGrounded = false;
            isJumping = false;
        }


        #region Horizontal movement

        float moveBy = horizontalValue * speed * 100 * Time.fixedDeltaTime;
        player.velocity = new Vector2(moveBy, player.velocity.y);

        if (facingRight && horizontalValue < 0) 
        {
            transform.localScale = new Vector3(-1, 1, 1);
            facingRight = false;
        }
        else if (!facingRight && horizontalValue > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
        }

        animator.SetFloat("xVelocity", Mathf.Abs(player.velocity.x));

        #endregion
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coins"))
        {
            Destroy(other.gameObject);
        }
    }
}
