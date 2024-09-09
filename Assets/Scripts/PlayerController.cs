using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

public class PlayerController : BaseObject
{
    public static PlayerController Instance;
    public Animator animator;
    public CharacterController characterController;
    public CheckController checkController;
    public float moveSpeed = 1;
    private Quaternion quaternion;

    private bool isAttacking = false;

    private float hungry = 100f;

    private bool isHurt = false;
    public float Hungry { get => hungry; 
    set{
        hungry = value;
            if (hungry <= 0)
            {
                hungry = 0;
                //衰减Hp
                Hp -= Time.deltaTime / 2;
            }
            //更新饥饿值UI
            hungryImg.fillAmount = hungry / 100;
        } 
    }

    public Image hpImg;
    public Image hungryImg;

    private void Awake() {
        Instance = this;
        //初始化检测器
        checkController.Init(this,30);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHungry();
        //既不在攻击中，也不在受伤中        
        if(!isAttacking && !isHurt){
            Move();
            Attack();
        }else{
            //攻击过程中 test
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
            //当前鼠标正在交互UI
            if(UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()){
                return;
            }

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

    /// <summary>
    /// 更新饥饿值
    /// </summary>
    private void UpdateHungry(){
        Hungry -= Time.deltaTime * 3;
    }

    public override void Hurt(int damage)
    {
        base.Hurt(damage);
        animator.SetTrigger("Hurt");
        PlayAudio(2);
        isHurt = true;
    }

    protected override void OnHpUpdate()
    {
        //更新血条
        hpImg.fillAmount = Hp / 100;
    }


    public override bool AddItem(ItemType itemType)
    {
        //检测背包能不能放下
        return UIBagPanel.Instance.AddItem(itemType);
    }

    #region 动画事件
    private void StartHit(){
        //播放音效
        PlayAudio(0);
        //攻击检测
        checkController.StartHIt();
    }
    
    private void StopHit(){
        //停止攻击检测
        isAttacking =  false;
        checkController.StopHit();
    }

    private void HurtOver(){
        isHurt = false;
    }
    #endregion
}
