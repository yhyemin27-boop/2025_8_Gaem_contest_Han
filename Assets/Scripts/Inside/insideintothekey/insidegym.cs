using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // 추가
using UnityEngine.SceneManagement;
public class insidegym : MonoBehaviour
{
    public Transform science_insideoutside;
    public string nextSceneName = "OldSchool"; // 이동할 씬 이름
    private GameObject player;
    private bool playerNearby = false;
    public TextMeshProUGUI Notinside;

    private static insidegym currentDoor = null; // 타입 맞춤

   void Update()
{
    if (playerNearby && currentDoor == this && Input.GetMouseButtonDown(0))
    {
        if (gameObject.CompareTag("GymDoor"))
        {
            
            StartCoroutine(gym_EnterInside());
        }
        else if (gameObject.CompareTag("ScienceDoor"))
        {
            StartCoroutine(science_EnterInside());
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

            if (currentDoor == this) currentDoor = null;

            Debug.Log(gameObject.name + " 문 근처에서 플레이어 이탈");
        }
    }

    private IEnumerator gym_EnterInside()
    {
        if (player != null)
        {
            if (Inventory.instance != null && Inventory.instance.HasItem("past_gym_key"))
            {
                if (Inventory.instance.HasItem("Alcohol_lamp"))
                {
                    Inventory.instance.RemoveItem("Alcohol_lamp");
                }
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                Notinside.text = "아직 들어올 조건을 만족하지 못했습니다";
                yield return new WaitForSeconds(2f);
                Notinside.text = "";
            }

            playerNearby = false;
            currentDoor = null;
        }
    }

    private IEnumerator science_EnterInside()
    {
        if (player != null && science_insideoutside != null  && Inventory.instance.HasItem("past_gym_key"))
        {
            player.transform.position = science_insideoutside.position;
        }
        else
        {
            Notinside.text = "아직 들어올 조건을 만족하지 못했습니다";
            yield return new WaitForSeconds(2f);
            Notinside.text = "";
        }
    }
}
