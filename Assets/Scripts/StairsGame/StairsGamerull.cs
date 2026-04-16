using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StairsGamerull : MonoBehaviour
{
    public Button rockButton, scissorsButton, paperButton;
    public GameObject gamePanel;
    public Transform playerPiece; ////무엇을 냈는지 확인 용도(UI)
    public Transform ghostPiece;
    public List<Transform> stairPoints;
    public List<Transform> ghostPoints;
    public Transform playerEndPoint; // 게임 종료 시 플레이어 이동 위치
    public Transform ghostEndPoint;  // 게임 종료 시 고스트 이동 위치
    public DialogueManager dialogueManager;

    public TextMeshProUGUI resultText; // TMP로 결과 출력용
    public DialogueData winDialogue;  // 게임에서 플레이어가 승리했을 때 보여줄 대사
    public DialogueData loseDialogue; // 게임에서 고스트가 승리했을 때 보여줄 대사


    private int playerIndex = 0;
    private int ghostIndex = 0;
    private string playerChoice;  //무엇을 냈는지 확인 용도
    private string ghostChoice;

    private void Start()
    {
        gamePanel.gameObject.SetActive(false);
        resultText.gameObject.SetActive(false);

        rockButton.onClick.AddListener(() => OnPlayerChoice("rock"));
        scissorsButton.onClick.AddListener(() => OnPlayerChoice("scissors"));
        paperButton.onClick.AddListener(() => OnPlayerChoice("paper"));

    }

    public void StartGame()
    {
        gamePanel.gameObject.SetActive(true);
        
        resultText.gameObject.SetActive(true);
        StartCoroutine(StartGameCoroutine());
    }

    string GetChoiceInKorean(string choice)
{
    switch (choice)
    {
        case "rock": return "바위";
        case "paper": return "보";
        case "scissors": return "가위";
        default: return "";
    }
}

   IEnumerator StartGameCoroutine()
{
    while (playerIndex < stairPoints.Count && ghostIndex < ghostPoints.Count)
    {
        gamePanel.SetActive(true);
        resultText.text = "가위바위보!";
        resultText.gameObject.SetActive(true);

        yield return new WaitUntil(() => !string.IsNullOrEmpty(playerChoice));

        ghostChoice = GetRandomChoice();
        string result = DetermineWinner(playerChoice, ghostChoice);
        string previousChoice = playerChoice;
        playerChoice = "";

        string playerKo = GetChoiceInKorean(previousChoice);
        string ghostKo = GetChoiceInKorean(ghostChoice);
        resultText.text = $"플레이어: {playerKo} / 귀신: {ghostKo}";

        Debug.Log($"Player: {previousChoice}, AI: {ghostChoice}, Result: {result}");

        switch (result)
        {
            case "win":
                playerIndex++;
                if (playerIndex < stairPoints.Count)
                    MovePieces(stairPoints[playerIndex], playerPiece);
                break;
            case "lose":
                ghostIndex++;
                if (ghostIndex < ghostPoints.Count)
                    MovePieces(ghostPoints[ghostIndex], ghostPiece);
                break;
            case "draw":
                break;
        }

        yield return new WaitForSeconds(2f);
    }

        gamePanel.SetActive(false);
        resultText.text = "";

        // 게임 종료 시 이동 처리
        if (playerIndex >= stairPoints.Count)
        {
            Debug.Log("플레이어 승");
            if (playerEndPoint != null && ghostEndPoint != null)
            {
                playerPiece.position = playerEndPoint.position;
                ghostPiece.position = ghostEndPoint.position;
            }
            // 대사 강제 실행
                dialogueManager.StartDialogue(winDialogue, true);
        }
        else if (ghostIndex >= ghostPoints.Count)
        {
            Debug.Log("학생 귀신 win");
            if (ghostEndPoint != null && playerEndPoint != null)
             {
                playerPiece.position = playerEndPoint.position;
                ghostPiece.position = ghostEndPoint.position;
            }
            dialogueManager.StartDialogue(loseDialogue, true);
        }

    }

    void MovePieces(Transform point, Transform piece)
    {
        piece.position = point.position;
    }

    void OnPlayerChoice(string choice)
    {
        playerChoice = choice;
    }

    string GetRandomChoice()
    {
        string[] choices = { "rock", "paper", "scissors" };
        return choices[Random.Range(0, choices.Length)];
    }

    string DetermineWinner(string player, string ai)
    {
        if (player == ai) return "draw";
        if ((player == "rock" && ai == "scissors") ||
            (player == "scissors" && ai == "paper") ||
            (player == "paper" && ai == "rock"))
            return "win";
        return "lose";
    }
}
