using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PinCounter))]
public class PinSetter : MonoBehaviour {

    

    public float distanceToRaisePins = 40;
    public GameObject pinSet;
    private Animator animator;
    private PinCounter pinCounter;


    private void Start() {
        animator = GetComponent<Animator>();
        pinCounter = GetComponent<PinCounter>();

    }

    void RaisePins() {
        //Debug.Log("Raising Pins");
        Pin[] standingPins = GameObject.FindObjectsOfType<Pin>();

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
        Pin [] standingPins = GameObject.FindObjectsOfType<Pin>();

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
        Pin[] standingPins = GameObject.FindObjectsOfType<Pin>();

        foreach (Pin thisPin in standingPins) {
            //Debug.Log("does this work?");
            Rigidbody thisRigidbody = thisPin.GetComponent<Rigidbody>();
            thisRigidbody.velocity = Vector3.zero;
            thisRigidbody.angularVelocity = Vector3.zero;
            thisRigidbody.useGravity = false;
        }
    }

    public void PerformAction(ActionMaster.Action action) {
        if (action == ActionMaster.Action.Tidy) {
            animator.SetTrigger("tidyTrigger");
            pinCounter.ResetLastSettled(false);
        } else if (action == ActionMaster.Action.EndTurn) {
            animator.SetTrigger("resetTrigger");
            pinCounter.ResetLastSettled(true);
        } else { // need to expand to consider other actions, but fine for now
            animator.SetTrigger("resetTrigger");
            pinCounter.ResetLastSettled(true);
        }
    }
}
