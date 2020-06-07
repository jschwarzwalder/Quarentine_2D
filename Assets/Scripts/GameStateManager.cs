using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour
{
    private int moodScore;
    private int time;
    private int day;

    [SerializeField] private Dialogue openingText;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private Dialogue newDayDialogue;
    [SerializeField] private Dialogue endGameDialogue;
    [SerializeField] private GameObject moodCanvas;
    [SerializeField] private Text dayTextWhite;
    [SerializeField] private Text dayTextBlack;
    [SerializeField] private Text timeTextWhite;
    [SerializeField] private Text timeTextBlack;

    [SerializeField] private static int STARTING_SCORE = 50;
    [SerializeField] private static int UPPER_MOOD_THRESHOLD = 100;
    [SerializeField] private static int LOWER_MOOD_THRESHOLD = 0;

    [SerializeField] private static int START_OF_DAY = 8; // 8 am Military time
    [SerializeField] private static int END_OF_DAY_THRESHOLD = 20; // 8pm in Military time

    private bool gameIsActive;
    private MoodBar moodBar;



    private enum PossibleEndStates
    {
        Win,
        Lose
    }

    // Start is called before the first frame update
    void Start()
    {
        moodScore = STARTING_SCORE;
        moodBar = moodCanvas.GetComponent<MoodBar>();
        time = 8;
        Debug.Log(time);
        day = 1;
        Debug.Log(day);

        dialogueManager.StartDialogue(openingText);
        displayDay();
        displayTime();
        moodBar.SetMaxMood(UPPER_MOOD_THRESHOLD);
        moodBar.SetMood(moodScore);
        gameIsActive = true;
        

    }

    public void incrementMood(int moodValue)
    {
        if (day < 1)
        {
            day = 1;
            displayDay();
        }

        moodScore += moodValue;
        Debug.Log("Incrementing moodValue: " + moodValue + " New score: " + moodScore);
        time += 1;
        if (time >= END_OF_DAY_THRESHOLD)
        {
            endDay();
        }

        displayTime();
        moodBar.SetMood(moodScore);

        if (moodScore >= UPPER_MOOD_THRESHOLD)
        {
            endGame(PossibleEndStates.Win);
        }
        else if (moodScore <= LOWER_MOOD_THRESHOLD)
        {
            endGame(PossibleEndStates.Lose);
        }

    }

    private void endDay()
    {

        string endOfDayMessage = "The day has ended! Resetting the day, but keeping the score. >>";
        newDayDialogue.sentences[0] = endOfDayMessage;
        Debug.Log(endOfDayMessage);
        day = day + 1;
        time = START_OF_DAY;
        string newDayMessage = "Day " + day + ". Time is now: " + time + " Score is still: " + moodScore + ">>";
        newDayDialogue.sentences[1] = newDayMessage;
        Debug.Log(newDayMessage);

        newDayDialogue.sentences[2] = "Click on an Item to Interact";
        dialogueManager.StartDialogue(newDayDialogue);
        displayDay();
    }

    private void displayDay()
    {
        dayTextWhite.text = "Day " + day;
        dayTextBlack.text = "Day " + day;
    }

    private void displayTime()
    {

        if (time > 12)
        {
            Debug.Log("It is now " + (time - 12) + " pm.");
            int hour = time - 12;
            timeTextWhite.text = hour + " pm";
            timeTextBlack.text = hour + " pm";
        }
        else
        {
            Debug.Log("It is now " + time + " am.");
            timeTextWhite.text = time + " am";
            timeTextBlack.text = time + " am";
        }

    }

    private void endGame(PossibleEndStates endState)
    {
        gameIsActive = false;

        switch (endState)
        {
            case PossibleEndStates.Win:
                Debug.Log("You won! Resetting score...");
                endGameDialogue.sentences[0] = "Congratulations you Won! >>";
                endGameDialogue.sentences[1] = "You lasted " + day + " days. >>";
                endGameDialogue.sentences[2] = "Your final mood score was " + moodScore + ".";
                dialogueManager.StartDialogue(endGameDialogue);
                break;
            case PossibleEndStates.Lose:
                Debug.Log("Oops, you lose. Resetting score...");
                endGameDialogue.sentences[0] = "Unfortuallly you Lost! >>";
                endGameDialogue.sentences[1] = "You lasted " + day + " days. >>";
                endGameDialogue.sentences[2] = "Your final mood score was " + moodScore + ".";
                dialogueManager.StartDialogue(endGameDialogue);
                break;
            default:
                Debug.Log("Hmm.... you seem to have ended the game in an unexpected way. Resetting score...");
                break;
        }
        
    }

    private void resetScore()
    {
        moodScore = STARTING_SCORE;
        day = 0;
        Debug.Log("Score updated to: " + moodScore);
    }

    public bool isGameActive()
    {
        return gameIsActive; 
    }

}
