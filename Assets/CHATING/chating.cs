using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chating : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void OnMouseDown() 
    {
        GKFAJSLDialogue npcDialogue = GetComponent<GKFAJSLDialogue>();

        if (npcDialogue != null)
        {
            dialogueManager.StartDialogue(npcDialogue.dialogueData);
        }
    }

}