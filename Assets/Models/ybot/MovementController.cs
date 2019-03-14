using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public bool isGrounded { get; set; }
    public float speed { get; set; }
    public float rotSpeed { get; set; }
    public GameObject terrain;

    private float jumpHeight = 10000.0f;
    private float walkSpeed = 0.05f;
    private float rot_speed = 3.0f;

    Rigidbody rb;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        isGrounded = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Terrain")
        {
            Debug.Log("Ground hit !");
            isGrounded = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
    }

    void UpdateMovement()
    {
        if (isGrounded)
        {
            if (Input.GetKey(KeyCode.W))
            {
                speed = walkSpeed;
                MovementControl("WalkingForward");
            }
            else if (Input.GetKey(KeyCode.S))
            {
                speed = walkSpeed;
                MovementControl("WalkingBackward");
            }
            else
            {
                MovementControl("idle");
            }

            if (Input.GetKey(KeyCode.A))
            {
                rotSpeed = rot_speed;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                rotSpeed = rot_speed;
            }
            else
            {
                rotSpeed = 0;
            }
        }

        var z = Input.GetAxis("Vertical") * speed;
        var y = Input.GetAxis("Horizontal") * rotSpeed;
        transform.Translate(0, 0, z);
        transform.Rotate(0, y, 0);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            anim.SetTrigger("isJumping");
            rb.AddForce(0, jumpHeight * Time.deltaTime, 0);
            isGrounded = false;
        }
    }

    void MovementControl(string state)
    {
        switch (state)
        {
            case "WalkingForward":
                anim.SetBool("isWalkingForward", true);
                anim.SetBool("isWalkingBackward", false);
                anim.SetBool("isIdle", false);
                break;
            case "WalkingBackward":
                anim.SetBool("isWalkingForward", false);
                anim.SetBool("isWalkingBackward", true);
                anim.SetBool("isIdle", false);
                break;
            case "idle":
                anim.SetBool("isWalkingForward", false);
                anim.SetBool("isWalkingBackward", false);
                anim.SetBool("isIdle", true);
                break;
        }
    }

}
