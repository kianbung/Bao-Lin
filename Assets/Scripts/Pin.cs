using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {

    public float standingThreshold;
    private Quaternion startQuaternion;

	// Use this for initialization
	void Start () {
        startQuaternion = gameObject.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public bool IsStanding() {

        /* WTF DOESN'T WORK CAUSE OF WEIRD ROTATIONS SHOWING -1
            if (Mathf.Abs(transform.eulerAngles.x) < standingThreshold && Mathf.Abs(transform.eulerAngles.y) < standingThreshold && Mathf.Abs(transform.eulerAngles.z) < standingThreshold) {
                print(gameObject.name + " is standing");
                return true;
            } */

        /* simplified solution from forums. 
            if (Vector3.Angle(transform.up, Vector3.up) < standingThreshold) {
                print(name + " is standing");
                return true;
            } */

        // This seems to be the most correct (and advanced) answer in forums.
        // ACTUALLY: the above sohai solution works fucking fine.
        // After all this 4D science, it still detects (up) wrongly if spinning around in space
        // Ignores Y axis rotation by multiplying current eulers by 1 except Y axis
        Vector3 eulerWithoutTwist = Vector3.Scale(transform.eulerAngles, new Vector3(1, 0, 1));

        // Documentation is confusing is fuck, but basically converts Euler to Queternions
        Quaternion pinQuaternion = Quaternion.Euler(eulerWithoutTwist);

        // Gets the tilt angle (difference between the current rotation and startingRotation)
        // returns a nice clean angle irrespective of direction (no pesky 359 degrees returned)
        float tiltAngle = Quaternion.Angle(pinQuaternion, startQuaternion);

        // compare the tilt angle against standingThreshold
        if (tiltAngle < standingThreshold) {
            //print(name + " is standing");
            return true;
        }

        //print(name + " has fallen");
        return false;
    }
}
