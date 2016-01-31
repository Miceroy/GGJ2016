using UnityEngine;
using System.Collections;

public class RotatorScript : MonoBehaviour {
    Vector3 rotateAxis = new Vector3(0.0f,1.0f,0.0f);
    float rotateSpeed = 360.0f;

	// Use this for initialization
	void Start () {
        rotateAxis.Normalize();
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Euler(rotateAxis * Time.timeSinceLevelLoad * rotateSpeed);
	}
}
