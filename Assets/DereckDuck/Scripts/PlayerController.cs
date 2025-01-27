using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private Animator anim;
    private float horizontalInput;

    public float Speed;
    public float jumpForce;
  


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Jump();


    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        playerRb.velocity = new Vector2 (horizontalInput * Speed,playerRb.velocity.y);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        }
    }



}
