using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour {

	public bool _haveToBlink = true;

	private Renderer renderer;

	void Start () {
		renderer = GetComponent<Renderer>();
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
