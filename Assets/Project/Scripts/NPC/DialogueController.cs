using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class DialogueController : MonoBehaviour
{
    [Header("NPC Info")]
    [SerializeField] private TextMeshProUGUI NPCNameText;
    [SerializeField] private TextMeshProUGUI NPCDialogueText;

    [Header("NPC Info")]
    [SerializeField] private float typingSpeed;

    private Queue<string> _speakerDialogue = new Queue<string>();

    private bool _conversationEnded;
    private bool _isTyping;
    private string _paragraphs;
    private Coroutine _typeDialogueCoroutine;

    private const string HTML_ALPHA = "<color=#00000000>";
    private const float MAX_TYPE_TIME = 0.1f;

    public void DisplayNextParagraph(DialogueText dialogueText)
    {
        if (_speakerDialogue.Count == 0)
        {
            if (!_conversationEnded)
            {
                StartConversation(dialogueText);
            }
            else if (_conversationEnded && !_isTyping)
            {
                EndConversation();
                return;
            }
        }

        if (!_isTyping)
        {
            _paragraphs = _speakerDialogue.Dequeue();

            _typeDialogueCoroutine = StartCoroutine(TypeDialogueText(_paragraphs));
        }
        else
        {
            FinishParagraphEarly();
        }

        if (_speakerDialogue.Count == 0)
        {
            _conversationEnded = true;
        }
    }

    private void StartConversation(DialogueText dialogueText)
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }

        NPCNameText.text = dialogueText.speakerName;

        for (int i = 0; i < dialogueText.dialogueTexts.Length; i++)
        {
            _speakerDialogue.Enqueue(dialogueText.dialogueTexts[i]);
        }
    }

    private void EndConversation()
    {
        _conversationEnded = false;

        if (gameObject.activeSelf)
            gameObject.SetActive(false);
    }

    private IEnumerator TypeDialogueText(string text)
    {
        _isTyping = true;

        NPCDialogueText.text = "";

        string originalText = text;
        string displayedText = "";
        int alphaIndex = 0;

        foreach (char c in text.ToCharArray())
        {
            alphaIndex++;
            NPCDialogueText.text = originalText;

            displayedText = NPCDialogueText.text.Insert(alphaIndex, HTML_ALPHA);
            NPCDialogueText.text = displayedText;

            yield return new WaitForSeconds(MAX_TYPE_TIME / typingSpeed);
        }

        _isTyping = false;
    }

    private void FinishParagraphEarly()
    {
        StopCoroutine(_typeDialogueCoroutine);

        NPCDialogueText.text = _paragraphs;

        _isTyping = false;
    }
}