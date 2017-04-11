using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : MonoBehaviour {

    public float mouseSensitivity = -0.01f;
    private Vector3 lastPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            lastPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 delta  = Input.mousePosition - lastPosition;
            transform.Translate(delta.x  * mouseSensitivity, delta.y  * mouseSensitivity, 0);
            lastPosition = Input.mousePosition;
        }
    }
}
