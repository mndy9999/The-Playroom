using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Dialogue", menuName = "Cinematics/Dialogue")]
public class DialogueTrigger : Cinematic
{
    public bool triggerOnEnable = true;
    public Dialogue[] dialogue;

    private void Awake()
    {
        if (triggerOnEnable)
            TriggerDialogue();
    }

    public override IEnumerator Play()
    {
        yield return new WaitForSeconds(delay);
        TriggerDialogue();
    }

    public void TriggerDialogue()
    {
        if(DialogueManager.Instance != null)
            DialogueManager.Instance.StartDialogue(dialogue);
    }

}
