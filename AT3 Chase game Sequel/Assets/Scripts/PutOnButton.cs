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
    public GameObject destination;
    public GameObject depart;
    public GameObject goingTo;

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
        // making a timer that starts when the button is first selected and switchs the color to white after a period of time
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
        // checking if this is the current selection and if it is making it red and starting the timer, it also gives the menu navigator its variables.
        if (thisG != null)
        {
            if (currSel == thisG)
            {
                buttonImage.color = Color.green;
                timer = timeMax;
                EventManager.updateCurrentDirections(up, down, left, right, destination, depart, goingTo);
            }
        }
        
    }
}