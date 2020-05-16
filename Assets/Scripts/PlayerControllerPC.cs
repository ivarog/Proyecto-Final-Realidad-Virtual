using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerPC : MonoBehaviour
{

    [SerializeField] float velocity;
    [SerializeField] GameObject gameCamera;
    // private Rigidbody rb;
    CharacterController character;
    [SerializeField] float sensitivity = 100;
    [SerializeField] Transform playerBody;
    
    float xRotation = 0;
    AudioSource steps;
    public bool iAmMoving;

    private void Start() 
    {
        character = GetComponent<CharacterController>();
        steps = GetComponent<AudioSource>();
    }

    private void FixedUpdate() 
    {
        Move();    
        Rotate();
        Ground();
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 movement = transform.right * x + transform.forward * z;
        character.Move(movement * Time.deltaTime * velocity);

        if(Mathf.Abs(x) > 0.05 || Mathf.Abs(z) > 0.05)
        {
            if(!steps.isPlaying)
                steps.Play();
            iAmMoving = true;
        }
        else
        {
            steps.Stop();
            iAmMoving = false;
        }
    }

    void Rotate()
    {
        float rotateX = Input.GetAxis("Camera Horizontal") * sensitivity * Time.deltaTime;
        float rotateY = Input.GetAxis("Camera Vertical") * sensitivity * Time.deltaTime;

        xRotation -= rotateY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        gameCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * rotateX);
    }

    void Ground()
    {
        bool isGrounded = character.isGrounded;
        float verticalVelocity = 0.0f;
 
        if (isGrounded)
        {
            verticalVelocity -= 0;
        }
        else
        {
            verticalVelocity -= 1;
        }
 
        Vector3 moveVector = new Vector3(0, verticalVelocity, 0);
        character.Move(moveVector);
    }
}