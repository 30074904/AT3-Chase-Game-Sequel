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

    public GameObject up;
    public GameObject down;
    public GameObject left;
    public GameObject right;

    public bool going = false;

    //Start of the interactable gameobjects

    public GameObject GStart;
    public GameObject GQuit;
    public GameObject GBack;

    //End of the interactable gameobjects

    //Variables end here
    // Start is called before the first frame update
    void Start()
    {
        currSelect = Root;
    }

    // Update is called once per frame
    void Update()
    {
        EventManager.updateCurrentDirections += SetDirrection;

        DetectInputs();

        EventManager.updateCurrentMenu(currSelect);

        CheckInteractable();
    }
    private void DetectInputs()
    {
        float y_Axis = Input.GetAxisRaw("Vertical");
        float x_Axis = Input.GetAxisRaw("Horizontal");


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
    private void SetDirrection(GameObject Up, GameObject Down, GameObject Left, GameObject Right)
    {
        up = Up;
        down = Down;
        left = Left;
        right = Right;
    }
    private void CheckInteractable()
    {
        if ((currSelect == GStart) && Input.GetButtonDown("Submit"))
        {
            Debug.Log("why it no workie");
            EventManager.ChangeLevelEvent(2);
        }
        else if ((currSelect == GStart) && Input.GetButtonDown("Submit"))
        {
            Application.Quit();
        }
        else if ((currSelect == GStart) && Input.GetButtonDown("Submit"))
        {
            EventManager.ChangeLevelEvent(1);
        }
    }
}
