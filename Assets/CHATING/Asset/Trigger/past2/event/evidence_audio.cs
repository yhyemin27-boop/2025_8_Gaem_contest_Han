using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class evidence_audio : MonoBehaviour
{
    public DialogueData dialogueData;
    public GameObject dialogueBox;
    public DialogueManager dialogueManager;
    private bool isInDialogue = false;

    private void OnMouseDown()
    {
        Debug.Log("오브젝트 클릭됨"); // 추가
        if (!dialogueManager.isTalking)
        {
            dialogueBox.SetActive(true);
            dialogueManager.StartDialogue(dialogueData);
            isInDialogue = true;
        }
    }

    void Update()
    {
        // 왼쪽 마우스 클릭으로 다음 대사 진행
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