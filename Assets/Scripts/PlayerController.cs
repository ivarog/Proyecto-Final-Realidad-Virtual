using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float velocity;
    [SerializeField] GameObject gameCamera;
    // private Rigidbody rb;
    CharacterController character;
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
        Ground();
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

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

        Vector3 movement = transform.right * x + transform.forward * z;
        Vector3 eulerCameraRotation = Camera.main.transform.localRotation.eulerAngles;
        transform.rotation = Quaternion.Euler(new Vector3(0f, eulerCameraRotation.y, 0f));
        character.Move(movement * Time.deltaTime * velocity);
        gameCamera.transform.position = transform.position + new Vector3(0.0f, 0.4f, 0.0f);
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