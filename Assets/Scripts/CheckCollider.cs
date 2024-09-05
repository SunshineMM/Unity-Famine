using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 检测碰撞物体
/// </summary>
public class CheckCollider : MonoBehaviour
{
    private BaseObject owner;
    public int damge;
    private bool canAttack = false;

    public void Init(BaseObject owner,int damage){
         this.damge = damage;
         this.owner = owner;
    }

    /// <summary>
    /// 开启伤害检测
    /// </summary>
    public void StartHIt(){
        canAttack = true;
    }
    
    /// <summary>
    /// 关闭伤害检测
    /// </summary>
    public void StopHit(){
        canAttack = false;

    }


    private List(GameObject) lastAttackObjectList = new List<GameObject>();
    private void OnTriggerEnter(Collider other) {
        //如果当前允许伤害检测
        if(canAttack){
            if(lastAttackObjectList)
        }
    }
}
