using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 3f;
    [SerializeField] float thrustForce = 10f ;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainThrustParticles;
    [SerializeField] ParticleSystem leftEngineParticles;
    [SerializeField] ParticleSystem rightEngineParticles;


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
        ExitApp();
    }

    private static void ExitApp()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }

    }
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();

        }
        else
        {
            StopRotating();
        }
    }



    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustForce * Time.deltaTime);
        if (!mainThrustParticles.isPlaying)
        {
            mainThrustParticles.Play();
        }
        if (!RocketAudio.isPlaying)
        {
            RocketAudio.PlayOneShot(mainEngine);
        }
    }

    private void StopThrusting()
    {
        RocketAudio.Stop();
        mainThrustParticles.Stop();
    }

    private void StopRotating()
    {
        leftEngineParticles.Stop();
        rightEngineParticles.Stop();
    }

    private void RotateRight()
    {
        ApplyRotation(-rotationSpeed);
        if (!leftEngineParticles.isPlaying)
        {
            leftEngineParticles.Play();
        }
    }

    private void RotateLeft()
    {
        ApplyRotation(rotationSpeed);

        if (!rightEngineParticles.isPlaying)
        {
            rightEngineParticles.Play();
        }
    }

    private void ApplyRotation(float thisRotationSpeed)
    {
        rb.freezeRotation = true; //freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * thisRotationSpeed * Time.deltaTime);
        rb.freezeRotation = false; //unfreezing it so physics system can take it
    }
}
