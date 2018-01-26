using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private Animator animator; // for GameMaster
    private ActionMaster actionMaster = new ActionMaster();
    private PinCounter pinCounter;

    private Ball ball;

    private List<int> pins = new List<int>();

	// Use this for initialization
	void Start () {
        ball = GameObject.FindObjectOfType<Ball>();
        pinCounter = FindObjectOfType<PinCounter>();
        animator = pinCounter.GetComponent<Animator>(); // for GameMaster


    }

    // Update is called once per frame
    void Update () {
		
	}

    // TODO move to GameMaster when ready
    public void PinsHaveSettled(int pinsFallen) {


        ActionMaster.Action action = actionMaster.Bowl(pinsFallen); // take action based on last round (if first frame)

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

        print(pinsFallen);
        print(action);

        pinCounter.BallLeftBox(false);
        ball.Reset();

    }
}
