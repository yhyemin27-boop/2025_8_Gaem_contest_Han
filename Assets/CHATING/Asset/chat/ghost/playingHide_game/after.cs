using UnityEngine;

public class after : MonoBehaviour
{
    [Header("이기고지고채팅")]
    public DialogueData dialogueData1; // 플레이어 승리
    public DialogueData dialogueData2; // 귀신 승리
    public GameObject dialogueBox;
    public DialogueManager dialogueManager;
    public ScoreManager scoreManager;
public HideGameManager hideGameManager;
    private bool isInDialogue = false;

   private void OnMouseDown()
{
    // 3라운드가 끝났을 때만 대화 가능
    if (!dialogueManager.isTalking && scoreManager != null && 
        scoreManager.playerWins + scoreManager.ghostWins >= hideGameManager.totalRounds)
    {
        DialogueData selectedDialogue = 
            (scoreManager.playerWins >= scoreManager.ghostWins) ? dialogueData1 : dialogueData2;

        dialogueManager.StartDialogue(selectedDialogue);
        dialogueBox.SetActive(true);
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
        dialogueBox.SetActive(false);
        dialogueManager.isTalking = false;
        isInDialogue = false;
    }
}
