using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {
    void StartGame()
    {
        //Debug.Log("Start game clicked");
        Application.LoadLevel(1);
    }
    void ShowCredits()
    {
        //Debug.Log("Start game clicked");
        Application.LoadLevel(2);
    }

    void Exit()
    {
        Application.Quit();
    }
}
