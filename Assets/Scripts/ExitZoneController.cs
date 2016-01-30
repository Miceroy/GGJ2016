using UnityEngine;
using System.Collections;

public class ExitZoneController : MonoBehaviour {

    GameObject enteredGO;
    GameObject leavedGO;
    GameSceneController sceneController;
    // Use this for initialization
    void Start()
    {
       // m_picked = false;
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

    void OnTriggerEnter(Collider other)
    {
        updateVisuals(true);
        if (other.gameObject.tag == "Player")
        {
            if (sceneController.isAllObjectivesCompleted())
            {
                Debug.Log("Level completed!");
                sceneController.onLevelCompleted();
            }
            else
            {
                Debug.Log("Some items are not yet picked can not complete the level.");
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
    }

    void OnTriggerExit(Collider other)
    {
        updateVisuals(false);
    }
}
