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
        EventManager.ChangeLevelEvent += SwitchLevel;
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void SwitchLevel(int currLevel)
    {
        if (currLevel == 1)
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
        else if (currLevel == 2)
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
        else if ( currLevel == 3)
        {
            SceneManager.LoadScene(2, LoadSceneMode.Single);
        }
        else if (currLevel == 4)
        {
            Debug.Log("?");
            SceneManager.LoadScene(3, LoadSceneMode.Single);
            
        }
    }
    public void ButtonSwitchLevel(int levelSend)
    {
        SwitchLevel(levelSend);
    }


}