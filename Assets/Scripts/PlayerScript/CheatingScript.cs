using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
public class CheatingScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (Inventory.instance != null)
            {
                Inventory.instance.AddItem("ring", 1);
                Debug.Log("[치트] 링 아이템 /열쇠 지급됨!");
            }
            else
            {
                Debug.LogWarning("Inventory가 존재하지 않습니다.");
            }
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            if (Inventory.instance != null)
            {
                Debug.Log("Inventory instance found");
                Inventory.instance.AddItem("gym_key", 1);
                Inventory.instance.AddItem("1key", 1);
                Inventory.instance.AddItem("graduation_album", 1);
                Inventory.instance.AddItem("Alcohol_lamp", 1);
                Debug.Log("[치트] 링 아이템 /열쇠 지급됨!");
            }
            else
            {
                Debug.LogWarning("Inventory가 존재하지 않습니다.");
            }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            Inventory.instance.AddItem("Alcohol_lamp", 1);
            Inventory.instance.AddItem("Alcohol_lamp", 1);
            Inventory.instance.AddItem("record", 1);
            Inventory.instance.AddItem("note", 1);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            Inventory.instance.AddItem("video", 1);
        }

        // 방향키 위: 호감도 증가
            if (Input.GetKeyDown(KeyCode.O))
            {
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.AddAffection(1);
                    Debug.Log("[치트] 호감도 +1");
                }
            }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddAffection(-1);
                Debug.Log("[치트] 호감도 -1");
            }
        }
    }
}
