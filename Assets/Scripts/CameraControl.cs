using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    private Ball ball;
    private Vector3 offsetFromBall;

	// Use this for initialization
	void Start () {
        ball = GameObject.FindObjectOfType<Ball>();
        offsetFromBall = transform.position - ball.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        if (ball.transform.position.z <= 1829) {
            transform.position = ball.transform.position + offsetFromBall;

        }

    }
}
