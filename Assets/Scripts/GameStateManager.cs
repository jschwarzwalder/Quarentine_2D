using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public int moodScore;
    public int time;

    private static int STARTING_SCORE = 50;
    private static int UPPER_MOOD_THRESHOLD = 100;
    private static int LOWER_MOOD_THRESHOLD = 0;

    private static int START_OF_DAY = 8; // 8 am Military time
    private static int END_OF_DAY_THRESHOLD = 20; // 8pm in Military time


    private enum PossibleEndStates
    {
        Win, 
        Lose
    }

    // Start is called before the first frame update
    void Start()
    {
        moodScore = STARTING_SCORE;
        time = 8;
    }

    public void incrementMood(int moodValue){

        moodScore += moodValue;
        Debug.Log("Incrementing moodValue: " + moodValue + " New score: " + moodScore);

        time += 1;
        if( time >= END_OF_DAY_THRESHOLD){
            endDay();
        }

        displayTime();

        if(moodScore >= UPPER_MOOD_THRESHOLD){
            endGame(PossibleEndStates.Win);
        } else if (moodScore <= LOWER_MOOD_THRESHOLD){
            endGame(PossibleEndStates.Lose);
        }

    }

    private void endDay(){
        Debug.Log("The day has ended! Resetting the day, but keeping the score.");
        time = START_OF_DAY;
        Debug.Log("Time is now: " + time + " Score is still: " + moodScore);
    }

    private void displayTime(){

        if(time > 12){
            Debug.Log("It is now " + (time - 12) + " pm.");
        } else {
            Debug.Log("It is now " + time + " am.");
        }
        
    }

    private void endGame(PossibleEndStates endState){

        switch (endState)
        {
            case PossibleEndStates.Win:
                Debug.Log("You won! Resetting score...");
                resetScore();
                break;
            case PossibleEndStates.Lose:
                Debug.Log("Oops, you lose. Resetting score...");
                resetScore();
                break;
            default:
                Debug.Log("Hmm.... you seem to have ended the game in an unexpected way. Resetting score...");
                resetScore();
                break;
        }
        

    }

    private void resetScore(){
        moodScore = STARTING_SCORE;
        Debug.Log("Score updated to: " + moodScore);
    }

}
