using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

public class ActionMasterTest {

    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
    private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
    private ActionMaster.Action reset = ActionMaster.Action.Reset;
    private ActionMaster.Action endGame = ActionMaster.Action.EndGame;
    private ActionMaster actionMaster;
    private List<int> testBowl;


    // Gotta use setup so that I reset the ActionMaster script every test run. Else fucking vars get incremented
    [SetUp]
    public void Setup() {
        actionMaster = new ActionMaster();
        testBowl = new List<int>();
    }
   
    
    [Test]
    public void T01OneStrikeEndsTurn() {
       
        Assert.AreEqual(endTurn, actionMaster.Bowl(10));
    }

    [Test]
    public void T02Bowl8ReturnsTidy() {
        Assert.AreEqual(tidy, actionMaster.Bowl(8));
    }

    [Test]
    public void T03Bowl2SpareReturnsEndTurn() {
        actionMaster.Bowl(8);
        Assert.AreEqual(endTurn, actionMaster.Bowl(2));
    }

    [Test]
    public void T04BowlTenFramesEndGame() {
        int count = 0;
        while (count < 20) {
            actionMaster.Bowl(8);
            count++;
        }
        Assert.AreEqual(endGame, actionMaster.Bowl(1));
    }

    [Test]
    public void T05TenthFrameSecondBowlSpareReset() {
        int count = 0;
        while (count < 19) {
            actionMaster.Bowl(8);
            count++;
        }
        Assert.AreEqual(reset, actionMaster.Bowl(2));
    }

    // next test: if no spare on second bowl in tenth frame, end game
    [Test]
    public void T06TenthFrameSceondBowlNoSpareEndGame() {
        int count = 0;
        while (count < 19) {
            actionMaster.Bowl(8);
            count++;
        }
        Assert.AreEqual(endGame, actionMaster.Bowl(0));
    }

    [Test]
    public void T07ThirdFrameFirstBowlTidy() {
        actionMaster.Bowl(0);
        actionMaster.Bowl(0); // first frame
        actionMaster.Bowl(0);
        actionMaster.Bowl(0); // second frame
        Assert.AreEqual(tidy, actionMaster.Bowl(3));
    }

    [Test]
    public void T08StrikeAtBeginningOfLastFrameReset() {
        int count = 0;
        while (count < 18) {
            actionMaster.Bowl(8);
            count++;
        }
        Assert.AreEqual(reset, actionMaster.Bowl(10));
    }

    [Test]
    public void T09LastFrameStrikeThenBowl20Tidy() {
        //if strike, and then cacat bowl after that strike on last frame, should be tidy for final (21st) bowl
        int count = 0;
        while (count < 18) {
            actionMaster.Bowl(8);
            count++;
        }
        actionMaster.Bowl(10);
        Assert.AreEqual(tidy, actionMaster.Bowl(5));
    }

    // what if strikes on bowls 19 and 20?
    [Test]
    public void T10LastFrameTwoStrikesReset() {
        int count = 0;
        while (count < 18) {
            actionMaster.Bowl(8);
            count++;
        }
        actionMaster.Bowl(10);
        Assert.AreEqual(reset, actionMaster.Bowl(10));

    }

    [Test]
    public void T11LastFrameStrikeThenBowl20TidyIfGutter() {
        //if strike, and then cacat bowl after that strike on last frame, should be tidy for final (21st) bowl
        int count = 0;
        while (count < 18) {
            actionMaster.Bowl(8);
            count++;
        }
        actionMaster.Bowl(10);
        Assert.AreEqual(tidy, actionMaster.Bowl(0));
    }

    [Test]
    public void T12TestEmpytScore() {
        Assert.AreEqual(0, actionMaster.ScoreCheck(1));
    }

    [Test]
    public void T13CheckIfScoresAndBowlAlign() {
        int count = 0;
        while (count < 18) {
            actionMaster.Bowl(8);
            count++;
        }
        actionMaster.Bowl(10); // bowl 19
        actionMaster.Bowl(0); // bowl 20
        Assert.AreEqual(10, actionMaster.ScoreCheck(19)); // check bowl 19's score
    }

    [Test]
    public void T14If10PinSpareOnSecondBowlIncrementOne() {
        actionMaster.Bowl(0);
        actionMaster.Bowl(10); // end of frame 1
        Assert.AreEqual(tidy, actionMaster.Bowl(5)); // start of frame 2, should tidy after bowl
    }

    [Test]
    public void T15Dondi10thFrameTurkey() {
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        foreach (int roll in rolls) {
            actionMaster.Bowl(roll);
        }
        Assert.AreEqual(reset, actionMaster.Bowl(10));
        Assert.AreEqual(reset, actionMaster.Bowl(10));
        Assert.AreEqual(endGame, actionMaster.Bowl(10));
    }

    [Test]
    public void T16BowlZeroThenOne() {
        actionMaster.Bowl(0);
        Assert.AreEqual(endTurn, actionMaster.Bowl(1));
    }

    [Test]
    public void TB01StrikeTest() {
        
        testBowl.Add(10);
        Assert.AreEqual(endTurn,ActionMaster.NextAction(testBowl));
    }

    [Test]
    public void TB02FirstBowl1Tidy() {
        testBowl.Add(1);
        Assert.AreEqual(tidy, ActionMaster.NextAction(testBowl));
    }

    [Test]
    public void TB03Bowl2SpareReturnsEndTurn() {
        testBowl.Add(8);
        testBowl.Add(2);
        Assert.AreEqual(endTurn, ActionMaster.NextAction(testBowl));
    }

    [Test]
    public void TB04Bowl21EndGame() {
        int count = 0;
        while (count < 21) {
            testBowl.Add(5);
            count++;
        }
        Assert.AreEqual(endGame, ActionMaster.NextAction(testBowl));
    }

    [Test]
    public void TB05TenthFrameSecondBowlSpareReset() {
        int count = 0;
        while (count < 20) {
            testBowl.Add(5);
            count++;
        }
        Assert.AreEqual(reset, ActionMaster.NextAction(testBowl));
    }

    
}
