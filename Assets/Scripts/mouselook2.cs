using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class mouselook2 : MonoBehaviour
{
    // public vars
    public float mouseSensitivityX = 1;
    public float mouseSensitivityY = 1;
    public float walkSpeed = 6;
    public float jumpForce = 220;
    public LayerMask groundedMask;
    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;

    public CinemachineVirtualCamera cm;
    private CinemachineComposer cmcom;

    // System vars
    bool grounded;
    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;
    float verticalLookRotation;
    public Transform cameraTransform;
    Rigidbody rb;


    void Awake()
    {
        cmcom = cm.GetCinemachineComponent<CinemachineComposer>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        LookRotation();
        Movement();
        Jump();
    }

    private void Jump()
    {
        // Grounded check
        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        Debug.Log(grounded);

        // Jump
        if (Input.GetButtonDown("Jump"))
        {
            if (grounded)
            {
                rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

    private void Movement()
    {
        // Calculate movement:
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = new Vector3(inputX, 0, inputY).normalized;
        Vector3 targetMoveAmount = moveDir * walkSpeed;
        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);
    }

    private void LookRotation()
    {
        // Look rotation:
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX);
        verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, .5f, .8f);
        //cameraTransform.localEulerAngles = Vector3.left * verticalLookRotation;
        cmcom.m_ScreenY = verticalLookRotation;
    }

    void FixedUpdate()
    {
        // Apply movement to rigidbody
        Vector3 localMove = transform.TransformDirection(moveAmount) * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + localMove);
    }
}
