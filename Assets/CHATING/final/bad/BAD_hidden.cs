using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class BAD_hidden : MonoBehaviour
{
    public Image hiddendimage;
    public Image badEedimage;
    public Button rebutton;
    public DialogueData BADdialogue;
    public DialogueData hiddendialogue;
    public dialogueEDManager dialogueEDManager;
    public string targetSceneName;
    private bool isInDialogue = false;

    void Start()
    {
        hiddendimage.gameObject.SetActive(false);
        badEedimage.gameObject.SetActive(false);
        rebutton.gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        // 2. 아이템O / 배드엔딩
        if (GameManager.Instance != null && GameManager.Instance.affection < -4 && Inventory.instance.HasItem("gym_key"))
        {
            if (badEedimage != null) badEedimage.gameObject.SetActive(true);
            if (dialogueEDManager != null && BADdialogue != null)
            {
                dialogueEDManager.StartDialogue(BADdialogue);
                isInDialogue = true;
            }
        }

        else if (!Inventory.instance.HasItem("gym_key")) //boss전에서 바로 현관-히든엔딩
        {
            hiddendimage.gameObject.SetActive(true);
            if (dialogueEDManager != null && hiddendialogue != null)
            {
                dialogueEDManager.StartDialogue(hiddendialogue);
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
