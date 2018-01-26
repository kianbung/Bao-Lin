using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq; // advanced list stuff - specifically, for converting Arrays .ToList

public class ActionMasterTest {

    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
    private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
    private ActionMaster.Action reset = ActionMaster.Action.Reset;
    private ActionMaster.Action endGame = ActionMaster.Action.EndGame;

    private List<int> testBowl;


    // Gotta use setup so that I reset the ActionMaster script every test run. Else fucking vars get incremented
    [SetUp]
    public void Setup() {
        testBowl = new List<int>();
    }
   
    
    [Test]
    public void T01OneStrikeEndsTurn() {
        testBowl.Add(10);
        Assert.AreEqual(endTurn, ActionMaster.NextAction(testBowl));
    }

    [Test]
    public void T02Bowl8ReturnsTidy() {
        testBowl.Add(8);
        Assert.AreEqual(tidy, ActionMaster.NextAction(testBowl));
    }

    [Test]
    public void T03Bowl2SpareReturnsEndTurn() {
        testBowl.Add(8);
        testBowl.Add(2);
        Assert.AreEqual(endTurn, ActionMaster.NextAction(testBowl));
    }

    [Test]
    public void T04BowlTenFramesEndGame() {
        
        int count = 0;
        while (count < 20) {
            testBowl.Add(5);
            count++;
        }
        testBowl.Add(1);
        Assert.AreEqual(endGame, ActionMaster.NextAction(testBowl));
    }

    [Test]
    public void T05TenthFrameSecondBowlSpareReset() {
        int count = 0;
        while (count < 19) {
            testBowl.Add(5);
            count++;
        }
        testBowl.Add(5);
        Assert.AreEqual(reset, ActionMaster.NextAction(testBowl));
    }

    // next test: if no spare on second bowl in tenth frame, end game
    [Test]
    public void T06TenthFrameSceondBowlNoSpareEndGame() {
        int count = 0;
        while (count < 19) {
            testBowl.Add(5);
            count++;
        }

        testBowl.Add(0);

        Assert.AreEqual(endGame, ActionMaster.NextAction(testBowl));
    }

    [Test]
    public void T07ThirdFrameFirstBowlTidy() {
        testBowl.Add(0);
        testBowl.Add(0);// first frame
        testBowl.Add(0);
        testBowl.Add(0);// second frame
        testBowl.Add(3);
        Assert.AreEqual(tidy, ActionMaster.NextAction(testBowl));
    }

    [Test]
    public void T08StrikeAtBeginningOfLastFrameReset() {
        int count = 0;
        while (count < 18) {
            testBowl.Add(5);
            count++;
        }
        testBowl.Add(10);
        Assert.AreEqual(reset, ActionMaster.NextAction(testBowl));
    }

    [Test]
    public void T09LastFrameStrikeThenBowl20Tidy() {
        //if strike, and then cacat bowl after that strike on last frame, should be tidy for final (21st) bowl
        int count = 0;
        while (count < 18) {
            testBowl.Add(5);
            count++;
        }
        testBowl.Add(10); // at start of tenth frame
        testBowl.Add(5);
        Assert.AreEqual(tidy, ActionMaster.NextAction(testBowl));
    }

    // what if strikes on bowls 19 and 20?
    [Test]
    public void T10LastFrameTwoStrikesReset() {
        int count = 0;
        while (count < 18) {
            testBowl.Add(8);
            count++;
        }
        testBowl.Add(10);
        testBowl.Add(10);
        Assert.AreEqual(reset, ActionMaster.NextAction(testBowl));

    }

    [Test]
    public void T11LastFrameStrikeThenBowl20TidyIfGutter() {
        //if strike, and then cacat bowl after that strike on last frame, should be tidy for final (21st) bowl
        int count = 0;
        while (count < 18) {
            testBowl.Add(8);
            count++;
        }
        testBowl.Add(10);
        testBowl.Add(0);
        Assert.AreEqual(tidy, ActionMaster.NextAction(testBowl));
    }

    //[Test] no longer needed, writing scoremaster
    //public void T12TestEmpytScore() {
    //    Assert.AreEqual(0, actionMaster.ScoreCheck(1));
    //}

    //[Test] no longer needed, writing scoremaster
    //public void T13CheckIfScoresAndBowlAlign() {
    //    int count = 0;
    //    while (count < 18) {
    //        testBowl.Add(5);
    //        count++;
    //    }
    //    testBowl.Add(10); // bowl 19
    //    testBowl.Add(0); // bowl 20
    //    Assert.AreEqual(10, actionMaster.ScoreCheck(19)); // check bowl 19's score
    //}

    [Test]
    public void T14If10PinSpareOnSecondBowlIncrementOne() {
        testBowl.Add(0);
        testBowl.Add(10); // end of frame 1
        testBowl.Add(5); // start of frame 2, should tidy after bowl
        Assert.AreEqual(tidy, ActionMaster.NextAction(testBowl)); 
    }

    [Test]
    public void T15Dondi10thFrameTurkey() {
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        testBowl = rolls.ToList(); // finally get to use advanced list function
        testBowl.Add(10);
        Assert.AreEqual(reset, ActionMaster.NextAction(testBowl));

        testBowl.Add(10);
        Assert.AreEqual(reset, ActionMaster.NextAction(testBowl));

        testBowl.Add(10);
        Assert.AreEqual(endGame, ActionMaster.NextAction(testBowl));
    }

    [Test]
    public void T16BowlZeroThenOne() {
        testBowl.Add(0);
        testBowl.Add(1);
        Assert.AreEqual(endTurn, ActionMaster.NextAction(testBowl));
    }

    [Test]
    public void TB01StrikeTest() {

        testBowl.Add(10);
        Assert.AreEqual(endTurn, ActionMaster.NextAction(testBowl));
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
