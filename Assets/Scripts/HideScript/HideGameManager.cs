using System.Collections;
using UnityEngine;
using TMPro;

public class HideGameManager : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text countdownText;

    [Header("References")]
    public HideSystem playerHide;
    public ghostTP ghost;
    public RhythmicGameManager rhythmicGameManager;

    [Header("Game Settings")]
    public float hidePhaseTime = 10f;        // 숨는 시간
    public float roundTime = 90f;            // 한 판 시간
    public float ghostMoveInterval = 30f;    // 귀신 이동 간격
    public float rhythmGameTime = 20f;       // 리듬게임 시간
    public float checkRadius = 3f;           // 귀신과 플레이어 거리 체크
    public int totalRounds = 3;              // 전체 라운드 수

    private bool canGhostMove = false;
    private bool isRhythmPlaying = false;
    private bool roundEnded = false;         // 현재 라운드 강제 종료 플래그

    public void StartHideGame()
    {
        StartCoroutine(RoundSequence());
    }

    // 전체 라운드 진행
    private IEnumerator RoundSequence()
    {
        for (int round = 1; round <= totalRounds; round++)
        {
            countdownText.text = $"Round {round} 시작!";
            yield return new WaitForSeconds(2f);

            // 현재 라운드 진행
            yield return StartCoroutine(GameSequence(round));

            countdownText.text = $"Round {round} 종료!";
            yield return new WaitForSeconds(2f);
        }

        rhythmicGameManager.StopRhythmGame(); // 혹시 남아있을 리듬게임 강제 종료
    }

    // 한 라운드 진행
    private IEnumerator GameSequence(int round)
    {
        roundEnded = false; // 초기화

        // 숨는 시간
        yield return StartCoroutine(CountdownCoroutine(hidePhaseTime, "숨는 시간"));

        countdownText.text = "Go!";

        // 귀신 이동 시작
        canGhostMove = true;
        StartCoroutine(GhostMoveRoutine());

        // 라운드 타이머
        float timer = roundTime;
        rhythmicGameManager.missCount = 0; // MISS 초기화

        while (timer > 0f)
        {
            if (roundEnded) break; // 현재 라운드 강제 종료 시 루프 탈출

            timer -= Time.deltaTime;
            UpdateCountdownText(timer, "라운드 진행");
            yield return null;
        }

        // 라운드 끝 처리
        rhythmicGameManager.StopRhythmGame();
        canGhostMove = false;

        if (!roundEnded)
        {
            // 끝까지 안 잡혔으면 플레이어 승리
            ScoreManager.instance.PlayerWin();
        }
    }

    private IEnumerator GhostMoveRoutine()
    {
        while (canGhostMove)
        {
            ghost.TeleportToRandomPoint();

            // 플레이어와 귀신 거리 체크
            if (Vector3.Distance(ghost.transform.position, playerHide.transform.position) <= checkRadius
                && playerHide.isHidden
                && !isRhythmPlaying)
            {
                StartCoroutine(StartRhythmGame());
            }

            yield return new WaitForSeconds(ghostMoveInterval);
        }
    }

    private IEnumerator StartRhythmGame()
    {
        isRhythmPlaying = true;
        canGhostMove = false;

        rhythmicGameManager.StartRhythmGame();
        yield return StartCoroutine(CountdownCoroutine(rhythmGameTime, "리듬게임"));

        rhythmicGameManager.StopRhythmGame();
        isRhythmPlaying = false;
        canGhostMove = true;
    }

    private IEnumerator CountdownCoroutine(float duration, string phaseName)
    {
        float timer = duration;
        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            UpdateCountdownText(timer, phaseName);
            yield return null;
        }
    }

    private void UpdateCountdownText(float timeLeft, string phaseName)
    {
        int seconds = Mathf.CeilToInt(timeLeft);
        countdownText.text = $"{phaseName} : {seconds:00}초";
    }

    // 플레이어 잡힘 또는 리듬게임 실패 시 호출
    public void OnPlayerCaught()
    {
        if (roundEnded) return; // 중복 호출 방지

        Debug.Log("플레이어 잡힘!");
        ScoreManager.instance.GhostWin();

        roundEnded = true; // GameSequence 중단
        canGhostMove = false;
        isRhythmPlaying = false;

        countdownText.text = "플레이어가 잡혔습니다!";
        rhythmicGameManager.StopRhythmGame();
    }
}
