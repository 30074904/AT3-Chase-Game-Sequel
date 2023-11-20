using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    //Variables start

    public AudioSource step;
    public AudioSource neck;

    //Variables end
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayStep()
    {
        step.Play();
    }
    public void PlayNeckSwish()
    {
        neck.Play();
    }
}
