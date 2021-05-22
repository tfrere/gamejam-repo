using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideToSideTeleport : MonoBehaviour {

	void Start () {
	}

	void FixedUpdate () {
        handleSideTeleport();
	}

    void handleSideTeleport() {
        //you get a world space coord and transfom it to viewport space.
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        //everything from here on is in viewport space where 0,0 is the bottom 
        //left of your screen and 1,1 the top right.
        if (pos.x < 0.0f) {
            pos = new Vector3(1.0f, pos.y, pos.z);
        }
        else if (pos.x >= 1.0f) {
            pos = new Vector3(0.0f, pos.y, pos.z);
        }
        if (pos.y < 0.0f) {
            pos = new Vector3(pos.x, 1.0f, pos.z);
        }
        else if (pos.y >= 1.0f) {
            pos = new Vector3(pos.x, 0.0f, pos.z);
        }

        //and here it gets transformed back to world space.
        transform.position = Camera.main.ViewportToWorldPoint(pos); 
    }

}
