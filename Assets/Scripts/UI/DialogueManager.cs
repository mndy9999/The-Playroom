using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager mInstance;
    public static DialogueManager Instance
    {
        get
        {
            if (mInstance == null)
                mInstance = FindObjectOfType<DialogueManager>();
            return mInstance;
        }
    }

    Queue<Dialogue> sentences = new Queue<Dialogue>();

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public Animator animator;

    private bool dialogueStarted;

   public void StartDialogue(Dialogue[] dialogue)
    {
        dialogueStarted = true;
        animator.SetBool("isOpen", true);
        CinematicsManager.Instance.CanContinue = false;
        sentences.Clear();

        foreach(var d in dialogue)
        {
            sentences.Enqueue(d);
        }

        DisplayNextSentence();
    }

    private Dialogue sentenceDialogue;

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        sentenceDialogue = sentences.Dequeue();
        nameText.text = sentenceDialogue.Name;

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentenceDialogue.Sentences));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(var letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    private void EndDialogue()
    {
        dialogueStarted = false;
        animator.SetBool("isOpen", false);
        CinematicsManager.Instance.DisplayNextSentence();
        CinematicsManager.Instance.CanContinue = true;
    }

    private void LateUpdate()
    {
        if (dialogueStarted && Input.GetKeyDown(KeyCode.Space))
        {
            if (dialogueText.text == sentenceDialogue.Sentences)
                DisplayNextSentence();
        }           
    }
}
