using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour 
{ 
    Rigidbody rigidBody;
    new AudioSource audio;

    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("OK");
                break;
            case "Fuel":
                print("Fuel");
                break;
            default:
                print("Dead");
                break;
        }
    }
    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space)) // Thrust while rotating
        {
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);
            if (!audio.isPlaying) // Doesn't overlap audio 
            {
                audio.Play();
            }
        }
        else
        {
            audio.Stop();
        }
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true; // Take manual control of rotation

        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {

            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
        rigidBody.freezeRotation = false; // Resume physics rotation after player control
        
    }
}

