using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public Transform CameraT;
    public float mouseSensitivity = 100f;
    public Transform playerBody; //moet miss een rb worden

    private float xRotation = 0f;

    float verticalLookRotation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
       //Transform CameraT = transform.GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        //MouseMovement();
        MouseRotate();
    }

    private void MouseMovement()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void MouseRotate()
    {
        playerBody.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity);
        verticalLookRotation += Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);
        transform.localEulerAngles = Vector3.left * verticalLookRotation;
    }
}
