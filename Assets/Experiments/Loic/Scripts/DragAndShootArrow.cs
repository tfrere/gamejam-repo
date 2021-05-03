using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndShootArrow : MonoBehaviour
{
    public float power = 2f;
    public Rigidbody2D rb;
    public GameObject Arrow;

    public GameObject player;

    public Vector2 minPower;
    public Vector2 maxPower;

    TrajectoryLineArrow tl;

    Camera cam;
    Vector2 force;
    Vector3 startPoint;
    Vector3 endPoint;

    private void Start()
    {
        cam = Camera.main;
        tl = GetComponent<TrajectoryLineArrow>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {                   
            startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            startPoint.z = 15;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 currentPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            currentPoint.z = 15;
            tl.RenderLine(startPoint, currentPoint);
        }

        if (Input.GetMouseButtonUp(0))
        {
            endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            endPoint.z = 15;
            GameObject newArrow = Instantiate(Arrow, player.transform.position, new Quaternion(0,0,0.707106829f,0.707106829f));            
            force = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x), Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y));
            newArrow.GetComponent<Rigidbody2D>().AddForce(force * power, ForceMode2D.Impulse);
            tl.EndLine();
        }
    }
}
