using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //Variables



    public int interactionDistance = 5;

    private float stunTme = 4;

    private bool keyStolen;
    //Variables End


    // Start is called before the first frame update
    void Start()
    {
        EventManager.ArtifactStolenEvent += KeyStolen;
        EventManager.DoAttackEvent += DoInteraction;
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
        EnermyPatrol interaction;

        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance))
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.yellow);

            if (hit.collider.tag == "knight")
            {
                interaction = hit.collider.GetComponentInParent<EnermyPatrol>();
                Debug.Log("hit Enermy");
                if (interaction.chaseState != EnermyPatrol.EnermyState.stun)
                {
                    
                    interaction.GoStun(stunTme);
                    stunTme = stunTme - 1;
                }
                
            }
            else if (hit.collider.tag == "door")
            {
                if (keyStolen == true)
                {
                    EventManager.GarrageEvent();
                }
                
            }
        }
    }
    private void KeyStolen(bool stolen)
    {

        if (stolen == true)
        {
            Debug.Log("???");
            keyStolen = true;
        }
    }
}
