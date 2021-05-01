using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


// To activate the effect at the right time, just enable or disable
// the script at the right time
public class TeleType : MonoBehaviour {

  public float intervalBetweenLetters;
  private TextMeshPro m_textMeshPro;

  IEnumerator StartCountdown() {

    int totalVisibleCharacters = m_textMeshPro.textInfo.characterCount;
    int counter = 0;

    while(counter <= totalVisibleCharacters) {

      //visibleCount = counter % (totalVisibleCharacters + 1);
      m_textMeshPro.maxVisibleCharacters = counter;

      counter += 1;
      yield return new WaitForSeconds(intervalBetweenLetters);
    }

  }

  void Start()
  {
    m_textMeshPro = gameObject.GetComponent<TextMeshPro>();
  }

  void OnEnable() {
    StartCoroutine(StartCountdown());
  }

  void Update()
  {
  }

}
