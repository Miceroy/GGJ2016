using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BloodEngine : MonoBehaviour {

    List<BloodScript> bloodPool = new List<BloodScript>();
    List<BloodScript> activeBlood = new List<BloodScript>();
    public List<GameObject> baseBloodSplatters = new List<GameObject>();

    GameObject BloodSplatters;
    public int maxAmountOfBlood = 1000;


    
	// Use this for initialization
	void Start () {
        BloodSplatters = new GameObject("BloodSplatters");
        Transform bst = BloodSplatters.GetComponent<Transform>();
        Vector3 spawnPos = new Vector3(0, -100, 0);
        //Fill the blood pool
        for (int a = 0; a < maxAmountOfBlood; ++a)
        {
            GameObject go = (GameObject)Instantiate(baseBloodSplatters[0], spawnPos, Quaternion.identity);
            go.transform.parent = bst;
            bloodPool.Add(go.GetComponent<BloodScript>());
            bloodPool[a].Freeze();
        }

    }

    void ReserveBlood(int amount)
    {
        int reserveAmount = amount;
        if (bloodPool.Count < amount)
        {
            if (maxAmountOfBlood < reserveAmount)
                reserveAmount = maxAmountOfBlood;
            FreeBlood(reserveAmount - bloodPool.Count);
        }
        for (int a = 0; a < reserveAmount; ++a)
        {
            bloodPool[a].active = true;
            activeBlood.Add(bloodPool[a]);
        }
        bloodPool.RemoveRange(0, reserveAmount);
    }

    void FreeBlood(int amount)
    {
        int freeAmount = amount;
        if (activeBlood.Count < amount)
        {
            freeAmount = activeBlood.Count;
        }
        for (int a = 0; a < amount; ++a)
        {
            activeBlood[a].active = false;
            bloodPool.Add(activeBlood[a]);   
        }
        activeBlood.RemoveRange(0, freeAmount);
       
    }
    Vector3 AddVariation(Vector3 src, Vector3 variance)
    {
        Vector3 output = src;
        output.x = output.x + Random.Range(-variance.x, variance.x);
        output.y = output.y + Random.Range(-variance.y, variance.y);
        output.z = output.z + Random.Range(-variance.z, variance.z);
        return output;
    }

    public void CreateTestSplash(int testVal)
    {
        switch (testVal)
        {
            case 0:
                CreateSplash(new Vector3(0,2,0), Vector3.forward, new Vector3(200,200,1), 200, 30);
                break;
            case 1:
                CreateSplash(Vector3.zero, Vector3.forward, new Vector3(10, 10, 10), 2, 100);
                break;
            default:
                CreateSplash(Vector3.zero, Vector3.forward, new Vector3(10, 10, 10), 2, 500);
                break;
        }
    }

    public void CreateSplash(Vector3 startPos, Vector3 direction, Vector3 variance, float startSpeed, int amount)
    {
        int bloodAmount = amount;
        if (maxAmountOfBlood < amount)
            bloodAmount = maxAmountOfBlood;
        ReserveBlood(bloodAmount);
        int targetEntity = 0;
        Vector3 cameraLookDirection = Camera.main.transform.forward;
        Vector3 normalizedDirection = direction.normalized;
        for (int a = 0; a < bloodAmount; ++a)
        {
            targetEntity = activeBlood.Count - bloodAmount + a;
            activeBlood[targetEntity].SetPosition(startPos);
            activeBlood[targetEntity].SetRotation(Quaternion.LookRotation(cameraLookDirection));
            activeBlood[targetEntity].AddForce(AddVariation(normalizedDirection*startSpeed, variance));
        }

    }

    // Update is called once per frame
    void Update () {
	
	}
}
