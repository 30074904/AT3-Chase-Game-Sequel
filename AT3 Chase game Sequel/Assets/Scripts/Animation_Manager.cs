using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Animation_Manager : MonoBehaviour
{
    public Animator anim;

    private bool keyStolen;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        EventManager.updateAnimationHandsEvent += ChangeAnimationHands;
        EventManager.updateAnimationEvent += ChangeAnimation;
        EventManager.GarrageEvent += garrageOpen;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void ChangeAnimation(int animNum)
    {
        foreach (AnimatorControllerParameter param in anim.parameters)
        {
            if (param.name == "CurrAnim")
            {
                anim.SetInteger("CurrAnim", animNum);
            }
            
        }
            
        
    }
    private void ChangeAnimationHands()
    {
        anim.SetTrigger("Push");
        
    }
    private void garrageOpen()
    {
        anim.SetTrigger("Open");
    }
    private void DoInteraction()
    {
        EventManager.DoAttackEvent();
    }
    private void Win()
    {
        EventManager.ChangeLevelEvent(3);
    }
}
