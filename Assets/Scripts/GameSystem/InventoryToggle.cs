using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class InventoryToggle : MonoBehaviour
{
    public GameObject INVRNTORYhttm;       // 인벤토리 전체 UI (씬마다 새로 찾음)
    public Transform itemListParent;       // 슬롯들을 담을 부모
    public static InventoryToggle instance;
    public GameObject itemSlotPrefab;      // Project에 저장된 Prefab
    private bool isInventoryOpen;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            if (transform.parent != null)
                Debug.LogError("InventoryToggle은 루트 오브젝트여야 합니다.");
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        if (itemSlotPrefab == null)
        {
            Debug.LogError("itemSlotPrefab을 Inspector에서 연결하세요!");
        }

        FindInventoryUI();

        if (INVRNTORYhttm != null)
            INVRNTORYhttm.SetActive(false);

        isInventoryOpen = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && SceneManager.GetActiveScene().name != "firstrull")
        {
            isInventoryOpen = !isInventoryOpen;

            if (INVRNTORYhttm != null)
                INVRNTORYhttm.SetActive(isInventoryOpen);

            if (isInventoryOpen)
                RefreshUI();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬 이동 후 UI 다시 연결
        FindInventoryUI();

        if (isInventoryOpen)
            RefreshUI();
    }

    void FindInventoryUI()
    {
        INVRNTORYhttm = GameObject.Find("INVRNTORYhttm");
        if (INVRNTORYhttm == null)
        {
            Debug.LogWarning("INVRNTORYhttm를 찾지 못했습니다.");
            return;
        }
        else
        {
            Debug.Log("INVRNTORYhttm를 찾았습니다.");
        }

        itemListParent = null;
        Transform[] children = INVRNTORYhttm.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in children)
        {
            if (t.name == "itemListParent")
            {
                itemListParent = t;
                break;
            }
        }

        if (itemListParent != null)
            Debug.Log("itemListParent를 찾았습니다.");
        else
            Debug.LogWarning("itemListParent를 찾지 못했습니다.");
    }

    void RefreshUI()
    {
        if (itemListParent == null || itemSlotPrefab == null || Inventory.instance == null)
        {
            Debug.LogWarning("RefreshUI() 호출 조건 불충분: itemListParent 또는 itemSlotPrefab 또는 Inventory.instance가 null");
            return;
        }

        // 기존 슬롯 삭제
        foreach (Transform child in itemListParent)
        {
            Destroy(child.gameObject);
        }

        // 아이템 목록 표시
        foreach (string itemName in Inventory.instance.items)
        {
            GameObject slot = Instantiate(itemSlotPrefab, itemListParent);
            slot.name = itemName; // 슬롯 이름을 아이템 이름으로 지정
            TextMeshProUGUI nameText = slot.GetComponentInChildren<TextMeshProUGUI>();
            if (nameText != null)
                nameText.text = itemName;
        }
    }
}
