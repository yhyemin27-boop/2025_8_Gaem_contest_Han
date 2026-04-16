using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class firstrullevent : MonoBehaviour
{
    [Header("씬 및 UI")]
    public string firstSceneName;
    public GameObject rullImage;
    public Button startButton;
    public Button rullButton;

    public Button gameQuit;

    void Start()
    {

        rullButton.gameObject.SetActive(true);
        rullImage.SetActive(false);
        startButton.gameObject.SetActive(false);
        gameQuit.gameObject.SetActive(true);
    }

    // 룰 열기 버튼 클릭 시
    public void OnRullOpenEvent()
    {
        if (startButton != null) startButton.gameObject.SetActive(true);
        if (rullImage != null) rullImage.SetActive(true);
    }

    public void OnFirstScene()
    {
        GameManager.Instance.affection = 0;
        // Inventory 초기화
        Inventory.instance.firstClear();

        // 씬 전환
        SceneManager.LoadScene(firstSceneName);
    }

    public void gameoutgameQuit()
    {
        Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
