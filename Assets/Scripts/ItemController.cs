using UnityEngine;
using System.Collections;


public class ItemController : MonoBehaviour, ActionInterface
{
    public Items.ItemType m_triggerItemId;
    public Transform m_gameObjectToFollow;
    public GameObject[] m_pickedStateActiveObjects;
    public GameObject[] m_notPickedStateActiveObjects;

    public void doAction(GameObject other)
    {
        gameObject.GetComponent<Collider>().enabled = false;
        updateActiveObjects(true);
        enteredGO.SetActive(false);
        leavedGO.SetActive(false);
        sceneController.itemPicked(m_triggerItemId);
    }



    GameObject enteredGO;
    GameObject leavedGO;
    GameSceneController sceneController;
    Vector3 m_initialPosition;



	// Use this for initialization
    void Start()
    {
        m_initialPosition = transform.localPosition;

        TextMesh pickupText = gameObject.transform.FindChild("Entered/Pickup/PickupText").GetComponent<TextMesh>();
        pickupText.text = "Pick up\n" + m_triggerItemId.ToString();
        enteredGO = gameObject.transform.FindChild("Entered").gameObject;
        leavedGO = gameObject.transform.FindChild("Leaved").gameObject;
        sceneController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameSceneController>();
        updateActiveObjects(false);
        updateVisuals(false);
	}

    // Update is called once per frame
    void Update()
    {
        if (m_gameObjectToFollow != null)
        {
            transform.position = m_gameObjectToFollow.position + m_initialPosition;
        }
    }

    void updateActiveObjects(bool actionDone)
    {
        for (int i = 0; i < m_pickedStateActiveObjects.Length; ++i)
        {
            if (m_pickedStateActiveObjects[i] != null)
            {
                m_pickedStateActiveObjects[i].SetActive(actionDone);
            }
        }
        for (int i = 0; i < m_notPickedStateActiveObjects.Length; ++i)
        {
            if (m_notPickedStateActiveObjects[i] != null)
            {
                m_notPickedStateActiveObjects[i].SetActive(!actionDone);
            }
        }
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
                enteredGO.SetActive(true);
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
