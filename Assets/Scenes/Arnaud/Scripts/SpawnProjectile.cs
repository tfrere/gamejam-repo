using UnityEngine;

public class SpawnProjectile : MonoBehaviour
{

    public Rigidbody2D projectile;
    public float speed = 0;
    private Rigidbody2D bulletRB;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            this.bulletRB = Instantiate(projectile, transform.position, transform.rotation);
            //bulletRB.AddForce(new Vector2(speed, 0), ForceMode2D.Impulse);
            this.bulletRB.velocity = new Vector2(speed, 0);
            Invoke("ClearBullet", 1.5f);
        }

    }

    void ClearBullet()
    {
        print("ClearBullet");
        Destroy(this.bulletRB);
    }

}
