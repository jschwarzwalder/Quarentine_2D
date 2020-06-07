using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public bool selectSingleRandomString;
    public Dialogue dialogue;

    private DialogueManager dialogueManager;
    private GameStateManager gameStateManager;

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
            triggerDialogue();
        }
        
    }

}
