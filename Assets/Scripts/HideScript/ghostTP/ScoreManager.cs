using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TMP_Text resultText;     // 라운드 결과 표시 (일시적으로 표시됨)
    public TMP_Text roundScoreText; // 누적 점수 표시 (항상 유지)

    public int playerWins = 0;
    public int ghostWins = 0;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void PlayerWin()
    {
        playerWins++;
        ShowResult("플레이어 탈출 성공!");
    }

    public void GhostWin()
    {
        ghostWins++;
        ShowResult("플레이어가 잡혔다!");
    }

    private void ShowResult(string message)
    {
        // 결과 텍스트 잠깐 표시
        if (resultText != null)
        {
            resultText.text = message;
            StartCoroutine(ClearResultAfterDelay(2f)); // 2초 후 초기화
        }

        // 누적 점수는 항상 갱신
        if (roundScoreText != null)
            roundScoreText.text = $"Player {playerWins} : Ghost {ghostWins}";
    }

    private IEnumerator ClearResultAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (resultText != null)
            resultText.text = ""; // 텍스트 지움
    }
}
