using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum EnemyState{
    Idle,
    Move,
    Pursue,
    Attack,
    Hurt,
    Die
}

public class PigController : BaseObject
{

    public Animator animator;
    public NavMeshAgent navMeshAgent;
    public CheckController checkController;
    //行动范围
    public float maxX = 4.74f;
    public float minX = -5.62f;
    public float maxZ = 5.92f;
    public float minZ = -6.33f;

    private EnemyState enemyState;
    private Vector3 targePos;

    public EnemyState EnemyState { get => enemyState; 
        set{
            enemyState = value;
            switch (enemyState)
            {
                case EnemyState.Idle:
                    //播放动画
                    //关闭导航
                    //休息一段时间之后去巡逻
                    animator.CrossFadeInFixedTime("Idle",0.25f);
                    navMeshAgent.enabled = false;
                    Invoke(nameof(GoMove),Random.Range(3f,10f));
                    break;
                case EnemyState.Move:
                    //播放动画
                    //开启导航
                    //获取巡逻点
                    //移动到指点位置
                    animator.CrossFadeInFixedTime("Move",0.25f);
                    navMeshAgent.enabled = true;
                    targePos = GetTargetPos();
                    navMeshAgent.SetDestination(targePos);
                    break;
                case EnemyState.Pursue:
                     animator.CrossFadeInFixedTime("Move",0.25f);
                     navMeshAgent.enabled = true;
                    break;
                case EnemyState.Attack:
                    animator.CrossFadeInFixedTime("Attack",0.25f);
                    transform.LookAt(PlayerController.Instance.transform.position);
                    navMeshAgent.enabled = false;
                    break;
                case EnemyState.Hurt:
                    animator.CrossFadeInFixedTime("Hurt",0.25f);
                    PlayAudio(0);
                    navMeshAgent.enabled = false;
                    break;
                case EnemyState.Die:
                   animator.CrossFadeInFixedTime("Die",0.25f);
                    PlayAudio(0);
                    navMeshAgent.enabled = false;
                    break;
            }
        }
    }

    private void Start() {
        checkController.Init(this,10);
        EnemyState = EnemyState.Idle;
    }

    private void Update() {
        StateOnUpdate(); 
    }

    private void StateOnUpdate(){
          switch (enemyState)
            {
        
                case EnemyState.Move:
                    if(Vector3.Distance(transform.position,targePos) < 1.5f){
                        EnemyState = EnemyState.Idle;
                    }
                    break;
                case EnemyState.Pursue:
                    //距离近 攻击
                    if(Vector3.Distance(transform.position,PlayerController.Instance.transform.position)< 1){
                        EnemyState = EnemyState.Attack;
                    }else{
                    // 距离远 继续追击
                    navMeshAgent.SetDestination(PlayerController.Instance.transform.position);
                    }
                    break;
        
            } 
    }

    private void GoMove(){
         EnemyState = EnemyState.Move;
    }

    /// <summary>
    /// 获取一个范围内的随机点
    /// </summary>
    private Vector3 GetTargetPos(){
        return new Vector3(Random.Range(minX,maxX),0,Random.Range(minZ,maxZ));
    }

    public override void Hurt(int damage)
    {   
        if(EnemyState == EnemyState.Die ) return;
        CancelInvoke(nameof(GoMove)); //取消移动
        base.Hurt(damage);
        if(Hp > 0){ //没死 切换受伤
            EnemyState = EnemyState.Hurt;
        }
    }

    protected override void Dead()
    {
        base.Dead();
        EnemyState  =  EnemyState.Die;
    }

    #region 
    private void StartHit(){
        checkController.StartHIt();
    }

    private void StopHit(){
        checkController.StopHit();
    }

    private void StopAttack(){
    
        if(EnemyState != EnemyState.Die) {
            EnemyState = EnemyState.Pursue;
        }
    }

    private void HurtOver(){
        if(EnemyState != EnemyState.Die) {
            EnemyState = EnemyState.Pursue;
        }
    }

    private void Die(){
        Destroy(gameObject);
    }
    #endregion
}
