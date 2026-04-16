using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ghostfirst : MonoBehaviour
{
    public DialogueData dialogueData;
    public DialogueManager dialogueManager;
    private bool isInDialogue = false;

    public TextMeshProUGUI Yesgive;
    public TextMeshProUGUI catchyou;
    public Image badEedimage;
    public Button returnbutton;

    public void Start()
    {
        Yesgive.gameObject.SetActive(false);
        catchyou.gameObject.SetActive(false);
        badEedimage.gameObject.SetActive(false);
        returnbutton.gameObject.SetActive(false);
    }
    private void OnMouseDown()
    {
        if (!dialogueManager.isTalking)
        {
            dialogueManager.StartDialogue(dialogueData);
            isInDialogue = true;
        }
    }

    void Update()
    {
        if (isInDialogue && Input.GetKeyDown(KeyCode.Space))
        {
            if (dialogueManager.isTalking)
            {
                dialogueManager.NextLine();
            }
            else
            {
                EndDialogue();
            }
        }
    }

    public void EndDialogue()
    {
        dialogueManager.isTalking = false;
        isInDialogue = false;
    }


    public void badEed()
{
    Debug.Log("badEed 호출됨");
    Yesgive.gameObject.SetActive(true);
    catchyou.gameObject.SetActive(false);
    badEedimage.gameObject.SetActive(true);
    returnbutton.gameObject.SetActive(true);
}

public void badEed2()
{
    Debug.Log("badEed2 호출됨");
    catchyou.gameObject.SetActive(true);
    Yesgive.gameObject.SetActive(false);
    badEedimage.gameObject.SetActive(true);
    returnbutton.gameObject.SetActive(true);
}

}