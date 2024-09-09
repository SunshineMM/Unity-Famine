using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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

    public void PlayAudio(int index){
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
        if(lootObject != null){
            Instantiate(lootObject,transform.position+ new Vector3(Random.Range(-0.5f,0.5f),Random.Range(1f,1.2f), Random.Range(-0.5f, 0.5f)),
            Quaternion.identity,null);
        }
    }

    /// <summary>
    /// 受伤
    /// </summary>
    public virtual void Hurt(int damage){
        Hp -= damage;
        
    }

    /// <summary>
    /// 添加物品
    /// </summary>
    /// <returns></returns>
    public virtual bool AddItem(ItemType itemType){
        return false;
    }
}
