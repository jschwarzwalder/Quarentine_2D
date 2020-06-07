using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameStateManager : MonoBehaviour
{
    private int moodScore;
    private int time;
    private int day;

    [SerializeField] private Dialogue openingText;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private GameObject moodCanvas;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip neutralMusic;
    [SerializeField] private AudioClip lowMoodMusic;
    [SerializeField] private AudioClip EndOfDayChime;
    [SerializeField] private Text dayTextWhite;
    [SerializeField] private Text dayTextBlack;
    [SerializeField] private Text timeTextWhite;
    [SerializeField] private Text timeTextBlack;

    [SerializeField] private static int STARTING_SCORE = 50;
    [SerializeField] private static int UPPER_MOOD_THRESHOLD = 100;
    [SerializeField] private static int LOWER_MOOD_THRESHOLD = 0;
    [SerializeField] private static float AUDIO_VOLUME = .75f;

    [SerializeField] private static int STARTING_DAY = 1;
    [SerializeField] private static int STARTING_TIME = 8; // 8 am Military time
    [SerializeField] private static int END_OF_DAY_THRESHOLD = 21; // 8pm in Military time

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
        time = STARTING_TIME;
        day = STARTING_DAY;

        dialogueManager.StartDialogue(openingText);
        displayDay();
        displayTime();
        moodBar.SetMaxMood(UPPER_MOOD_THRESHOLD);
        moodBar.SetMood(moodScore);
        gameIsActive = true;
        pauseMenuUI.SetActive(false);
        audioSource.clip = neutralMusic;
        audioSource.volume = AUDIO_VOLUME;
        audioSource.Play();
    }

    public void incrementMood(int moodValue)
    {
        if (day < STARTING_DAY)
        {
            day = STARTING_DAY;
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
            audioSource.volume = audioSource.volume / 50;
        }
        else if (moodScore <= LOWER_MOOD_THRESHOLD)
        {
            endGame(PossibleEndStates.Lose);
            audioSource.volume = audioSource.volume / 50;
        }
        else if (moodScore < 50)
        {
            if (audioSource.clip == neutralMusic)
            {
                audioSource.clip = lowMoodMusic;
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.clip == lowMoodMusic)
            {
                audioSource.clip = neutralMusic;
                audioSource.Play();
                
            }
        }

    }

    private void endDay()
    {
        Dialogue newDayDialogue = new Dialogue();
        string[] endDaySentences = new string[3];

        string endOfDayMessage = "The day has ended! Resetting the day, but keeping the score. >>";
        endDaySentences[0] = endOfDayMessage;
        Debug.Log(endOfDayMessage);
        day = day + 1;
        time = STARTING_TIME;
        string newDayMessage = "Day " + day + ". Time is now: " + time + " Score is still: " + moodScore + ">>";
        endDaySentences[1] = newDayMessage;
        Debug.Log(newDayMessage);

        endDaySentences[2] = "Click on an Item to Interact";
        newDayDialogue.sentences = endDaySentences;
        dialogueManager.StartDialogue(newDayDialogue);
        displayDay();

        resetDailyInteractions();
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
        else if (time == 12)
        {
            Debug.Log("It is now " + time + " pm.");
            timeTextWhite.text = time + " pm";
            timeTextBlack.text = time + " pm";
        }
        else
        {
            Debug.Log("It is now " + time + " am.");
            timeTextWhite.text = time + " am";
            timeTextBlack.text = time + " am";
        }

    }

    private void resetDailyInteractions()
    {
        DialogueTrigger[] interactables = Object.FindObjectsOfType<DialogueTrigger>();
        foreach (DialogueTrigger interactable in interactables)
        {
            interactable.resetDay();
        }
    }

    private void endGame(PossibleEndStates endState)
    {
        gameIsActive = false;

        Dialogue endGameDialogue = new Dialogue();
        string[] endGameSentences = new string[3];

        switch (endState)
        {
            case PossibleEndStates.Win:
                Debug.Log("You won! Resetting score...");
                endGameSentences[0] = "Congratulations, You Won! >>";
                endGameSentences[1] = "You lasted " + day + " days. >>";
                endGameSentences[2] = "Your final mood score was " + moodScore + ".";
                endGameDialogue.sentences = endGameSentences;
                dialogueManager.StartDialogue(endGameDialogue);
                break;
            case PossibleEndStates.Lose:
                Debug.Log("Oops, you lose. Resetting score...");
                endGameSentences[0] = "Unfortunately, You Lost! >>";
                endGameSentences[1] = "You lasted " + day + " days. >>";
                endGameSentences[2] = "Your final mood score was " + moodScore + ".";
                endGameDialogue.sentences = endGameSentences;
                dialogueManager.StartDialogue(endGameDialogue);
                break;
            default:
                Debug.Log("Hmm.... you seem to have ended the game in an unexpected way. Resetting score...");
                break;
        }

    }

    public void resetScore()
    {

        Dialogue restartGameDialogue = new Dialogue();
        string[] restartSentences = new string[4];

        restartSentences[0] = "Restarting from Day 1.";
        restartSentences[1] = "You lasted " + day + " days. >>";
        restartSentences[2] = "Your mood score was " + moodScore + ".";

        moodScore = STARTING_SCORE;
        day = STARTING_DAY;
        time = STARTING_TIME;
        Debug.Log("Score updated to: " + moodScore);
        restartSentences[2] = "Score reset to " + moodScore + ".";
        gameIsActive = true;
        displayDay();
        displayTime();


        restartGameDialogue.sentences = restartSentences;
        dialogueManager.StartDialogue(restartGameDialogue);
        pauseMenuUI.SetActive(false);
        resetDailyInteractions();

        audioSource.clip = neutralMusic;
        audioSource.volume = AUDIO_VOLUME;
        audioSource.Play(0);
    }

    public bool isGameActive()
    {
        return gameIsActive;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape Pressed");
            if (gameIsActive)
            {
                gameIsActive = false;
            }
            else
            {
                Resume();
            }

            if (gameIsActive != true)
            {
                Pause();
            }
        }


    }

    public void Resume()
    {
        Debug.Log("Game Resumed");
        pauseMenuUI.SetActive(false);
        gameIsActive = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void Pause()
    {
        Debug.Log("Game Paused");
        pauseMenuUI.SetActive(true);
        gameIsActive = false;
    }

}
