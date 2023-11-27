using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuNav : MonoBehaviour
{
    //Variables start here


    public float Y_Axis;

    public GameObject currSelect;

    public GameObject Root;

    // start of the variables taken from the current sel
    public GameObject up;
    public GameObject down;
    public GameObject left;
    public GameObject right;
    public GameObject destination;
    public GameObject depart;
    public GameObject goingTo;

    // end of the variables taken from current sel

    public bool going = false;

    //Start of the interactable gameobjects

    public GameObject GStart;
    public GameObject GQuit;
    public GameObject GInfo;
    public GameObject GBack;
    public GameObject infoScreen;

    //End of the interactable gameobjects

    //Variables end here
    // Start is called before the first frame update
    void Start()
    {
        // setting the first selection
        currSelect = Root;
    }

    // Update is called once per frame
    void Update()
    {
        //syncronising the event update current directions with the methord setdirrection
        EventManager.updateCurrentDirections += SetDirrection;

        DetectInputs();

        EventManager.updateCurrentMenu(currSelect);

        CheckInteractable();
    }
    private void DetectInputs()
    {
        float y_Axis = Input.GetAxisRaw("Vertical");
        float x_Axis = Input.GetAxisRaw("Horizontal");

        // detecting if the player is trying to move selcetion
        if (y_Axis == 0f && x_Axis == 0f)
        {
            going = false;
        }
        
        if (y_Axis > 0 && going != true)
        {
            going = true;
            currSelect = up;

        }
        else if (y_Axis < 0 && going != true)
        {
            going = true;
            currSelect = down;

        }
        if (x_Axis < 0 && going != true)
        {
            going = true;
            currSelect = left;

        }
        else if (x_Axis > 0 && going != true)
        {
            going = true;
            currSelect = right;

        }
    }
    private void SetDirrection(GameObject Up, GameObject Down, GameObject Left, GameObject Right, GameObject Destination, GameObject Depart, GameObject GoingTo)
    {
        // Getting variables from the current button
        up = Up;
        down = Down;
        left = Left;
        right = Right;
        destination = Destination;
        goingTo = GoingTo;
        depart = Depart;
        
    }
    private void CheckInteractable()
    {
        //Checking agaste the current selection to see if its any buttons with fuctionality and if so exicuting said functionality when pressed.
        if ((currSelect == GStart) && Input.GetButtonDown("Submit") || Input.GetButtonDown("AButton"))
        {
            Debug.Log("why it no workie");
            EventManager.ChangeLevelEvent(2);
        }
        else if ((currSelect == GQuit) && Input.GetButtonDown("Submit") || Input.GetButtonDown("AButton"))
        {
            Application.Quit();
        }
        else if ((currSelect == GInfo) && Input.GetButtonDown("Submit") || Input.GetButtonDown("AButton"))
        {
            currSelect = goingTo;
            destination.SetActive(true);
            depart.SetActive(false);
        }
        else if ((currSelect == GBack) && Input.GetButtonDown("Submit") || Input.GetButtonDown("AButton"))
        {
            currSelect = goingTo;
            destination.SetActive(true);
            depart.SetActive(false);
        }
        else if ((currSelect == GStart) && Input.GetButtonDown("Submit") || Input.GetButtonDown("AButton"))
        {
            EventManager.ChangeLevelEvent(1);
        }
    }
}
