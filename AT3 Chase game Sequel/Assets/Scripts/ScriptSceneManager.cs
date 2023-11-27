using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptSceneManager : MonoBehaviour
{
    public static SceneManager instance;

    // Start is called before the first frame update
    int level = 1;
    void Start()
    {
        //syncronising the change level event with the switch level methord
        EventManager.ChangeLevelEvent += SwitchLevel;
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void SwitchLevel(int currLevel)
    {
        // switching the level to what the methord is called with
        if (currLevel == 1)
        {
            //start menu
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
        else if (currLevel == 2)
        {
            //game scene
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
        else if ( currLevel == 3)
        {
            //win
            SceneManager.LoadScene(2, LoadSceneMode.Single);
        }
        else if (currLevel == 4)
        {
            //lose
            Debug.Log("?");
            SceneManager.LoadScene(3, LoadSceneMode.Single);
            
        }
    }
    // used for the button click 
    public void ButtonSwitchLevel(int levelSend)
    {
        SwitchLevel(levelSend);
    }


}