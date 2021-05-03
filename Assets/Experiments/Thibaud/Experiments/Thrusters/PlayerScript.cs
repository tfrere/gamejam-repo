 using UnityEngine;
 using System.Collections;
 
 public class PlayerScript : MonoBehaviour
 {
     public float m_speed = 20f;

     public Rigidbody2D thruster_left;
     public Rigidbody2D thruster_right;
     public GameObject thruster_left_square;
     public GameObject thruster_right_square;

     void Start()
     {
        //  thruster_left = GetComponent<Rigidbody2D>();
        //  thruster_right = GetComponent<Rigidbody2D>();
         print("player start");
     }
 
    // Update is called once per frame
    void Update()
    {
        thruster_left_square.transform.position = thruster_left.transform.position;
        thruster_right_square.transform.position = thruster_right.transform.position;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            print("down");
            thruster_left.velocity = new Vector2(0, m_speed);
        }
        if(Input.GetKey(KeyCode.RightArrow)) {
            print("not down");
            thruster_right.velocity = new Vector2(0, m_speed);
        }
    }

    //  void FixedUpdate()
    //  {
    //     if (isMoving)
    //     {
    //         thruster_left.velocity = new Vector2(0, m_speed);
    //         print("up arrow key is held down");
    //     }
    //  }
 }