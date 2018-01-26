using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private PinSetter pinSetter;
    private Ball ball;

    private List<int> scoreList = new List<int>();

	// Use this for initialization
	void Start () {
        ball = GameObject.FindObjectOfType<Ball>();
        pinSetter = GameObject.FindObjectOfType<PinSetter>();

    }

    // Update is called once per frame
    void Update () {
		
	}

    // TODO move to GameMaster when ready
    public void PinsHaveSettled(int pinsFallen) {

        scoreList.Add(pinsFallen);

        //ActionMaster.Action action = actionMaster.Bowl(pinsFallen); // take action based on last round (if first frame)

        ActionMaster.Action action = ActionMaster.NextAction(scoreList); // variable size scorelist??! holy shit does that work?

        pinSetter.PerformAction(action);

        print(pinsFallen);
        print(action);

        ball.Reset();

    }
}
