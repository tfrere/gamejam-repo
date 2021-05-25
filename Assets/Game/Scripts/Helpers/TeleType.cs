using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


// To activate the effect at the right time, just enable or disable
// the script at the right time
public class TeleType : MonoBehaviour {

  public float intervalBetweenLetters;
  private string textToPrint;
  private TextMeshPro m_textMeshPro;

  public AudioSource TeletypeSound;

  private int counter = 0;

  IEnumerator StartCountdown() {
    while(counter <= textToPrint.Length) {
      TeletypeSound.Play();
      m_textMeshPro.text = textToPrint.Substring(0, counter);
      m_textMeshPro.ForceMeshUpdate();
      counter += 1;

      yield return new WaitForSeconds(intervalBetweenLetters);
    }
  }

  void Start()
  {
    m_textMeshPro = gameObject.GetComponent<TextMeshPro>();
    textToPrint = m_textMeshPro.text;
    StartCoroutine(StartCountdown());
  }

  public void Interruption() {
    counter = textToPrint.Length;
    TeletypeSound.Play();
    m_textMeshPro.text = textToPrint.Substring(0, counter);
    m_textMeshPro.ForceMeshUpdate();
  }

}
