using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levitate : MonoBehaviour {
	// User Inputs
	public float degreesPerSecond = 15.0f;
	public float amplitude = 10.0f;
	public float frequency = 0.7f;
	public int _scaleMultiplier = 10;
	public bool isReverse = false;

	public bool isHorizontal = false;

	Vector3 posOffset = new Vector3 ();
	Vector3 tempPos = new Vector3 ();

	void Start () {
		posOffset = transform.position;
	}

	void Update () {

		tempPos = posOffset;
		float test = isReverse ? -Time.fixedTime : Time.fixedTime;
		float value = Mathf.Sin (test * Mathf.PI * frequency) * amplitude;
		float clampedValue = Mathf.Clamp(value, -2, 2);
		if(isHorizontal){
			tempPos.x += clampedValue;
		}
		else {
			tempPos.y += clampedValue;
		}
		transform.position = tempPos;
	}

}
