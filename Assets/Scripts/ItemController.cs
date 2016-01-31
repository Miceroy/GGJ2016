using UnityEngine;
using System.Collections;


public class ItemController : MonoBehaviour, ActionInterface
{
    public Items.ItemType m_triggerItemId;

    public void doAction(GameObject other)
    {
        gameObject.GetComponent<Collider>().enabled = false;
        enteredGO.SetActive(false);
        leavedGO.SetActive(false);
        sceneController.itemPicked(m_triggerItemId);
    }



    GameObject enteredGO;
    GameObject leavedGO;
    GameSceneController sceneController;
	// Use this for initialization
	void Start () {
        TextMesh pickupText = gameObject.transform.FindChild("Entered/Pickup/PickupText").GetComponent<TextMesh>();
        pickupText.text = "Pick up\n" + m_triggerItemId.ToString();
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
    }

    void OnTriggerStay(Collider other)
    {
    }

    void OnTriggerExit(Collider other)
    {
        updateVisuals(false);
    }
}
