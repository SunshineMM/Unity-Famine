using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

/// <summary>
/// 背包面板
/// </summary>
public class UIBagPanel : MonoBehaviour
{
    public static UIBagPanel Instance;
    private UIBagItem[] items;
    public GameObject itemPrefab;
    private void Awake() {
        Instance = this;
       
    }

    private void Start() {
        items =  new UIBagItem[5];
        UIBagItem item = Instantiate(itemPrefab,transform).GetComponent<UIBagItem>();
        //先生成篝火
        item.Init(ItemManager.Instance.GetItemDefine(ItemType.Campfire));
        items[0] = item;
        for(int i = 1; i < 5; i++){
            item = Instantiate(itemPrefab,transform).GetComponent<UIBagItem>();
            item.Init(null);
            items[i] = item;
        }
  
    }

    /// <summary>
    /// 添加物品
    /// </summary>
    /// <param name="itemType"></param>
    /// <returns></returns>
    public bool AddItem(ItemType itemType){
        for(int i = 0; i<items.Length; i++){
            if(items[i].itemDefine == null){
                ItemDefine itemDefine = ItemManager.Instance.GetItemDefine(itemType);
                items[i].Init(itemDefine);
                return true;
            }
        }
        return false;
    }
}


