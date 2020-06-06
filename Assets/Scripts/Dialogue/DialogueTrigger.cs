using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;

	public void triggerDialogue ()
	{
        Debug.Log("Trigger Clicked");
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}

    void OnMouseDown() {
        triggerDialogue();
    }

}
