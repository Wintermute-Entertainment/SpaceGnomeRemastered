
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]

public class DialogueObject : ScriptableObject
{
    [SerializeField][TextArea] string[] dialogueStringArray;

    public string[] Dialogue => dialogueStringArray;
}
