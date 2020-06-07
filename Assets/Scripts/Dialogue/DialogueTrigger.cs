using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private bool selectSingleRandomString;
    [SerializeField] private int maxInteractionsPerDay;
    [SerializeField] private Dialogue dialogue;

    private DialogueManager dialogueManager;
    private GameStateManager gameStateManager;
    private int interactionToday;

    void Start()
    {
        gameStateManager = FindObjectOfType<GameStateManager>();
        dialogueManager = FindObjectOfType<DialogueManager>();
    }


    public void triggerDialogue()
    {
     

        Debug.Log("Trigger Clicked");
        if (selectSingleRandomString)
        {
            dialogueManager.SelectRandom(dialogue);
            gameStateManager.incrementMood(dialogue.moodValue);
            

        }
        else
        {
            dialogueManager.StartDialogue(dialogue);
            gameStateManager.incrementMood(dialogue.moodValue);
        }

    }

    void OnMouseDown()
    {

        if (gameStateManager.isGameActive())
        {
            if (interactionToday < maxInteractionsPerDay)
            {
                interactionToday += 1;
                triggerDialogue();
            }
        }
        
    }

    public void resetDay()
    {
        interactionToday = 0;
    }

}
