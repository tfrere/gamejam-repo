using UnityEngine;

public class Example : MonoBehaviour
{
    public Texture2D tex;

    private Rigidbody2D rb2D;
    private Sprite mySprite;
    private float thrust = 1f;

    void Awake()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
        transform.position = new Vector3(0.0f, -2.0f, 0.0f);
    }

    void FixedUpdate()
    {
        //rb2D.AddForce(transform.up * thrust);
        //// Alternatively, specify the force mode, which is ForceMode2D.Force by default
        //rb2D.AddForce(transform.up * thrust, ForceMode2D.Impulse);

        //AddForce();
        //AddTorqueImpulse(30.0f);
        //MovePosition();
        //MoveRotation();
        UseVelocity();

    }

    public void MovePosition()
    {
        Vector2 velocity = new Vector2(2f, 5f);
        rb2D.MovePosition(rb2D.position + velocity * Time.fixedDeltaTime);
    }

    public void MoveRotation()
    {
        float revSpeed = 50.0f;
        rb2D.MoveRotation(rb2D.rotation + revSpeed * Time.fixedDeltaTime);
    }
    public void AddForce()
    {
        //rb2D.AddForce(transform.up * thrust);
        //// Alternatively, specify the force mode, which is ForceMode2D.Force by default
        rb2D.AddForce(transform.up * thrust, ForceMode2D.Impulse);


    }

    // Add an impulse which produces a change in angular velocity (specified in degrees).
    public void AddTorqueImpulse(float angularChangeInDegrees)
    {
        var body = GetComponent<Rigidbody2D>();
        var impulse = (angularChangeInDegrees * Mathf.Deg2Rad) * body.inertia;

        body.AddTorque(impulse, ForceMode2D.Impulse);
    }


    private float t = 0.0f;
    private bool moving = false;


    public void UseVelocity()
    {

        var rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        //Press the Up arrow key to move the RigidBody upwards
        if (Input.GetKey(KeyCode.UpArrow))
        {

            rb.velocity = new Vector2(0.0f, 2.0f);
            moving = true;
            t = 0.0f;
        }

        //Press the Down arrow key to move the RigidBody downwards
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.velocity = new Vector2(0.0f, -1.0f);
            moving = true;
            t = 0.0f;
        }

        if (moving)
        {
            // Record the time spent moving up or down.
            // When this is 1sec then display info
            t = t + Time.deltaTime;
            if (t > 1.0f)
            {
                Debug.Log(gameObject.transform.position.y + " : " + t);
                t = 0.0f;
            }
        }
    }
}
