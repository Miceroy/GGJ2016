using UnityEngine;
using System.Collections;

public class ItemSlotController : MonoBehaviour, ActionInterface
{
    public Items.ItemType m_requiredTriggerItemId;

    public void doAction(GameObject other)
    {
        if (sceneController.isItemPicked(m_requiredTriggerItemId))
        {
            Debug.Log("Do action for item slot " + m_requiredTriggerItemId.ToString());
            sceneController.itemAction(m_requiredTriggerItemId);
            updateVisuals(false);
            gameObject.GetComponent<Collider>().enabled = false;
        }
        else
        {
            Debug.Log("Can't do action for item slot " + m_requiredTriggerItemId.ToString() + "! No item was picked!");
        }
    }



    GameObject enteredGO;
    GameObject leavedGO;
    GameSceneController sceneController;
    // Use this for initialization
    void Start()
    {
        TextMesh pickupText = gameObject.transform.FindChild("Entered/Pickup/PickupText").GetComponent<TextMesh>();
        pickupText.text = "Use " + m_requiredTriggerItemId.ToString() + "\nhere";
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

    //    Debug.Log("OnTriggerEnter " + other.gameObject.name);

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
