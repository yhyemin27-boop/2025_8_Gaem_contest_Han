using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChoiceResult
{
    public string text; // 선택지 텍스트
    public DialogueData nextDialogue; // 선택 후 이어질 대사
    public string rewardItem; // 보상 아이템 이름
    public int rewardAmount; // 보상 수량
    public int affectionChange; // 호감도 변화량 등 추가 가중치
    public bool autoContinueDialogue;    // 채팅 반복 X
}

[CreateAssetMenu(fileName = "New DialogueData", menuName = "Dialogue/DialogueData")]
public class DialogueData : ScriptableObject
{
    public string GKFAJSL_chat;

    [TextArea(3, 10)]
    public string[] dialogueLines;

    public bool hasChoice;
    public ChoiceResult[] choices; // 선택지 배열
}


