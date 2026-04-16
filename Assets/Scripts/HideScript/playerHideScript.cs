using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHideScript : MonoBehaviour
{
    public Transform hidingSpot; // 숨는 위치 (X축 중심)
    public float hideRange = 2f; // 숨을 수 있는 거리 범위

    // 플레이어가 숨을 수 있는 위치에 있는지 판정
    public bool CanHide(Transform player)
    {
        if (hidingSpot == null) return false;
        return Mathf.Abs(player.position.x - hidingSpot.position.x) <= hideRange;
    }
}
