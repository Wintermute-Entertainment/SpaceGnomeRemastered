using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{

    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject testDialogue;
    [SerializeField] private GameObject dialogueBox;

    private TypeWriterEffect typeWriterEffect;

    private void Start()
    {
        typeWriterEffect = GetComponent<TypeWriterEffect>();

        CloseDialogueBox();
        if (testDialogue != null)
        {
            ShowDialogue(testDialogue);
        }
    }
    public void ShowDialogue(DialogueObject dialogueObject)
    {
        if (dialogueBox != null) { dialogueBox.SetActive(true); }
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }
    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        //  yield return new WaitForSeconds(1);
        if (dialogueObject != null)
        {
            foreach (string dialogue in dialogueObject.Dialogue)
            {
                if (dialogueObject != null)
                {
                    yield return typeWriterEffect.RunTextCoRoutine(dialogue, textLabel);
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                }
            }
        }
        CloseDialogueBox();
    }
    private void CloseDialogueBox()
    {
        if (dialogueBox != null) { dialogueBox.SetActive(false); textLabel.text = string.Empty; }
    }
}
