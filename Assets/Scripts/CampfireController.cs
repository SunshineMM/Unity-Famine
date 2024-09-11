using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireController : MonoBehaviour
{
    public Light CampfireLight;
    private float time = 20; //最大的燃烧时间
    private float currentTime = 20; //当前剩余燃烧时间


    private void Update() {
        if (currentTime <= 0){
            currentTime = 0;
            CampfireLight.transform.parent.gameObject.SetActive(false);
        }
        else {
            currentTime -= Time.deltaTime;
        }

        CampfireLight.intensity = Mathf.Clamp(currentTime/time,0,1) * 10f;


    }

    public void AddWood(){
        currentTime += 10;
        CampfireLight.transform.parent.gameObject.SetActive(true);
    }
}
