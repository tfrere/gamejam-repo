using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    public float LifeTime;
    float startTime;
    SpriteRenderer sprite;
    SpriteMask mask;
    Color color;
    bool dying;
    // Start is called before the first frame update
    void Start()
    {
        mask = GetComponent<SpriteMask>();
        sprite = GetComponent<SpriteRenderer>();
        if (sprite)
            color = sprite.color;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime > LifeTime && !dying)
        {
            dying = true;
            StartCoroutine(Fade());
        }
    }
    IEnumerator Fade()
    {
        float value = 1;
        while (value > 0)
        {
            value -= Time.deltaTime * 0.05f;
            if (sprite)
                sprite.color = new Color(color.r, color.g, color.b, value);
            transform.localScale = new Vector3(value, value, value);
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }
}