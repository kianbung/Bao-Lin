using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// stripped out Monobehaviour from inheritance.. will this cause issues down the line?
public class ActionMaster {

    // the number of bowls we have taken
    private int bowlCount = 1;
    private int[] bowlScores = new int[22]; //21 frames maximum (+1, cause arrays start at 0)
    
    //possible actions
    public enum Action {Tidy, Reset, EndTurn, EndGame};

    // and now you're thinking with portals
    // so this is how you recycle code instead of redoing everything from scratch
    public static Action NextAction(List<int> pinFalls) {
        ActionMaster am = new ActionMaster(); // instantiate new ActionMaster and currentAction, so don't fuck with numbers
        Action currentAction = new Action();

        foreach (int pin in pinFalls) {
            currentAction = am.Bowl(pin); // cycle and loop Bowl(), until the last action is received
        }

        return currentAction;
    }

    private Action Bowl(int pins) { 

        if (pins < 0 || pins > 10) { throw new UnityException("Pins less than zero or more than 10!"); }

        bool strike = (pins == 10);

        // record pins struck into scores first
        bowlScores[bowlCount] = pins;

        if (bowlCount < 19) { // Frames 1-9 (18 bowls total)
            
            /* Legacy code - strike detection moved for better logic at second half of frame 
            if (strike) {

                // checks to see if it's final frame, strike on final frame resets for final bowl
                if (bowlCount == 19) {
                    bowlCount += 1; // you do a bowl 20 + 21 if strike on first bowl of final frame
                    return Action.Reset;
                }

                bowlCount += 2; // skip a bowlCount, directly to next frame
                return Action.EndTurn;
            }
            */

            // % returns the remainder between the two - check if we in a new frame or not
            if (bowlCount % 2 != 0) { // check if first half of frame
                if (strike) {
                    bowlCount += 2; // skip to next frame (end turn) if strike
                    return Action.EndTurn;
                }
                bowlCount++;
                return Action.Tidy;
            } else { // if second half of frame
                bowlCount++;
                return Action.EndTurn;
            }
        } else if (bowlCount == 19) {
            bowlCount++;
            if (strike) {return Action.Reset;}
            return Action.Tidy;
        } else if (bowlCount == 20) {

            // check if end game, or spare
            if ((bowlScores[bowlCount] + bowlScores[bowlCount - 1]) < 10) {
                return Action.EndGame;
            }

            if (bowlScores[bowlCount - 1] == 10 && !strike) { // if previous bowl was strike and current bowl is not strike, tidy instead of reset
                return Action.Tidy;
            }

            bowlCount++;
            return Action.Reset;
        } else if (bowlCount == 21) {
            return Action.EndGame;
        } else {
            throw new UnityException("Bowl number exceeds 21.");
        }
    }

    public int ScoreCheck(int bowl) {
        if (bowl < 1 || bowl > 21) {
            throw new UnityException("Checking for score outside of accepted range: 1-21");
        }

        return bowlScores[bowl];
    }


    /* lmao kaypo redo everything
    public static Action NextAction(List<int> pinFalls) {

        int currentBowlIndex = pinFalls.Count-1; //convert here first, make life easier later; index starts from 0
        int currentBowlCount = pinFalls.Count;

        if (currentBowlIndex < 0 || currentBowlIndex > 20) {
            throw new UnityException("Number of bowls out of expected range (0-20) - remember that index starts from 0");
        }

        bool strike = (pinFalls[currentBowlIndex] == 10);



        if (currentBowlCount < 19) { // for frames 1-9
            if (currentBowlCount % 2 != 0) { // check if first bowl of frame
                if (strike) {
                    return Action.EndTurn; // skip second bowl of frame if strike
                }
                return Action.Tidy;
            } else { //second bowl of frame
                return Action.EndTurn;
            }
        } else if (currentBowlCount == 19) {
            if (strike) {
                return Action.Reset;
            }
            return Action.Tidy;
        } else if (currentBowlCount == 20) {
            //TODO oh shit I lazy to think
            // Copypasta from above code
            
            // check if end game, or spare
            if ((pinFalls[currentBowlIndex] + pinFalls[currentBowlIndex - 1]) < 10) { // if last two bowls in 10th frame not spare, end game
                return Action.EndGame;
            }

            if (pinFalls[currentBowlIndex - 1] == 10 && !strike) { // if previous bowl was strike and current bowl is not strike, tidy instead of reset
                return Action.Tidy;
            }
            
            return Action.Reset;

        } else if (currentBowlCount == 21) {
            return Action.EndGame;
        }

        throw new UnityException("wtf nigga");
    } */
}
