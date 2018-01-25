using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    private Rigidbody ballRigidbody;
    private AudioSource audioSource;
    private Vector3 ballStartPosition;
    private Quaternion ballStartRotation;

    public Vector3 launchVelocity;
    public bool inPlay = false;

	// Use this for initialization
	void Start () {
        ballRigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        ballStartPosition = transform.position;
        ballStartRotation = transform.rotation;
        ballRigidbody.useGravity = false;
    }

    public void LaunchBall(Vector3 velocity) {

        if (!inPlay) {
            ballRigidbody.velocity = velocity;
            ballRigidbody.useGravity = true;
            audioSource.Play();
            inPlay = true;
        }

        
    }

    public void Reset() {
        //Debug.Log("Resetting ball.");
        ballRigidbody.velocity = Vector3.zero;
        ballRigidbody.angularVelocity = Vector3.zero;
        ballRigidbody.useGravity = false;

        inPlay = false;
        transform.rotation = ballStartRotation;
        transform.position = ballStartPosition;
    }

}
