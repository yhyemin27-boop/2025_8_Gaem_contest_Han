
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class dialogueEDManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    private DialogueData currentDialogue;
    private int index = 0;
    public bool isTalking = false;

    private HashSet<DialogueData> finishedDialogues = new HashSet<DialogueData>();

    void Update()
    {
        if (isTalking && Input.GetKeyDown(KeyCode.Space))
        {
            NextLine();
        }
    }

    public void StartDialogue(DialogueData data, bool forceStart = false)
    {
        if (finishedDialogues.Contains(data) && !forceStart)
        {
            Debug.Log("[DEBUG] 이 대화는 이미 끝났습니다.");
            return;
        }

        finishedDialogues.Add(data);
        currentDialogue = data;
        index = 0;
        isTalking = true;

        StopAllCoroutines();
        StartCoroutine(TypeSentence(data.dialogueLines[0]));
    }

    IEnumerator TypeSentence(string sentence)
    {
        Debug.Log($"TypeSentence called: '{sentence}'");
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }

    public void NextLine()
    {
        if (!isTalking)
            return;

        index++;

        if (index < currentDialogue.dialogueLines.Length)
        {
            StopAllCoroutines();
            StartCoroutine(TypeSentence(currentDialogue.dialogueLines[index]));
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        dialogueText.text = "";
        currentDialogue = null;
        isTalking = false;
    }
}
