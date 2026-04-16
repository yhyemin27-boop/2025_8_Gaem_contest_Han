using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class insideDream : MonoBehaviour
{
    public string nextSceneName = "OldSchool"; // 이동할 씬 이름
    public Transform player; // 플레이어를 직접 참조할 수 있게 공개
    public float interactRange = 2f; // 문과의 거리 제한

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryEnterDream();
        }
    }

    void TryEnterDream()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        // 1. 플레이어와의 거리 측정
        if (Vector2.Distance(transform.position, player.position) > interactRange)
        {
            return; // 거리가 멀면 실행 안함
        }

        // 2. 인벤토리 조건 확인
        if (Inventory.instance != null)
        {
            if (currentSceneName == "first")
            {
                SceneManager.LoadScene(nextSceneName);
            }
            else if (currentSceneName == "OldSchool" || (currentSceneName == "OldSchool_Sce"))
            {
                SceneManager.LoadScene(nextSceneName);
            }
            else if (currentSceneName == "StairsGame")
            {
                // HasItem 메서드로 보유 여부 확인
                if (Inventory.instance.HasItem("1key"))
                {
                    SceneManager.LoadScene(nextSceneName);
                }
                else
                {
                    Debug.Log("1key 없음. 씬 이동 불가.");
                }
            }

            else if (currentSceneName == "Hide_game")
            {
                // HasItem 메서드로 보유 여부 확인
                if (Inventory.instance.HasItem("2key"))
                {
                    SceneManager.LoadScene(nextSceneName);
                }
                else
                {
                    Debug.Log("2key 없음. 씬 이동 불가.");
                }
            }

            else if (currentSceneName == "ghostpast")
            {
                // HasItem 메서드로 보유 여부 확인
                if (Inventory.instance.HasItem("1key"))
                {
                    SceneManager.LoadScene(nextSceneName);
                }
                else
                {
                    Debug.Log("1key 없음. 씬 이동 불가.");
                }
            }

            else if (currentSceneName == "real")
            {
                // HasItem 메서드로 보유 여부 확인
                if (Inventory.instance.HasItem("graduation_album"))
                {
                    SceneManager.LoadScene(nextSceneName);
                }
                else
                {
                    Debug.Log("1key 없음. 씬 이동 불가.");
                }
            }

            else if (currentSceneName == "final")
            {
                GameManager.Instance.affection = 0;
                Inventory.instance.firstClear();
                SceneManager.LoadScene(nextSceneName);
            }
        }
        else
        {
            Debug.Log("인벤토리 없음. 씬 이동 불가.");
        }
    }
}
