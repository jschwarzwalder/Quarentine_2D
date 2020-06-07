using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public bool selectSingleRandomString;
    public Dialogue dialogue;
    

    public void triggerDialogue()
    {
        Debug.Log("Trigger Clicked");
        if (selectSingleRandomString)
        {
            FindObjectOfType<DialogueManager>().SelectRandom(dialogue);
            FindObjectOfType<GameStateManager>().incrementMood(dialogue.moodValue);
        }
        else
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            FindObjectOfType<GameStateManager>().incrementMood(dialogue.moodValue);
        }

    }

    void OnMouseDown()
    {
        triggerDialogue();
    }

}
