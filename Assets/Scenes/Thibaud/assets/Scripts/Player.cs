using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool isJumping = false;
    private Vector3 velocity = Vector3.zero;
 
    public float moveSpeed;
    public float jumpForce;
    public bool isGrounded = false;
    public Rigidbody2D rigidbody;
    public Transform groundCheckLeft;
    public Transform groundCheckRight;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);
        if(Input.GetButtonDown("Jump") && isGrounded) {
            isJumping = true;
        }
        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        MovePlayer(horizontalMovement);
    }

    void MovePlayer(float _horizontalMovement) {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rigidbody.velocity.y);
        rigidbody.velocity = Vector3.SmoothDamp(rigidbody.velocity, targetVelocity, ref velocity, 0.05f);
        if(isJumping) {
            rigidbody.AddForce(new Vector2(.0f, jumpForce));
            isJumping = false;
        }
    }
}
