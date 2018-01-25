using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {

    

    public float distanceToRaisePins = 40;
    public GameObject pinSet;

    private int lastStandingCount = -1;
    private Animator animator;
    private ActionMaster actionMaster = new ActionMaster();
    private float lastChangeTime;
    private int lastSettledCount = 10;

    private Pin[] standingPins;
    private Text pinCounterText;
    private bool ballLeftBox = false;
    private Ball ball;
    

	// Use this for initialization
	void Start () {
        standingPins = GameObject.FindObjectsOfType<Pin>();
        pinCounterText = GameObject.Find("PinCount").GetComponent<Text>();
        ball = GameObject.FindObjectOfType<Ball>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        pinCounterText.text = CountStanding().ToString(); //Constantly counting standing pins yey


        if (ballLeftBox) {
            CheckStanding();

        }
    }

    int CountStanding() {
        int pinCount = 0;

        standingPins = GameObject.FindObjectsOfType<Pin>();

        foreach (Pin thisPin in standingPins) {

            if (thisPin) {
                if (thisPin.IsStanding()) {
                    pinCount++;
                }
            }
            
        }
        return pinCount;
    }

    void CheckStanding() {

        //teacher used lastChangeTime = Time.time, and then if ((Time.time - lastChangeTime) > 3) below
        // shouldn't be that much of a difference?
        // or will nonstop counting fuck things up? 
        lastChangeTime += Time.deltaTime;

        if (lastStandingCount != CountStanding()) {
            lastStandingCount = CountStanding();
            lastChangeTime = 0;
            return;
        }

        if (lastChangeTime >= 3) {
            PinsHaveSettled();
        }
    }

    void PinsHaveSettled() {
        pinCounterText.color = Color.green;

        int pinsFallen = lastSettledCount - lastStandingCount;

        ActionMaster.Action action = actionMaster.Bowl(pinsFallen); // take action based on last round (if first frame)

        if (action == ActionMaster.Action.Tidy) {
            animator.SetTrigger("tidyTrigger");
            lastSettledCount = lastStandingCount; 
        } else { // need to expand to consider other actions, but fine for now
            animator.SetTrigger("resetTrigger");
            lastSettledCount = 10;
        }

        print(pinsFallen);
        print(action);

        lastStandingCount = -1;
        ballLeftBox = false;
        ball.Reset();

    }


    private void OnTriggerExit(Collider other) {

        /* Not accurate enough yo
        if (other.GetComponent<Ball>()) {
            Debug.Log(CountStanding());
            pinCounterText.text = CountStanding().ToString();
        }
        */

        if (other.GetComponent<Ball>()) {
            ballLeftBox = true;
            pinCounterText.color = Color.red;
            lastChangeTime = 0;
        }

        //Debug.Log(other.name + " exit collider");
    }

    void RaisePins() {
        //Debug.Log("Raising Pins");
        standingPins = GameObject.FindObjectsOfType<Pin>();

        foreach (Pin thisPin in standingPins) {
            if (thisPin.IsStanding()) {
                Rigidbody thisRigidbody = thisPin.GetComponent<Rigidbody>();
                thisRigidbody.velocity = Vector3.zero;
                thisRigidbody.angularVelocity = Vector3.zero;
                thisRigidbody.useGravity = false;
                thisPin.transform.Translate(new Vector3(0, distanceToRaisePins, 0), Space.World);
            }
            
        }
    }

    void LowerPins() {
        //Debug.Log("Lowering Pins");
        standingPins = GameObject.FindObjectsOfType<Pin>();

        foreach (Pin thisPin in standingPins) {
            Rigidbody thisRigidbody = thisPin.GetComponent<Rigidbody>();
            thisRigidbody.velocity = Vector3.zero;
            thisRigidbody.angularVelocity = Vector3.zero;
            thisPin.transform.Translate(new Vector3(0, -distanceToRaisePins, 0), Space.World);
            
            thisRigidbody.useGravity = true;
        }

    }

    void RenewPins() {
        //Debug.Log("Renewing Pins");
        Instantiate(pinSet, new Vector3(0, distanceToRaisePins, 1829), Quaternion.identity);
        standingPins = GameObject.FindObjectsOfType<Pin>();

        foreach (Pin thisPin in standingPins) {
            //Debug.Log("does this work?");
            Rigidbody thisRigidbody = thisPin.GetComponent<Rigidbody>();
            thisRigidbody.velocity = Vector3.zero;
            thisRigidbody.angularVelocity = Vector3.zero;
            thisRigidbody.useGravity = false;
        }
    }
}
