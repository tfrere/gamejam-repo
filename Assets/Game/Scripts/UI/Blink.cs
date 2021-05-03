using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour {

	public bool _haveToBlink = true;

	void Start () {
		InvokeRepeating("BlinkText", 0.0f, .5f);
	}

	void Update() {

	}

  void BlinkText() {
	 if(_haveToBlink) {
		 gameObject.SetActive(!gameObject.activeSelf);
	 }
 }

}
