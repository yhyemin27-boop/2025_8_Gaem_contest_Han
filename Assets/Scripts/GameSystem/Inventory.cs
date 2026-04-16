using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public List<string> items = new List<string>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환해도 유지
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    public bool HasItem(string itemName)
    {
        return items.Contains(itemName);
    }

    public void AddItem(string itemName)
    {
        if (!items.Contains(itemName))
            items.Add(itemName);
    }

    public void RemoveItem(string itemName)
    {
        if (items.Contains(itemName))
            items.Remove(itemName);
    }

    public void AddItem(string itemName, int amount)
    {
        Debug.Log($"{itemName} 아이템 {amount}개 획득!");
        for (int i = 0; i < amount; i++)
        {
            AddItem(itemName);
        }
    }

    public void RemoveItem(string itemName, int amount)
    {
        for (int i = amount; i > 0; i--)
        {
            RemoveItem(itemName);
        }
    }

    public void LoadInventory(List<string> savedItems)
    {
        items.Clear();
        items.AddRange(savedItems);
    }

    public void firstClear()
    {
        items.Clear();
    }
}
