using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stuevent1 : MonoBehaviour{
    public TriggerDialogueManager triggerDialogueManager;
    public int dialogueIndex;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            triggerDialogueManager.TriggerDialogue(dialogueIndex);
        }
    }
}
