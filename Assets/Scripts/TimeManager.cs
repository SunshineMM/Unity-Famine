using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class TimeManager : MonoBehaviour
{

    
    public static TimeManager Instance;

    public Light sunLight;
    public float  dayTime; //白天时间
    public float dayToNightTime;//白天到晚上的时间
    public float nightTime;//晚上时间
    public float nightToDayTime;//晚上到白天的时间

    private float lightValue = 1;

    private float dayNum=0;

    public Image timeStateImg;
    public TMP_Text dayNumText;
    public Sprite[] dayStateSprites; //白天和晚上的图片

    private bool isDay = true;

    public bool IsDay { get => isDay; set {
            isDay = value;
            if(isDay){
                dayNum += 1;
                dayNumText.text = "Day "+dayNum;
                timeStateImg.sprite = dayStateSprites[0];
            }else{
                timeStateImg.sprite = dayStateSprites[1];
            }
    }
    }

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        IsDay = true;
        //计算时间
        StartCoroutine(UpdateTime());
    }    


    private IEnumerator UpdateTime(){
        while(true){
            yield return null;
            //当前是白天
            if(IsDay){
                lightValue -= 1 / dayToNightTime*Time.deltaTime;
                Debug.Log("灯光的值："+lightValue);
                SetLightValue(lightValue);
                if(lightValue<=0){ 
                    IsDay = false;
                    yield return new WaitForSeconds(nightTime); //等待夜晚过去
                }
            }else{
                //当前是夜晚
                lightValue += 1 / nightToDayTime * Time.deltaTime;
                Debug.Log("灯光的值：" + lightValue);
                SetLightValue(lightValue);

                if (lightValue >= 1)
                {
                    IsDay = true;
                    yield return new WaitForSeconds(dayTime); //等待白天过去
                }
            }
        }
    }

    /// <summary>
    /// 设置灯光的值
    /// </summary>
    /// <param name="value"></param>
    private void SetLightValue(float value){
        RenderSettings.ambientIntensity = value;
        sunLight.intensity = value;
    }
}
