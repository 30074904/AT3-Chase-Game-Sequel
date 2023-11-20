using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PutOnButton : MonoBehaviour
{
    //Variables start here

    public GameObject up;
    public GameObject down;
    public GameObject left;
    public GameObject right;

    private Image buttonImage;

    public GameObject thisG;

    private float timeMax = 0.1f;
    private float timer = 0.1f;
    //Variables end here

    // Start is called before the first frame update
    void Start()
    {
        EventManager.updateCurrentMenu += CompareButton;
        if (!TryGetComponent<Image>(out buttonImage))
        {
            Debug.Log("Image attatched");
        }

        //The fact i have to do this is dumb
        thisG = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = -1;
            buttonImage.color = Color.white;
        }
    }
    private void CompareButton(GameObject currSel)
    {
        if (thisG != null)
        {
            if (currSel == thisG)
            {
                buttonImage.color = Color.red;
                timer = timeMax;
                EventManager.updateCurrentDirections(up, down, left, right);
            }
        }
        
    }
}