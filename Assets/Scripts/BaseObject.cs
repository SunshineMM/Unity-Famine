using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 所有单位的基类
/// </summary>
public class BaseObject : MonoBehaviour
{

    public float hp;
    public AudioSource audioSource;
    public List<AudioClip> audioClips;
    public GameObject lootObject; //掉落的物品

    // Hp修改时自动判断死亡
    public float Hp { get => hp;
        set {
            hp = value;

            //检测死亡
            if(hp<=0){
                hp = 0;
                Dead();
            }
            //自动调用Hp更新逻辑
            OnHpUpdate(); 
        }
    }

    protected void PlayAudio(int index){
        audioSource.PlayOneShot(audioClips[index]);
    }

    /// <summary>
    /// 当生命值更新自动调用
    /// </summary>
    protected virtual void OnHpUpdate(){}

    /// <summary>
    /// 死亡
    /// </summary>
    protected virtual void Dead(){

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
