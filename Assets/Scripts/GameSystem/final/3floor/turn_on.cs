using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class turn_on : MonoBehaviour
{
    public GameObject turn_on_jym;
    public GameObject turn_off_jym;
    public float detectionRadius = 4f;
    public string sceneName;

    public GameObject mouseimage;

    public Transform ghostarget;
    private Collider2D[] detectedColliders;

    void Start()
    {
        mouseimage.SetActive(false);
        turn_off_jym.SetActive(true);
        turn_on_jym.SetActive(false);
        detectedColliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
    }

    void OnMouseDown()
    {
        if (Inventory.instance != null && Inventory.instance.HasItem("Alcohol_lamp"))
        {
            Inventory.instance.RemoveItem("Alcohol_lamp");
            turn_off_jym.SetActive(false);
            turn_on_jym.SetActive(true);
        }
        Ed2();
    }
   void Ed2()
{
    if (turn_on_jym.activeSelf)
    {
        detectedColliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        bool ghostInRange = false; // 플래그 선언

        // 콜라이더 확인
        foreach (var collider in detectedColliders)
        {
            if (collider.transform == ghostarget)
            {
                ghostInRange = true; // 귀신이 범위 안에 있음
                break;
            }
        }
        mouseimage.SetActive(ghostInRange);
        if (ghostInRange)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}   
        void OnDrawGizmosSelected()
    {
        // 범위 원 시각화
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

}