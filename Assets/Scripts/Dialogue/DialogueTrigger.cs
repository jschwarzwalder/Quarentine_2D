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
        }
        else
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }

    }

    void OnMouseDown()
    {
        triggerDialogue();
    }

}
