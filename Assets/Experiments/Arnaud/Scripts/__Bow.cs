
using UnityEngine;
using UnityEngine.InputSystem;

public interface IBowAction
{
    public void OnThrow(InputValue value);
}


public class ArrowsStock
{
    private int value = 0;
    public ArrowsStock(int v)
    {
        value = v;
    }
    public bool IsEmpty()
    {
        return value == 0;
    }
    public void Increase()
    {
        value++;
    }
    public void Decrease()
    {
        if (value > 0)
        {
            value--;
        }
    }

}


public class __Bow : MonoBehaviour, IBowAction
{

    public int maxArrows = 3;
    public float arrowSpeed = 2f;
    public GameObject Arrow;

    private Rigidbody2D rb2D;
    private ArrowsStock arrowStock;
    private Vector2 direction = Vector2.zero;

    private string ARROW_NAME_PREFIX = "ARROW";

    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        // Initialize stock of arrows
        arrowStock = new ArrowsStock(maxArrows);
    }

    void Update()
    {
        direction = rb2D.GetPointVelocity(rb2D.position);
        //print("Direction " + direction);

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.StartsWith(ARROW_NAME_PREFIX))
        {
            print("current velocity of arrow : " + collision.rigidbody.velocity.magnitude);
            print("RelativeVelocity - " + collision.relativeVelocity);
            arrowStock.Increase();
        }
    }





    public void OnThrow(InputValue value)
    {
        // Spaw an arrow, if there is still one
        if (arrowStock.IsEmpty())
        {
            print("Cant throw anymore");
        }
        else
        {
            bool isFacingLeft = direction.x < 0;

            float positionOffset = isFacingLeft ? -1 : 1f;


            Vector3 arrowVector = isFacingLeft ? Vector3.left : Vector3.right;
            Vector3 arrowPosition = new Vector3(this.transform.position.x + positionOffset, this.transform.position.y, this.transform.position.z);
            Quaternion quaternion = isFacingLeft ? Quaternion.AngleAxis(0, Vector3.back) : Quaternion.AngleAxis(0, Vector3.forward);


            GameObject newArrow = Instantiate(Arrow, arrowPosition, quaternion);
            newArrow.name = ARROW_NAME_PREFIX;
            newArrow.GetComponent<Rigidbody2D>().AddForce(arrowVector * arrowSpeed, ForceMode2D.Impulse);


            arrowStock.Decrease();
        }
    }
}
