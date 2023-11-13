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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            Debug.Log("you lose");
            EventManager.ChangeLevelEvent(4);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            EventManager.ChangeLevelEvent(4);
        }
        
    }
}
