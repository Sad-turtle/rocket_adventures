using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 3f;
    [SerializeField] float thrustForce = 10f ;
    [SerializeField] AudioClip mainEngine;


    Rigidbody rb;
    AudioSource RocketAudio;

    bool isAlive;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        RocketAudio = GetComponent<AudioSource>();
       
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }


    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * thrustForce * Time.deltaTime);
            if (!RocketAudio.isPlaying)
            {
                RocketAudio.PlayOneShot(mainEngine); 
            }

        }
        else
        {
            RocketAudio.Stop();
        }

    }
    
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationSpeed);
        }
    }

    private void ApplyRotation(float thisRotationSpeed)
    {
        rb.freezeRotation = true; //freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * thisRotationSpeed * Time.deltaTime);
        rb.freezeRotation = false; //unfreezing it so physics system can take it
    }
}
