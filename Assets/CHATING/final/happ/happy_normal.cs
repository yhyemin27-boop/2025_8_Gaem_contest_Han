using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class happy_normal : MonoBehaviour//엔딩 루트 구분
{
    public Image Happyimage;
    public Image normalimage;
    public Button rebutton;
    public DialogueData Happydialogue;
    public DialogueData normaldialogue;
    public dialogueEDManager dialogueEDManager;
    public string targetSceneName;
    private bool isInDialogue = false;

    void Start()
    {
        Happyimage.gameObject.SetActive(false);
        normalimage.gameObject.SetActive(false);
        rebutton.gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        // 2. 아이템-특정 아이템 모두 충족X / 노말엔딩
        if (GameManager.Instance != null && -5 < GameManager.Instance.affection)
        {
            if (Happyimage != null) Happyimage.gameObject.SetActive(true);
            if (dialogueEDManager != null && Happydialogue != null)
            {
                dialogueEDManager.StartDialogue(Happydialogue);
                isInDialogue = true;
            }
        }

        else if (Inventory.instance.HasItem("Alcohol_lamp") && Inventory.instance.HasItem("record") && Inventory.instance.HasItem("video") && Inventory.instance.HasItem("note") && -5 < GameManager.Instance.affection) //boss전에서 바로 현관-히든엔딩
        {
            normalimage.gameObject.SetActive(true);
            if (dialogueEDManager != null && normaldialogue != null)
            {
                dialogueEDManager.StartDialogue(normaldialogue);
                isInDialogue = true;
            }
            return;
        }
    }

    void Update()
    {
        if (isInDialogue && Input.GetKeyDown(KeyCode.Space))
        {
            if (dialogueEDManager != null && dialogueEDManager.isTalking)
            {
                dialogueEDManager.NextLine();
            }
            else
            {
                EndDialogue();
            }
        }
    }

    public void EndDialogue()
    {
        if (dialogueEDManager != null)
            dialogueEDManager.isTalking = false;

        isInDialogue = false;

        // 이미지 그대로 둠
        if (rebutton != null)
            rebutton.gameObject.SetActive(true); // 대화 끝나면 버튼만 켜기
    }

    public void RESTART()
    {
        SceneManager.LoadScene(targetSceneName);
    }
}
