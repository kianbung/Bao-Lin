using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinShredder : MonoBehaviour {

    private void OnTriggerExit(Collider other) {

        // lolol, gotta watch out for where the collider is placed
        if (other.GetComponent<Pin>()) {
            Destroy(other.gameObject);
        }

    }
}
