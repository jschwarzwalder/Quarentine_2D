using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{

    public Text dialogueTextWhite;
    public Text dialogueTextBlack;

    public Animator animator;





    private Queue<string> sentenceQueue;

    // Use this for initialization
    void Start()
    {
        sentenceQueue = new Queue<string>();
        animator.SetBool("isOpen", true);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        

        if (sentenceQueue.Count > 0)
        {

            sentenceQueue.Clear();
        }

        foreach (string sentence in dialogue.sentences)
        {
            sentenceQueue.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void SelectRandom(Dialogue dialogue)
    {
        Debug.Log("Starting dialogue");
        animator.SetBool("isOpen", true);

        string[] sentences = dialogue.sentences;

        sentenceQueue.Clear();

        int length = sentences.Length;

        string sentence = sentences[Random.RandomRange(0, length)];

        StopAllCoroutines();
        StartCoroutine(TypeSentence(" " + sentence));

        EndDialogue();
        return;

    }

    public void DisplayNextSentence()
    {
        if (sentenceQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentenceQueue.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueTextBlack.text = " ";
        dialogueTextWhite.text = " ";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueTextBlack.text += letter;
            dialogueTextWhite.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        dialogueTextBlack.text = "";
        dialogueTextWhite.text = "";
        animator.SetBool("isOpen", false);
    }

}