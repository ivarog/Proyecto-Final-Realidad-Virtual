using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    [SerializeField] float mouseSensitivity = 100;
    [SerializeField] Transform playerBody;
    [SerializeField] Transform myCamera;
    float xRotation = 0;
    public bool iAmLooking = false;

    void Start()
    {
        // Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        myCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);

        iAmLooking = (Mathf.Abs(mouseX) > 0.1 || Mathf.Abs(mouseY) > 0.05) ? true : false;
    }
}