using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;

	public void triggerDialogue ()
	{
        FindObjectOfType<GameStateManager>().incrementMood(dialogue.moodValue);
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}

    void OnMouseDown() {
        triggerDialogue();
    }

}
