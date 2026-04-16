using System.Collections;
using UnityEngine;

public class ghostTP : MonoBehaviour
{
    public Transform[] ghostPoints; // TP할 위치들
    private Transform targetPoint;


    public IEnumerator TeleportRoutine()
    {
        yield return new WaitForSeconds(10f); // 처음 10초 숨는 시간 대기

        while (true)
        {
            TeleportToRandomPoint();
            yield return new WaitForSeconds(30f); // 이후 30초마다 텔포
        }
    }
    public void TeleportToRandomPoint()
    {
        if (ghostPoints.Length == 0) return;

        int index = Random.Range(0, ghostPoints.Length);
        targetPoint = ghostPoints[index];
        transform.position = targetPoint.position;
        Debug.Log($"귀신이 {targetPoint.name} 로 순간이동!");
    }

    public Transform GetCurrentPoint() => targetPoint;
}
