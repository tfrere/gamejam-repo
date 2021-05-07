using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class MyPlayerScript : MonoBehaviour
{

    public Rigidbody2D rigidBody;

    public SpriteRenderer spriteRenderer;

    public float speed = 10f;

    private float inputX = 0f;

    void start () {
    }

    public void Punch(InputAction.CallbackContext context)
    {
        if(context.performed) {
            print("Punch!");
            spriteRenderer.color = Color.blue;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed) {
            print("Jump!");
            Vector2 value = new Vector2(inputX, 2f);
            rigidBody.AddForce(value, ForceMode2D.Impulse);
            spriteRenderer.color = Color.red;
        }
    }

    public void Throw(InputAction.CallbackContext context)
    {
        if(context.performed) {
            print("Throw!");
            spriteRenderer.color = Color.green;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        print("Move!");
        inputX = context.ReadValue<Vector2>().x;
    }

    void Update() {
        print("update" + inputX);
        Vector2 value = new Vector2(inputX, rigidBody.velocity.y);
        rigidBody.AddForce(value, ForceMode2D.Impulse);
        // rigidBody.velocity = value;
        // spriteRenderer.color = Color.white;
    }

}