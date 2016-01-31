using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjectivesController : MonoBehaviour {
    public GameObject objectiveTextPrefab;
    public Color notDoneColor = Color.red;
    public Color doneColor = Color.green;
 
    List<GameObject> objectives = new List<GameObject>();
    
    public void addObjective(string text)
    {
        GameObject newGo = GameObject.Instantiate(objectiveTextPrefab, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
        newGo.transform.SetParent(gameObject.transform);

        Vector3 pos = newGo.transform.position;
        pos.y += -20.0f * objectives.Count;
        newGo.transform.position = pos;

        UnityEngine.UI.Text t = newGo.GetComponent<UnityEngine.UI.Text>();
        t.text = (objectives.Count + 1).ToString() + ". " + text;
        t.color = notDoneColor;
        objectives.Add(newGo);        
    }

    public void doneObjective(string text)
    {
        for( int i=0;  i<objectives.Count; ++i )
        {
            string textToSearch = (i + 1).ToString() + ". " + text;
            UnityEngine.UI.Text t = objectives[i].GetComponent<UnityEngine.UI.Text>();
            if (t.text == textToSearch)
            {
                t.color = doneColor;
            }
        }
    }

// Use this for initialization
/*	void Start () {
        addObjective("Joo");
        addObjective("Jes");
        addObjective("Jaa");
        addObjective("Jes");


        doneObjective("Jes");
    }
		
	// Update is called once per frame
	void Update () {
	
	}
 * */
}
