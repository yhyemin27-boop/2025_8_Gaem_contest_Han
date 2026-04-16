using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class event3 : MonoBehaviour
{
    public DialogueData dialogueData;       // 대사 데이터 연결
    public DialogueManager dialogueManager; // DialogueManager 연결

    private bool isInDialogue = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 왼쪽 클릭
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(worldPoint);

            if (hit != null && hit.gameObject == gameObject)
            {
                // 대화 시작
                if (!isInDialogue && dialogueManager != null && !dialogueManager.isTalking)
                {
                    dialogueManager.StartDialogue(dialogueData);
                    isInDialogue = true;
                }
            }
        }

        // 대화 진행
        if (isInDialogue && Input.GetMouseButtonDown(0))
        {
            if (dialogueManager != null && dialogueManager.isTalking)
            {
                dialogueManager.NextLine();
            }
            else
            {
                EndDialogue();
            }
        }
    }

    void EndDialogue()
    {
        isInDialogue = false;
    }
}
