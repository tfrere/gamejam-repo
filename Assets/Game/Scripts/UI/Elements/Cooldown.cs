using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Cooldown : MonoBehaviour
{

    public int timeToWait;
    private TextMeshPro text;
    private float currCountdownValue;
    private bool hasStarted = false;

    public IEnumerator Countdown(float timeToWait)
    {
        hasStarted = true;
        //   EventParam param = new EventParam();
        currCountdownValue = timeToWait;
        while (currCountdownValue >= 0)
        {
            text.SetText("{0}", currCountdownValue);
            GameEvents.current.UISoundTrigger("countdown");
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }
    }

    public void StartCooldown(float time)
    {
        if (!hasStarted)
        {
            StartCoroutine(Countdown(time));
        }
    }


    void Start()
    {
        text = gameObject.GetComponent<TextMeshPro>();
    }

    void Update()
    {
    }

}
