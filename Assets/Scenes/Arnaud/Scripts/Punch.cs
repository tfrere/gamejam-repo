using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(GameObject))]
public class Punch : MonoBehaviour
{

    public GameObject punchAnchor;
    private Rigidbody2D rbody;

    private GameObject punch;
    private float prevX;
    private float prevY;


    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        prevX = punchAnchor.transform.position.x;
        prevY = punchAnchor.transform.position.y;


    }

    // Update is called once per frame
    void Update()
    {

        // Reset punch
        // Add math.abs and 0.5f ceil
        if (HasMoved() && punch)
        {
            DestroyImmediate(punch);
        }



        float direction = rbody.velocity.x > 0 ? 1 : -1 ;

        if (Input.GetButtonDown("Fire1"))
        {
            if (punch)
            {
                DestroyImmediate(punch);
            }
            punch = GetPunch();
            Vector2 punchPosition = new Vector2(punchAnchor.transform.position.x + direction * 2, punchAnchor.transform.position.y);
            punch.transform.position = punchPosition;
            punch.transform.rotation = punchAnchor.transform.rotation;
            Destroy(punch, 3);
        }


        // Update prevX and prevY to match new current position on next render
        prevX = punchAnchor.transform.position.x;
        prevY = punchAnchor.transform.position.y;



    }

    GameObject GetPunch()
    {
        GameObject punch = GameObject.CreatePrimitive(PrimitiveType.Cube);
        DestroyImmediate(punch.GetComponent<BoxCollider>());
        punch.AddComponent<BoxCollider2D>();
        return punch;
    }


    bool HasMoved()
    {
        // To fix tolerance
        float tolerance = 0.5f;
        return GetDiffX() > tolerance || GetDiffY() > tolerance;
    }

    float GetDiffX()
    {
        return Mathf.Abs(prevX - punchAnchor.transform.position.x);
    }

    float GetDiffY()
    {
        return Mathf.Abs(prevY - punchAnchor.transform.position.y);
    }
}
