using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private Animator anim;
    private float horizontalInput;

    public float Speed;
    public float jumpForce;
    private bool isFacingRight = true;
    [SerializeField] bool isGrounded;
    [SerializeField] GameObject GroundCheck;
    [SerializeField] LayerMask groundLayer; //Para almacenar una máscara que tenemos en Unity.
  
    



    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.transform.position, 0.1f, groundLayer); //Posición en la que se va a dibujar el círculo.
        Movement();
        Jump();


    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        playerRb.velocity = new Vector2 (horizontalInput * Speed,playerRb.velocity.y);
        
                        
        if (horizontalInput > 0)
        {
            anim.SetBool("IsRunning", true);
            if (!isFacingRight)
            {
                Flip();
            }
        }
        if(horizontalInput < 0)
        {
            anim.SetBool("IsRunning", true);
            if (isFacingRight)
            {
                Flip();
            }
        }
        if(horizontalInput == 0)
        {
            anim.SetBool("IsRunning", false);
        }
    }

    void Jump()
    {
        anim.SetBool("IsJumping", !isGrounded);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        }


    }

    void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
        isFacingRight = !isFacingRight;
    }

}
