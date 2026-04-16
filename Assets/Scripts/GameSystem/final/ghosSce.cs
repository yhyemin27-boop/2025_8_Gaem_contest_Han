using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ghostSce : MonoBehaviour
{
    public DialogueData dialogueData;
    public DialogueManager dialogueManager;
    private bool isInDialogue = false;




    private void OnMouseDown()
    {
        if (!dialogueManager.isTalking)
        {
            dialogueManager.StartDialogue(dialogueData);
            isInDialogue = true;
        }
    }

    void Update()
    {
        if (isInDialogue && Input.GetKeyDown(KeyCode.Space))
        {
            if (dialogueManager.isTalking)
            {
                dialogueManager.NextLine();
            }
            else
            {
                EndDialogue();
            }
        }
    }

    public void EndDialogue()
    {
        dialogueManager.isTalking = false;
        isInDialogue = false;
    }

}