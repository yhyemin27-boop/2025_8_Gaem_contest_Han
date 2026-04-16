using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TriggerDialogueManager : MonoBehaviour
{
    public DialogueData[] triggerDialogues; // 트리거 대사 목록
    private HashSet<int> triggeredIndices = new HashSet<int>();

    [Header("UI")]
    public GameObject dialoguePanel; // 대사창
    public TMP_Text dialogueText;    // 대사 텍스트
    public GameObject choicePanel;   // 선택지 패널
    public Button[] choiceButtons;   // 선택지 버튼

    private DialogueData currentDialogue;
    private int currentLineIndex;
    private bool isDialogueActive = false;

    public void TriggerDialogue(int index)
    {
        if (!triggeredIndices.Contains(index) && index >= 0 && index < triggerDialogues.Length)
        {
            triggeredIndices.Add(index);
            StartDialogue(triggerDialogues[index]);
        }
    }

    private void StartDialogue(DialogueData dialogue)
    {
        currentDialogue = dialogue;
        currentLineIndex = 0;
        isDialogueActive = true;
        dialoguePanel.SetActive(true);
        choicePanel.SetActive(false);
        ShowLine();
    }

    private void ShowLine()
    {
        if (currentDialogue != null && currentLineIndex < currentDialogue.dialogueLines.Length)
        {
            dialogueText.text = currentDialogue.dialogueLines[currentLineIndex];

            // 마지막 대사면 선택지 표시
            if (currentDialogue.hasChoice && currentLineIndex >= currentDialogue.dialogueLines.Length - 1)
            {
                ShowChoices();
            }
        }
        else
        {
            EndDialogue();
        }
    }

    private void ShowChoices()
    {
        if (choicePanel == null || choiceButtons == null)
            return;

        choicePanel.SetActive(true);

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (i < currentDialogue.choices.Length)
            {
                choiceButtons[i].gameObject.SetActive(true);
                choiceButtons[i].GetComponentInChildren<TMP_Text>().text = currentDialogue.choices[i].text;

                int choiceIndex = i;
                choiceButtons[i].onClick.RemoveAllListeners();
                choiceButtons[i].onClick.AddListener(() => OnChoiceSelected(choiceIndex));
            }
            else
            {
                choiceButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnChoiceSelected(int selectedIndex)
    {
        var choice = currentDialogue.choices[selectedIndex];

        // 보상 아이템
        if (!string.IsNullOrEmpty(choice.rewardItem))
            Inventory.instance?.AddItem(choice.rewardItem, choice.rewardAmount);

        //  호감도 변화
        if (GameManager.Instance != null)
            GameManager.Instance.AddAffection(choice.affectionChange);

        // 다음 대화로 이어지기
        if (choice.nextDialogue != null)
        {
            choicePanel.SetActive(false);
            StartDialogue(choice.nextDialogue);
        }
        else
        {
            EndDialogue();
        }
    }

    private void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            // 선택지 패널이 켜져 있으면 클릭 무시
            if (choicePanel != null && choicePanel.activeSelf)
                return;

            currentLineIndex++;
            ShowLine();
        }
    }

    private void EndDialogue()
    {
        isDialogueActive = false;
        dialoguePanel.SetActive(false);
        choicePanel.SetActive(false);
        currentDialogue = null;
    }
}
