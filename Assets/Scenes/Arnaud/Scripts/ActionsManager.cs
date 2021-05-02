using UnityEngine;


[RequireComponent(typeof(GameObject))]
public class ActionsManager : MonoBehaviour
{


    public GameObject actionsAnchor;
    public GameObject actionPrefab;
    private GameObject actionPrefabInstance;

    //private GameObject position;
    private Rigidbody2D rbody;
    private float prevX;
    private float prevY;


    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        prevX = actionsAnchor.transform.position.x;
        prevY = actionsAnchor.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Reset actionPrefabInstance
        if (HasMoved())
        {
            ResetPrefabInstance(actionPrefabInstance);
        }

        // Get direction of current RigidBody (ie player)
        float direction = rbody.velocity.x > 0 ? 1 : -1;

        // Spawn actionPrefab on Fire1 (ie 'click')
        if (Input.GetButtonDown("Fire1"))
        {
            ResetPrefabInstance(actionPrefabInstance);
            actionPrefabInstance = GameObject.Instantiate(actionPrefab);
            actionPrefabInstance.transform.position = new Vector2(actionsAnchor.transform.position.x + direction * 2, actionsAnchor.transform.position.y);
        }

        // Update prevX and prevY to match new current position on next render
        prevX = actionsAnchor.transform.position.x;
        prevY = actionsAnchor.transform.position.y;
    }

    bool HasMoved()
    {
        float tolerance = 0.03f;
        return GetDiffX() > tolerance || GetDiffY() > tolerance;
    }

    float GetDiffX()
    {
        return Mathf.Abs(prevX - actionsAnchor.transform.position.x);
    }

    float GetDiffY()
    {
        return Mathf.Abs(prevY - actionsAnchor.transform.position.y);
    }

    void ResetPrefabInstance(GameObject instance)
    {
        if (instance)
        {
            DestroyImmediate(instance, true);
        }
    }
}
