using UnityEngine;

[CreateAssetMenu(menuName = "NPC/New Dialoue Container")]
public class DialogueText : ScriptableObject
{
    public string speakerName;

    [TextArea(5, 10)]
    public string[] dialogueTexts;
}