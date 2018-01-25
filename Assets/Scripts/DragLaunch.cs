using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ball))]
public class DragLaunch : MonoBehaviour {

    private Ball ball;
    private float startTime;
    private Vector2 startMouseVector;
    private Vector2 endMouseVector;

	// Use this for initialization
	void Start () {
        ball = GetComponent<Ball>();
	}

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            ball.LaunchBall(new Vector3(0, 0, 888));
        }
    }

    public void DragStart() {
        if (!ball.inPlay) {
            startTime = Time.time;
            startMouseVector = Input.mousePosition;
        }
    }

    public void DragEnd() {
        if (!ball.inPlay) {
            endMouseVector = Input.mousePosition;
            float delay = Time.time - startTime;
            float angle = (endMouseVector.x - startMouseVector.x) / delay;
            float speed = (endMouseVector.y - startMouseVector.y) / delay;
            ball.LaunchBall(new Vector3(angle, 0, speed));
            ball.inPlay = true;
        }
        
    }

    public void MoveStart(float xNudge) {
        
        if (!ball.inPlay) {
            ball.transform.Translate(xNudge, 0, 0);
            ball.transform.position = new Vector3(Mathf.Clamp(ball.transform.position.x, -52.5f, 52.5f), ball.transform.position.y, ball.transform.position.z);
        }
    }
}
