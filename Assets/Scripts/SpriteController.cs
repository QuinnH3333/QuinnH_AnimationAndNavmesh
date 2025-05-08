using UnityEngine;

public class SpriteController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private Animator animator;
    private bool isGrounded = true;
    [SerializeField] private Transform grounded;
    [SerializeField] private bool lookingLeft = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");


        if (isGrounded)
        {
            if (Input.GetButton("Horizontal"))
            {
                rb.AddForce(new Vector2(horizontal, 0) * moveSpeed, ForceMode2D.Force);
                animator.SetBool("isWalking", true);


                if (rb.linearVelocityX > 0 && lookingLeft)
                {
                    Flip();
                }
                else if (rb.linearVelocityX < 0 && !lookingLeft)
                {
                    Flip();
                }


            }
            else
            {
                animator.SetBool("isWalking", false);
                rb.linearVelocityX = 0;
            }

            if (Input.GetButtonDown("Jump") && Input.GetAxis("Jump") > 0)
            {
                animator.SetBool("isJumping", true);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

                isGrounded = false;
            }
            else if (Input.GetButton("Jump") && Input.GetAxis("Jump") < 0)
            {
                Debug.Log("Ducked");
                animator.SetBool("isDucking", true);
            }
            else
            {
                animator.SetBool("isDucking", false);
            }
        }


        if (!isGrounded && rb.linearVelocityY <= 0)
        {
            //Not sure why it gets stuck sometimes. 
            animator.SetBool("isFalling", true);
            RaycastHit2D hitInfo = Physics2D.Raycast(grounded.position, Vector2.down, .05f); //Stopped at lecture 19 at 2:08:00
            if (hitInfo.collider != null && hitInfo.transform.CompareTag("Ground"))
            {
                isGrounded = true;
                animator.SetBool("isJumping", false);
                animator.SetBool("isFalling", false);
            }
        } }
        private void Flip()
    {
        Vector2 flipLookDirection = transform.localScale;
        flipLookDirection.x = transform.localScale.x * -1;
        transform.localScale = flipLookDirection;
        lookingLeft = !lookingLeft;
    }



    }

