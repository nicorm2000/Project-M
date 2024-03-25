using UnityEngine;

public class Guide : NPC, ITalkable
{
    [Header("Dialogue")]
    [SerializeField] private DialogueText dialogueText = null;
    [SerializeField] protected DialogueController dialogueController = null;

    public override void Interact()
    {
        Talk(dialogueText);
    }

    public void Talk(DialogueText dialogueText)
    {
        dialogueController.DisplayNextParagraph(dialogueText);
    }
}