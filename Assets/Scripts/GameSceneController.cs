using UnityEngine;
using System.Collections;

public class GameSceneController : MonoBehaviour {
  /*  public struct LevelItem
    {
        public Items.ItemType itemId;
        public bool itemPicked;
    };
    */
    public bool m_visibleTriggers = true;
    public Items.ItemType[] m_requiredItems;

    private GameObject m_youWinText = null;
    private GameObject m_youLoseText = null;
    GameObjectivesController m_gameObjectives = null;

    struct ItemPickAndAction
    {
        public bool picked;
        public bool action;
    }

    ItemPickAndAction[] m_pickedItems = null;

    public void itemPicked(Items.ItemType itemId)
    {
        if (m_pickedItems[(int)itemId].picked == true)
        {
            Debug.LogWarning("Item " + itemId.ToString() + " picked twice!" );
        }
        else
        { 
            Debug.Log("Item " + itemId.ToString() + " picked" );
        }
        m_pickedItems[(int)itemId].picked = true;
    }

    public void itemAction(Items.ItemType itemId)
    {
        if (m_pickedItems[(int)itemId].action == true)
        {
            Debug.LogWarning("Item " + itemId.ToString() + " action done twice!");
        }
        else
        {
            Debug.Log("Item " + itemId.ToString() + " done action");
        }
        m_pickedItems[(int)itemId].action = true;
        m_gameObjectives.doneObjective( Items.ItemObjectives[(int)m_requiredItems[(int)itemId]] );
    }

    /// <summary>
    /// Returns rue if given item is picked from the level.
    /// </summary>
    /// <param name="itemId"></param>
    /// <returns></returns>
    public bool isItemPicked(Items.ItemType itemId)
    {
        return m_pickedItems[(int)itemId].picked;
    }

    /// <summary>
    /// Returns true if all items are picked from the level. 
    /// </summary>
    /// <returns></returns>
    public bool isAllObjectivesCompleted()
    {
        for (int i = 0; i < m_requiredItems.Length; ++i)
        {
            if (!m_pickedItems[(int)i].action)
            {
                return false;
            }
        }

        return true;
    }

    private void disablePlayer()
    {
        GameObject [] players = GameObject.FindGameObjectsWithTag("Player");
        for( int j=0; j<players.Length; ++j )
        {
            PlayerCharacterController[] ctrl = players[j].GetComponentsInChildren<PlayerCharacterController>();
            for( int i=0; i<ctrl.Length; ++i )
            {
                ctrl[i].DisableMovement();
            }
        }
    }

    void LoadNextLevel()
    {
        Application.LoadLevel(1);
    }

    void LoadMainMenu()
    {
        Application.LoadLevel(0);
    }

    /// <summary>
    /// This method is called, when level is completed. Loads new level.
    /// </summary>
    public void onLevelCompleted()
    {
        //Debug.Log("WIN! TODO: Load next level");
        m_youWinText.SetActive(true);
        disablePlayer();
        Invoke("LoadNextLevel", 5.0f);
    }

    public void onLevelFailed()
    {
        m_youLoseText.SetActive(true);
        disablePlayer();
        Invoke("LoadMainMenu", 5.0f);
    }

    public bool areTriggersVisible()
    {
        return m_visibleTriggers;
    }

	// Use this for initialization
    void Start()
    {
        m_youWinText = transform.FindChild("GUI/Canvas/YouWinText").gameObject;
        m_youLoseText = transform.FindChild("GUI/Canvas/YouLoseText").gameObject;
        m_youWinText.SetActive(false);
        m_youLoseText.SetActive(false);

        m_gameObjectives = GetComponentInChildren<GameObjectivesController>();
        for (int i = 0; i < m_requiredItems.Length; ++i)
        {
            m_gameObjectives.addObjective(Items.ItemObjectives[(int)m_requiredItems[i]]);
        }

        m_pickedItems = new ItemPickAndAction[(int)Items.ItemType.LAST_ITEM_TYPE];
        for (int i = 0; i < m_pickedItems.Length; ++i)
        {
            m_pickedItems[i].picked = false;
            m_pickedItems[i].action = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
