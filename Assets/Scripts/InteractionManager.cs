using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public DialogueTrigger dialogueTrigger;

    void OnMouseDown() {
     dialogueTrigger.triggerDialogue();
  }


}
