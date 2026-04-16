using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class final_book : MonoBehaviour
{
    
    public DialogueManager dialogueManager;  
    public GameObject dialogueBox;            
    public DialogueData dialogueData;
    private bool isInDialogue = false; 
    void OnMouseDown()
    {
        if (!dialogueManager.isTalking)
        {
            dialogueBox.SetActive(true);
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
        dialogueBox.SetActive(false);
        dialogueManager.isTalking = false;
        isInDialogue = false;
    }
}
