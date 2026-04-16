using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GymEventAndDoor : MonoBehaviour
{
    [Header("Dialogue")]
    public DialogueData dialogueData;
    public DialogueManager dialogueManager;
    private bool isInDialogue = false;

    [Header("Door")]
    public Transform inside_outsidePosition; // 이동 위치
    private GameObject player;
    private bool playerNearby = false;

    private static GymEventAndDoor currentDoor = null;
    public static event System.Action<Vector2> OnPlayerTeleported;

    void Update()
    {
        // 대화 진행
        if (isInDialogue && Input.GetKeyDown(KeyCode.Space))
        {
            if (dialogueManager.isTalking)
            {
                dialogueManager.NextLine();
            }
            else
            {
                EndDialogue();
            }
        }

        // 문 이동
        if (playerNearby && currentDoor == this && Input.GetMouseButtonDown(0))
        {
            if (Inventory.instance.HasItem("gym_key"))
            {
                EnterInside();
            }
            else
            {
                Debug.Log("열쇠가 없어 들어갈 수 없습니다!");
            }
        }
    }

    private void OnMouseDown()
    {
        if (!dialogueManager.isTalking)
        {
            dialogueManager.StartDialogue(dialogueData);
            isInDialogue = true;
        }
    }

    public void EndDialogue()
    {
        dialogueManager.isTalking = false;
        isInDialogue = false;
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

            playerNearby = false;
            currentDoor = null;
        }
    }
}
