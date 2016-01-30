using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour
{
    public float stayTime = 0;
    // Use this for initialization
    bool timerActive = false;
    public void StartTimer()
    {
        timerActive = true;
    }

    public bool IsDone()
    {
        if (stayTime <= 0)
        {
            timerActive = false;
            return true;
        }
        return false;
    }
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (timerActive)
            stayTime -= Time.deltaTime;
	
	}
}
