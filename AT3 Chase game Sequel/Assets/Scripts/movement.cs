using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class movement : MonoBehaviour
{
    public int speed = 7;
    public int sprint = 10;
    public int specrouch = 4;
    public float gravity = -0f;
    public float jumph = 100;
    bool crouched = false;
    //public PauseObject s_pause;

    Vector3 currSpd = Vector3.zero;

    float x_move = 0f;
    float z_move = 0f;
    Vector3 dir = Vector3.zero;
    public bool grounded = false;

    public UnityEngine.CharacterController C_c;

    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        //get the components
        C_c = GetComponent<CharacterController>();
    }
    void FixedUpdate()
    {
        if (C_c.isGrounded == true)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        if (Input.GetKey("left ctrl"))
        {
            if (Physics.Raycast(transform.position, Vector3.up, out hit, 10f))
            {
                Debug.Log("cant do that.");
            }
            else
            {
                C_c.height = 0.5f;
                Vector3 scale = transform.localScale;
                scale.y = 1F; // your new value
                transform.localScale = scale;
                crouched = true;
            }
            

        }
        else
        {
            if (Physics.Raycast(transform.position, Vector3.up, out hit, 10f))
            {
                Debug.Log("cant do that.");
            }
            else
            {
                C_c.height = 2f;
                Vector3 scale = transform.localScale;
                scale.y = 1.2F; // your new value
                transform.localScale = scale;
                crouched = false;
            }
            
        }

        dir = Input.GetAxisRaw("Horizontal") * transform.right + Input.GetAxisRaw("Vertical") * transform.forward;
        dir = dir.normalized;

        if (crouched == true)
        {
            C_c.Move(dir * specrouch * Time.deltaTime);
        }
        else
        {
            C_c.Move(dir * speed * Time.deltaTime);
        }
        //checks if grounded and if no applys gravity
        if (grounded == true)
        {
            gravity = 0;
        }
        else if (grounded == false)
        {
            gravity = gravity - 0.3f;
            C_c.Move(new Vector3(0, gravity * Time.deltaTime, 0));
        }
        //Jumps when space is pressed
        if (Input.GetKey("space") && (grounded == true))
        {
            gravity = 7;

        }








    }
    // Update is called once per frame



}