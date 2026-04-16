using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HideSystem : MonoBehaviour
{
    public TMP_Text HideOK;   // UI 텍스트 (예: "E키로 들어가기 / 나오기")

    public bool isHidden = false;
    public playerHideScript[] hideSpots;   // 숨기 가능한 모든 장소

    private Renderer playerRenderer;
    private playerHideScript currentSpot;  // 현재 숨은 장소

    private void Start()
    {
        playerRenderer = GetComponent<Renderer>();
        HideOK.text = ""; // 시작 시 텍스트 초기화
    }

    public void Update()
    {
        // 숨지 않은 상태에서 가장 가까운 장소 탐색
        if (!isHidden)
        {
            playerHideScript nearestSpot = GetNearestHideSpot();
            if (nearestSpot != null && nearestSpot.CanHide(transform))
            {
                HideOK.text = "E키로 들어가기";

                if (Input.GetKeyDown(KeyCode.E))
                {
                    currentSpot = nearestSpot;
                    Hide();
                }
            }
            else
            {
                HideOK.text = "";
            }
        }
        // 숨은 상태
        else
        {
            HideOK.text = "E키로 나오기";

            if (Input.GetKeyDown(KeyCode.E))
            {
                Unhide();
            }
        }
    }

    private playerHideScript GetNearestHideSpot()
    {
        float minDist = float.MaxValue;
        playerHideScript nearest = null;

        foreach (var spot in hideSpots)
        {
            if (spot == null) continue;
            float dist = Vector3.Distance(transform.position, spot.hidingSpot.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = spot;
            }
        }
        return nearest;
    }

    public void Hide()
    {
        isHidden = true;
        playerRenderer.enabled = false;
        Debug.Log("플레이어 숨음!");
    }

    public void Unhide()
    {
        isHidden = false;
        playerRenderer.enabled = true;
        Debug.Log("플레이어 나옴!");
        currentSpot = null;
    }

    public bool IsHidden() => isHidden;
}
