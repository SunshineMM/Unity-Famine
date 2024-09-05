using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckController : MonoBehaviour
{
    private BaseObject owner;
    public int damge;
    private bool canAttack = false;

    public void Init(BaseObject owner, int damage)
    {
        this.damge = damage;
        this.owner = owner;
    }

    /// <summary>
    /// 开启伤害检测
    /// </summary>
    public void StartHIt()
    {
        canAttack = true;
    }

    /// <summary>
    /// 关闭伤害检测
    /// </summary>
    public void StopHit()
    {
        canAttack = false;

    }


    private List<GameObject> lastAttackObjectList = new List<GameObject>();
    private void OnTriggerEnter(Collider other)
    {
        //如果当前允许伤害检测
        if (canAttack)
        {
            //此次伤害还没有检测过这个单位
            if (!lastAttackObjectList.Contains(other.gameObject))
            {
            }
        }
    }

    private void Start() {
        
    }
}
