using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {

    

    public float distanceToRaisePins = 40;
    public GameObject pinSet;

    
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
}
