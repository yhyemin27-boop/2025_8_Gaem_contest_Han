using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // 호감도 변수 추가
    public int affection = 0;
    public void AddAffection(int amount)
    {
        affection += amount;
        Debug.Log($"호감도 {amount} 증가! 현재 호감도: {affection}");
    }

    void Update()
    {
     if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("ESC 입력 감지됨");
            SceneManager.LoadScene(0);
        }
}
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
