using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 12f;
    public float jumpHeight = 3f;
    public float gravity = -9.81f;

    public GameObject Planet;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    private bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        //PlayerInput();
        Jump();
        Movement();

    }
    private void FixedUpdate()
    {
        GravityDirection();
    }


    private void Movement()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        input = transform.TransformDirection(input);

        rb.MovePosition(transform.position + input * Time.deltaTime * speed);
    }

    private void Jump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            rb.AddRelativeForce(0, jumpHeight, 0, ForceMode.Impulse);
        }
    }

    public float rotationSpeed = 20;

    private void GravityDirection()
    {
        Vector3 gravityUp = Vector3.zero;
        gravityUp = (transform.position - Planet.transform.position).normalized;

        Vector3 localUp = transform.up;
        Quaternion targetRotation = Quaternion.FromToRotation(localUp, gravityUp) * transform.rotation;
        transform.up = Vector3.Lerp(transform.up, gravityUp, rotationSpeed * Time.deltaTime);

        rb.AddForce((gravityUp * gravity) * rb.mass);

    }
}
