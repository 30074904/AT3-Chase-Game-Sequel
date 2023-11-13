using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    }
    private void DetectInputs()
    {
        float y_Axis = Input.GetAxis("Vertical") / 10 * Time.deltaTime;
        float x_Axis = Input.GetAxis("Horizontal") / 10 * Time.deltaTime;

        Y_Axis = y_Axis;

        if (Input.GetKeyDown(KeyCode.W))
        {
            y_Axis = 0f;
            Debug.Log("more y");
            if (up != null)
            {
                currSelect = up;
                
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            y_Axis = 0f;
            Debug.Log("less y");
            if (down != null)
            {
                currSelect = down;
                
            }
            
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            x_Axis = 0f;
            Debug.Log("more x");
            if (left != null)
            {
                currSelect = left;
                
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            x_Axis = 0f;
            Debug.Log("less x");
            if (right != null)
            {
                currSelect = right;
                
            }
            
        }
    }
    private void SetDirrection(GameObject Up, GameObject Down, GameObject Left, GameObject Right)
    {
        up = Up;
        down = Down;
        left = Left;
        right = Right;
    }
}
