using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InsiderSystemScript : MonoBehaviour
{
    public Transform inside_outsidePosition; // 이동 위치
    private GameObject player;               // 플레이어 오브젝트
    private bool playerNearby = false;       // 플레이어가 문 근처에 있는지

    private static InsiderSystemScript currentDoor = null; 
    public static event System.Action<Vector2> OnPlayerTeleported;

    void Update()
    {
        // 플레이어가 문 근처이고, 현재 선택된 문이면 클릭 시 이동
        if (playerNearby && currentDoor == this && Input.GetMouseButtonDown(0))
        {
            EnterInside();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            player = other.gameObject;
            currentDoor = this;

            Debug.Log(gameObject.name + " 문 근처에 플레이어 진입");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            player = null;

            if (currentDoor == this)
            {
                currentDoor = null;
            }

            Debug.Log(gameObject.name + " 문 근처에서 플레이어 이탈");
        }
    }

    private void EnterInside()
    {
        if (player != null && inside_outsidePosition != null)
        {
            player.transform.position = inside_outsidePosition.position;
            Debug.Log("EnterInside 실행: " + gameObject.name);

            OnPlayerTeleported?.Invoke(inside_outsidePosition.position);

            // 이동 후 다시 상호작용 못 하도록 초기화
            playerNearby = false;
            currentDoor = null;
        }
    }
}
