using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Manager : MonoBehaviour
{
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        EventManager.updateAnimationEvent += ChangeAnimation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void ChangeAnimation(int currAnim)
    {
        anim.SetInteger("CurrAnim", currAnim);
    }
}
