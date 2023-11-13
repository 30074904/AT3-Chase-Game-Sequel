using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectKey : MonoBehaviour
{
    //My variables start here

    public GameObject key;
    public GameObject warning;

    private bool warningNow = false;
    private bool keyGot = false;
    public float timerMax = 5;
    public float timer;
    //My variables end here
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DoWarning();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (keyGot == false)
        {
            keyGot = true;
            EventManager.ArtifactStolenEvent(true);
            key.SetActive(false);
            warningNow = true;
            warning.SetActive(true);
        }
        
    }
    private void DoWarning()
    {
        if (warningNow == true)
        {
            timer += Time.deltaTime;

            if (timer > timerMax)
            {
                warning.SetActive(false);
                warningNow = false;
                timer = 0;
            }

        }
    }
}
