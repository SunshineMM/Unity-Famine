using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIBagItem : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public Image bg;
    public Image iconImg;

    public ItemDefine itemDefine;

    private bool isSelect = false;

    public bool IsSelect { get => isSelect; 
        set {
            isSelect = value;
            if(isSelect){
                bg.color = Color.green;
            }else{
                bg.color = Color.white;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        IsSelect = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsSelect = false;
    }

    /// <summary>
    /// 初始化，如果传一个null过来，相当于空格子的逻辑
    /// </summary>
    /// <param name="itemDefine"></param>
    public void Init(ItemDefine itemDefine = null){
        this.itemDefine = itemDefine;
        IsSelect = false;
        if(this.itemDefine == null){
            iconImg.gameObject.SetActive(false);
        }else{
            iconImg.gameObject.SetActive(true);
            iconImg.sprite = itemDefine.Icon;
        }
    }
}
