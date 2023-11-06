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
        anim.SetInteger("CurrAnim", animNum);
    }
    private void ChangeAnimationHands()
    {
        anim.SetTrigger("Push");
    }
    private void garrageOpen()
    {
        anim.SetTrigger("Open");
    }
}
