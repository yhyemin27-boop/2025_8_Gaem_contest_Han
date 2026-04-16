using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class floor : MonoBehaviour
{
    public Transform floor1; // 1층 위치
    public Transform floor2; // 2층 위치
    public Transform floor3; // 3층 위치

    private static floor currentDoor = null;
    private bool playerNearby = false;
    private GameObject player;

    public static event System.Action<Vector2> OnPlayerTeleported;//ghost그거

    // 플레이어 현재 층을 모든 문이 공유
    private static int currentFloorIndex = 1; // 1=1층, 2=2층, 3=3층

    void Update()
    {
        if (playerNearby && currentDoor == this)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                MoveFloor(1);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                MoveFloor(-1);
            }
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
                currentDoor = null;
            Debug.Log(gameObject.name + " 문 근처에서 플레이어 이탈");
        }
    }

    private void MoveFloor(int direction)
    {
        int targetFloorIndex = Mathf.Clamp(currentFloorIndex + direction, 1, 3);

        if (targetFloorIndex == currentFloorIndex)
        {
            Debug.Log("더 이상 이동할 수 없는 층입니다.");
            return;
        }

        currentFloorIndex = targetFloorIndex;

      switch (currentFloorIndex)
{
    case 1:
        player.transform.position = floor1.position;
        OnPlayerTeleported?.Invoke(floor1.position);
        break;
    case 2:
        player.transform.position = floor2.position;
        OnPlayerTeleported?.Invoke(floor2.position);
        break;
    case 3:
        player.transform.position = floor3.position;
        OnPlayerTeleported?.Invoke(floor3.position);
        break;
}
Debug.Log($"플레이어가 {currentFloorIndex}층으로 이동");

        Debug.Log($"플레이어가 {currentFloorIndex}층으로 이동");


    }
}
