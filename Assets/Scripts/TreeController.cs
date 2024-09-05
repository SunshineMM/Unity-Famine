using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 树木控制器
/// </summary>
public class TreeController : BaseObject
{

    public Animator animator;

    public override void Hurt(int damage)
    {
        base.Hurt(damage);
        animator.SetTrigger("Hurt");
        PlayAudio(0);
    }

    protected override void Dead()
    {
        base.Dead();
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
