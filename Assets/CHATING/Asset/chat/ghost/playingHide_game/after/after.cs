using UnityEngine;

public class After : MonoBehaviour
{
    [Header("이기고지고채팅")]
    public DialogueData dialogueData1; // 플레이어 승리
    public DialogueData dialogueData2; // 귀신 승리
    public DialogueManager dialogueManager;
    public ScoreManager scoreManager;

    private bool isInDialogue = false;

    private void OnMouseDown()
    {
        if (!dialogueManager.isTalking)
        {
            // 점수 비교 후 알맞은 DialogueData 선택
            DialogueData selectedDialogue;

            if (scoreManager == null)
            {
                Debug.LogWarning("ScoreManager 미설정!");
                return;
            }

            if (scoreManager.playerWins >= scoreManager.ghostWins)
                selectedDialogue = dialogueData1; // 플레이어 승리 또는 동점
            else
                selectedDialogue = dialogueData2; // 귀신 승리

            dialogueManager.StartDialogue(selectedDialogue);
            isInDialogue = true;
        }
    }

    private void Update()
    {
        // 왼쪽 마우스 클릭 또는 스페이스로 다음 대사 진행
        if (isInDialogue && Input.GetKeyDown(KeyCode.Space))
        {
            if (dialogueManager.isTalking)
                dialogueManager.NextLine();
            else
                EndDialogue();
        }
    }

    public void EndDialogue()
    {
        dialogueManager.isTalking = false;
        isInDialogue = false;
    }
}
