using UnityEngine;
using System.Collections;

public class BloodScript : MonoBehaviour {

    Transform t;
    Rigidbody r;
    public bool active = false;
    bool needToFreeze = false;

	// Use this for initialization
	void Start () {
        t = GetComponent<Transform>();
        r = GetComponent<Rigidbody>();
	}

    public void SetPosition(Vector3 worldPos)
    {
        t.position = worldPos;
        r.velocity = Vector3.zero;
        r.angularVelocity = Vector3.zero;
    }
    public void SetRotation(Quaternion rotation)
    {
        t.rotation = rotation;
    }
    public void AddForce(Vector3 force)
    {
        //r.velocity = force;
        r.isKinematic = false;
        r.useGravity = true;
        r.AddForce(force, ForceMode.Force);

    }
    public void Freeze()
    {
        needToFreeze = true;
        
    }
	
	// Update is called once per frame
	void Update () {
        if (needToFreeze)
        {
            r.isKinematic = true;
            r.useGravity = false;
            needToFreeze = false;
        }
	}

    void OnCollisionEnter(Collision coll)
    {
        //ContactPoint contact = coll.contacts[0];
        //Quaternion rot = Quaternion.FromToRotation(-Camera.main.transform.forward, contact.normal);
        
        //SetRotation(rot);
        //Freeze();
    }
}
