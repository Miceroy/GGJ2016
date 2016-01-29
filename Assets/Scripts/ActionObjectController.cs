using UnityEngine;
using System.Collections;

public class ActionObjectController : MonoBehaviour {
    GameObject enteredGO;
    GameObject leavedGO;
    GameSceneController sceneController;
	// Use this for initialization
	void Start () {
        enteredGO = gameObject.transform.FindChild("Entered").gameObject;
        leavedGO = gameObject.transform.FindChild("Leaved").gameObject;
        sceneController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameSceneController>();

        updateVisuals(false);
	}

    void updateVisuals(bool onTrigger)
    {
        if (sceneController.areTriggersVisible())
        {
            if (onTrigger)
            {
                enteredGO.SetActive(true);
                leavedGO.SetActive(false);
            }
            else
            {
                enteredGO.SetActive(false);
                leavedGO.SetActive(true);
            }
        }
        else
        {
            enteredGO.SetActive(false);
            leavedGO.SetActive(false);
        }
    }
    /*
	// Update is called once per frame
	void Update ()
    {
        
	
	}*/

    void OnTriggerEnter(Collider other)
    {
        updateVisuals(true);

        Debug.Log("OnTriggerEnter " + other.gameObject.name);

        //Destroy(other.gameObject);
    }

    void OnTriggerStay(Collider other)
    {
    //    Debug.Log("OnTriggerStay " + other.gameObject.name);
    }

    void OnTriggerExit(Collider other)
    {
        updateVisuals(false);


        //Destroy(other.gameObject);
    }
}
