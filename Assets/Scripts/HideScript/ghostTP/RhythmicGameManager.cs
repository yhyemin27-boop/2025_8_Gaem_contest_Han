using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmicGameManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject notePrefab;
    public Transform spawnPoint;
    public Transform targetPoint;

    [Header("Settings")]
    public float noteSpeed = 300f;
    public float spawnInterval = 2f;
    public float hitRange = 50f;

    [Header("Input")]
    public KeyCode hitKey = KeyCode.Space;

    private List<GameObject> activeNotes = new List<GameObject>();
    private bool isGameActive = false;

    [Header("Miss 시스템")]
    public int missCount = 0;
    public int maxMissAllowed = 3;
    public HideSystem hideSystem;
    public HideGameManager hideGameManager;

    // 리듬게임 시작
    public void StartRhythmGame()
    {
        Debug.Log("리듬게임 시작!");
        missCount = 0; // ✅ 시작 시 초기화
        isGameActive = true;
        StartCoroutine(SpawnNotes());
    }

    // 리듬게임 종료
    public void StopRhythmGame()
    {
        if (!isGameActive) return;

        Debug.Log("리듬게임 종료!");
        isGameActive = false;
        StopAllCoroutines();

        foreach (var note in activeNotes)
        {
            if (note != null) Destroy(note);
        }
        activeNotes.Clear();
    }

    // 노트 스폰
    private IEnumerator SpawnNotes()
    {
        while (isGameActive)
        {
            GameObject note = Instantiate(notePrefab, spawnPoint.position, Quaternion.identity, spawnPoint.parent);
            activeNotes.Add(note);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void Update()
    {
        if (!isGameActive) return;

        // 노트 이동
        for (int i = activeNotes.Count - 1; i >= 0; i--)
        {
            GameObject note = activeNotes[i];
            if (note == null) { activeNotes.RemoveAt(i); continue; }

            note.transform.position = Vector3.MoveTowards(
                note.transform.position,
                targetPoint.position,
                noteSpeed * Time.deltaTime
            );

            // 스페이스 입력 판정
            if (Input.GetKeyDown(hitKey))
            {
                float dist = Vector3.Distance(note.transform.position, targetPoint.position);
                if (dist <= hitRange)
                {
                    Debug.Log("Perfect!");
                    Destroy(note);
                    activeNotes.RemoveAt(i);
                }
                else
                {
                    missCount++;
                    Debug.Log("Miss!");
                    Destroy(note);
                    OnNoteMiss();
                }
            }

            // 중앙 지나가면 Miss 처리
            if (Vector3.Distance(note.transform.position, targetPoint.position) < 10f)
            {
                missCount++;
                Debug.Log("Miss! (놓침)");
                Destroy(note);
                activeNotes.RemoveAt(i);
                OnNoteMiss();
            }
        }
    }

    public void OnNoteMiss()
    {
        Debug.Log("MISS! 현재 MISS 수: " + missCount);

        if (missCount >= maxMissAllowed)
        {
            ForceCatch();
        }
    }

    private void ForceCatch()
    {
        Debug.Log("MISS 누적으로 발각 → 유령 점수 증가");

        if (hideSystem != null && hideSystem.isHidden)
        {
            hideSystem.Unhide();   // 강제로 나오게
        }

        // ✅ 현재 라운드 강제 종료 처리
        if (hideGameManager != null)
        {
            Debug.Log("RhythmicGameManager → OnPlayerCaught() 호출");
            hideGameManager.OnPlayerCaught();
        }
        else
        {
            Debug.LogWarning("HideGameManager 참조 없음!");
        }
    }
}
