using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Cooldown : MonoBehaviour
{

  public int timeToWait;
  private TextMeshPro text;

  private float currCountdownValue;

  private IEnumerator coroutine;

  public IEnumerator Countdown(float countdownValue)
  {
    //   EventParam param = new EventParam();
      text.color = new Color32(255, 255, 255, 255);
      currCountdownValue = countdownValue;
      while (currCountdownValue >= 0)
      {
          text.SetText("{0}", currCountdownValue);
          yield return new WaitForSeconds(1.0f);
          currCountdownValue--;
          if(currCountdownValue == 0) {
            // EventManager.TriggerEvent ("CooldownFinished", param);
          }
      }
  }

  public void StartCooldown(float time) {
    StopCoroutine(coroutine);
    currCountdownValue = time;
    StartCoroutine(coroutine);
  }


  void Start()
  {
    text = gameObject.GetComponent<TextMeshPro>();
    text.color = new Color32(255, 255, 255, 0);
    coroutine = Countdown(timeToWait);
  }

  void Update()
  {
  }

}
