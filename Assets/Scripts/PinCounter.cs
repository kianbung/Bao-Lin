using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinCounter : MonoBehaviour {

    private int lastStandingCount = -1;
    private float lastChangeTime;
    private int lastSettledCount = 10;



    private Pin[] standingPins;
    private Text pinCounterText;
    private bool ballLeftBox = false;

    // Use this for initialization
    void Start () {
        standingPins = GameObject.FindObjectsOfType<Pin>();
        pinCounterText = GameObject.Find("PinCount").GetComponent<Text>();


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
            ChangeTextColor(Color.green);

            GameManager gm = FindObjectOfType<GameManager>();
            int pinsFallen = lastSettledCount - lastStandingCount;

            gm.PinsHaveSettled(pinsFallen);
            lastStandingCount = -1;

        }
    }

    public void ResetLastSettled(bool reset) {
        if (reset) {
            lastSettledCount = 10;
        } else {
            lastSettledCount = lastStandingCount;
        }
    }

    public void BallLeftBox(bool set) {
        ballLeftBox = set;
    }

    public void ChangeTextColor(Color color) {
        pinCounterText.color = color;

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
            ChangeTextColor(Color.red);
            lastChangeTime = 0;
        }

        //Debug.Log(other.name + " exit collider");
    }
}
