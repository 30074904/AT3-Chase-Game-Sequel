using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //Variables

    public Transform tCamera;

    public int interactionDistance;

    private float stunTme = 4;
    //Variables End


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            DoInteraction();
            EventManager.updateAnimationHandsEvent();
        }
        
    }
    private void DoInteraction()
    {
        RaycastHit hit;


        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance))
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.yellow);

            if (hit.collider.TryGetComponent<EnermyPatrol>(out EnermyPatrol interaction))
            {
                interaction.GoStun(stunTme);
                stunTme = stunTme - 1;
            }
        }
    }
}
