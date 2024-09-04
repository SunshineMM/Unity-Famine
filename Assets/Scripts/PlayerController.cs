using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseObject
{
    public static PlayerController Instance;
    public Animator animator;
    public CharacterController characterController;

    public float moveSpeed = 1;
    private Quaternion quaternion;

    private bool isAttacking = false;

    private void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAttacking){
            Move();
            Attack();
        }else{
            //攻击过程中
            transform.localRotation = Quaternion.Slerp(transform.localRotation,quaternion,Time.deltaTime * 10);
        }
     
    }

    private void Move()
    {
        //获取玩家的输入
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        //玩家没有进行移动
        if(h==0 && v == 0){
            animator.SetBool("Walk",false);
        }else{
            animator.SetBool("Walk",true);
            //获取最终目标方向 过渡过去
            quaternion = Quaternion.LookRotation(new Vector3(h,0,v));
            transform.localRotation = Quaternion.Slerp(transform.localRotation,quaternion,Time.deltaTime * 10f);

            //处理实际的移动
            characterController.SimpleMove(new Vector3(h,0,v).normalized * moveSpeed);
        }
    }


    /// <summary>
    /// 攻击
    /// </summary>
    private void Attack(){
        //检测攻击按键
        if(Input.GetMouseButton(0)){
           
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out RaycastHit hisInfo,100,LayerMask.GetMask("Ground"))){
                //说明碰到地面了
                animator.SetTrigger("Attack");
                 //进入攻击状态
                isAttacking = true; 
                //转向到攻击点
                quaternion = Quaternion.LookRotation(hisInfo.point - transform.position);

            }
        }
    }

    #region 动画事件
    private void StartHit(){
        //播放音效
        PlayAudio(0);
        //攻击检测
    }
    
    private void StopHit(){
        //停止攻击检测
        isAttacking =  false;
    }
    #endregion
}
