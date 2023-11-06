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

        // dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        dir = Input.GetAxisRaw("Horizontal") * transform.right + Input.GetAxisRaw("Vertical") * transform.forward;
        dir = dir.normalized;

        if (crouched == true)
        {
            /*x_move = Input.GetAxis("Horizontal") * specrouch * Time.deltaTime;
            z_move = Input.GetAxis("Vertical") * specrouch * Time.deltaTime;*/
            C_c.Move(dir * specrouch * Time.deltaTime);
        }
        else
        {
            /*x_move = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            z_move = Input.GetAxis("Vertical") * speed * Time.deltaTime;*/
            C_c.Move(dir * speed * Time.deltaTime);
        }
        /*C_c.Move(transform.forward * z_move);
        C_c.Move(transform.right * x_move);
        C_c.velocity*/
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