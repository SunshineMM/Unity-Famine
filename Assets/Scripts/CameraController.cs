using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;//跟随的目标
    public Vector3 offset; // 目标的偏移量
    public float transitionSpeed = 2; //过度的速度

    //因为玩家的移动的Updata中执行，理论上最好的情况是玩家先移动，相机再去追
    private void LateUpdate() {
        if (target != null){
            //从当前坐标 平滑 过度到目标点
            transform.position = Vector3.Lerp(transform.position,target.position + offset,transitionSpeed * Time.deltaTime);
        }
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
