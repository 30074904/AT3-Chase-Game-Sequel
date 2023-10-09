using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class movement : MonoBehaviour
{
    int speed = 7;
    int sprint = 10;
    int specrouch = 4;
    public float gravity = -0f;
    public float jumph = 100;
    bool crouched = false;
    //public PauseObject s_pause;

    Vector3 currSpd = Vector3.zero;

    float x_move = 0f;
    float z_move = 0f;
    public bool grounded = false;

    public UnityEngine.CharacterController C_c;
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
            C_c.height = 1f;
            Vector3 scale = transform.localScale;
            scale.y = 1F; // your new value
            transform.localScale = scale;
            crouched = true;

        }
        else
        {
            C_c.height = 2f;
            Vector3 scale = transform.localScale;
            scale.y = 1.2F; // your new value
            transform.localScale = scale;
            crouched = false;
        }
        if (Input.GetKey("left shift"))
        {
            x_move = Input.GetAxis("Horizontal") * sprint * Time.deltaTime;
            z_move = Input.GetAxis("Vertical") * sprint * Time.deltaTime;

        }
        else
        {
            x_move = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            z_move = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        }
        if (crouched == true)
        {
            x_move = Input.GetAxis("Horizontal") * specrouch * Time.deltaTime;
            z_move = Input.GetAxis("Vertical") * specrouch * Time.deltaTime;
        }
        C_c.Move(transform.forward * z_move);
        C_c.Move(transform.right * x_move);
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
            Debug.Log("Jump");
            gravity = 7;

        }








    }
    // Update is called once per frame



}