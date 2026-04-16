using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public GameObject choicePanel;
    public GameObject chatbackground;
    public GameObject chatbackground2_bad;
    private DialogueData currentDialogue;
    public Button[] choiceButtons;
    private int index = 0;
    public bool isTalking = false;
    //
    public StairsGamerull stairsGameManager; // 인스펙터에서 연결(Stairs게임)
    public HideGameManager HideGameManager; // 인스펙터에서 연결(Hide게임)
    public ghostfirst ghostEd;//보스전 시작하기전 배드엔딩

    public boss bossStage; //보스전
    //

    private HashSet<DialogueData> finishedDialogues = new HashSet<DialogueData>();

    void Update()
    {
        if (isTalking && Input.GetKeyDown(KeyCode.Space))
        {
            NextLine();
        }
    }
    private void Awake()
    {
        if (chatbackground != null)
            chatbackground.SetActive(false);

        if (chatbackground2_bad != null)
            chatbackground2_bad.SetActive(false);

        if (choicePanel != null)
            choicePanel.SetActive(false);

    }

    public void StartDialogue(DialogueData data, bool forceStart = false)
    {
        if (GameManager.Instance != null && GameManager.Instance.affection < -6)
        {
            chatbackground.SetActive(false);
            chatbackground2_bad.SetActive(true);
        }
        else
        {
            chatbackground.SetActive(true);
            chatbackground2_bad.SetActive(false);
        }

        if (finishedDialogues.Contains(data) && !forceStart)
        {
            Debug.Log("[DEBUG] 이 대화는 이미 끝났습니다.");
            return;
        }

        finishedDialogues.Add(data); // << 시작하자마자 기록 (중복 방지)
        currentDialogue = data;
        index = 0;
        isTalking = true;

        StopAllCoroutines();
        StartCoroutine(TypeSentence(data.dialogueLines[0]));
    }


    IEnumerator TypeSentence(string sentence)
    {
        if (dialogueText == null)
        {
            Debug.LogError("dialogueText가 null입니다! Inspector에서 연결했는지 확인하세요.");
            yield break;
        }
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }

        if (currentDialogue.hasChoice && index >= currentDialogue.dialogueLines.Length - 1)
        {
            ShowChoices();
        }
    }

    public void NextLine()
    {
        if (!isTalking)
            return;

        index++;

        if (index < currentDialogue.dialogueLines.Length)
        {
            StopAllCoroutines();
            StartCoroutine(TypeSentence(currentDialogue.dialogueLines[index]));
        }
        else
        {
            if (currentDialogue.hasChoice)
                ShowChoices();
            else
                EndDialogue();
        }
    }

    void ShowChoices()
    {

        if (choicePanel == null)
        {
            Debug.LogError("choicePanel이 null입니다! Inspector에서 연결 필요");
            return;
        }
        Debug.Log(" ShowChoices() 호출됨");
        choicePanel.SetActive(true);

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (i < currentDialogue.choices.Length)
            {
                choiceButtons[i].gameObject.SetActive(true);
                choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentDialogue.choices[i].text;

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
    void OnChoiceSelected(int selectedIndex)
    {
        var choice = currentDialogue.choices[selectedIndex];

        // 보상 아이템
        if (!string.IsNullOrEmpty(choice.rewardItem))
            Inventory.instance.AddItem(choice.rewardItem, choice.rewardAmount); // 보상 수량 적용

        // 호감도 변화
        if (GameManager.Instance != null)
            GameManager.Instance.AddAffection(choice.affectionChange);

        // 게임 시작 (StairsGame 전용)
        if (choice.text == "(game_start)" && SceneManager.GetActiveScene().name == "StairsGame")
        {
            if (stairsGameManager != null)
            {
                stairsGameManager.StartGame();
            }
        }

        //Hide 게임
        if (choice.text == "game_start" && SceneManager.GetActiveScene().name == "Hide_game")
        {
            if (HideGameManager != null)
            {
                HideGameManager.StartHideGame();
            }
        }


        if (SceneManager.GetActiveScene().name == "final" && choice.text == "준다")
        {
            if (ghostEd != null)
            {
                ghostEd.badEed();
            }
        }

        if (SceneManager.GetActiveScene().name == "final" && choice.text == "주지 않는다.")
        {
            if (bossStage != null)
            {
                bossStage.StartBoss();//보스전
            }
        }


        // 다음 대사로 이어지기
        if (choice.nextDialogue != null)
        {
            choicePanel.SetActive(false); // 선택지 숨기기
            StartDialogue(choice.nextDialogue);
        }
        else
        {
            EndDialogue(); // 대화 종료
        }
    }
    void EndDialogue()
    {
        dialogueText.text = "";
        if (currentDialogue != null)
            finishedDialogues.Add(currentDialogue); // 끝난 대화 기록

        currentDialogue = null;
        isTalking = false;
        if (chatbackground != null)
            chatbackground.SetActive(false);
        if (chatbackground2_bad != null)
            chatbackground2_bad.SetActive(false);
        if (choicePanel != null)
            choicePanel.SetActive(false);
    }

}
