using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmicTrigger : MonoBehaviour
{
    public ghostTP ghost;
    public HideSystem player;
    public float triggerRange = 4.5f;
    public RhythmicGameManager rhythmicGameManager;

    private bool isGameRunning = false;

    private void Update()
    {
        if (ghost.GetCurrentPoint() == null || player == null) return;

        float dist = Vector3.Distance(player.transform.position, ghost.GetCurrentPoint().position);

        // 플레이어가 숨은 상태이고 귀신이 가까이 있으면 리듬게임 시작
        if (!isGameRunning && dist <= triggerRange && player.IsHidden())
        {
            rhythmicGameManager.StartRhythmGame();
            isGameRunning = true;
        }
        // 리듬게임 진행 중인데, 거리가 멀어졌거나 플레이어가 숨지 않으면 종료
        else if (isGameRunning && (dist > triggerRange || !player.IsHidden()))
        {
            rhythmicGameManager.StopRhythmGame();
            isGameRunning = false;
        }
    }
}
