using UnityEngine;

public class BaseMovement : MonoBehaviour
{
  private float m_speed = 10f;
  private int numberOfJumpsBeforeTouchingGround = 0;
  private Rigidbody2D rbody;
  public string leftInput;
  public string rightInput;
  public string topInput;
  public string bottomInput;

  void Start()
  {
      rbody = GetComponent<Rigidbody2D>();
      print("player start");
  }

 void OnCollisionEnter2D(Collision2D col) {
     print("toto");
     numberOfJumpsBeforeTouchingGround = 0;
 }

 // Update is called once per frame
 void Update()
 {
     if (Input.GetKey(leftInput))
     {
         print("left");
         rbody.velocity = new Vector2(-m_speed, 0);
     }
     if(Input.GetKey(rightInput)) {
         print("right");
         rbody.velocity = new Vector2(m_speed, 0);
     }
     if (Input.GetKey(topInput) && numberOfJumpsBeforeTouchingGround < 2)
     {
         print("up");
         numberOfJumpsBeforeTouchingGround++;
         rbody.velocity = new Vector2(0, m_speed);
     }
     if(Input.GetKey(bottomInput)) {
         print("down");
         rbody.velocity = new Vector2(0, -m_speed);
     }
 }
}
