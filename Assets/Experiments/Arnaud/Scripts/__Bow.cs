
using UnityEngine;
using UnityEngine.InputSystem;

public interface IBowAction
{
    public void OnThrow(InputValue value);
}

public class BowActionHandler: MonoBehaviour, IBowAction
{
    // Direction


    // Stock management
    private ArrowsStock arrowStock;

    public void AddRefToStock(ArrowsStock s)
    {
        arrowStock = s;
    }

    

    public void OnThrow(InputValue value)
    {
        print("Onthrow" + value.Get<float>());

        // Spaw an arrow, if there is still one
        if (arrowStock.IsEmpty())
        {
            print("Cant throw anymore");
            
        } else
        {

            print("Should throw something");
            arrowStock.Decrease();
        }
    }

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


public class __Bow : MonoBehaviour
{
    public int maxArrows = 3;

    private ArrowsStock stock;

    private BowActionHandler bah;

    void Start()
    {
        
        // Initialize stock of arrows
        stock = new ArrowsStock(maxArrows);
        // Initialize action handler
        bah = gameObject.AddComponent<BowActionHandler>();
        bah.AddRefToStock(stock);
    }

    void Update()
    {
        
        
    }
}
