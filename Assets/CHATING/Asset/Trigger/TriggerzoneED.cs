using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerzoneED : MonoBehaviour
{
    public TriggerED triggerED; // TriggerED 스크립트를 참조할 변수
    public int dialogueIndex;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && triggerED != null)
        {
            triggerED.TriggerDialogue(dialogueIndex);
        }
    }
}
