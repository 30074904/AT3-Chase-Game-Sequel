using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightHurt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // checking if the player has entered the knights trigger and if so triggering the lose state.
    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Entered a hitbox");
        if (collision.tag == "Player")
        {
            EventManager.ChangeLevelEvent(4);
        }
    }
}
