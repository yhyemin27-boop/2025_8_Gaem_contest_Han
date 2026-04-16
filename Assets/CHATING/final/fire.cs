using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : MonoBehaviour//마지막 엔딩 로그
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
        dialogueManager.isTalking = false;
        isInDialogue = false;
    }
    
    
}