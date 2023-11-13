using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnButton : MonoBehaviour
{
    //Variables start here

    public GameObject up;
    public GameObject down;
    public GameObject left;
    public GameObject right;


    //Variables end here

    // Start is called before the first frame update
    void Start()
    {
        EventManager.updateCurrentMenu += CompareButton;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void CompareButton(GameObject currSel)
    {
        if (currSel == gameObject)
        {
            EventManager.updateCurrentDirections(up, down, left, right);
        }

    }
}
