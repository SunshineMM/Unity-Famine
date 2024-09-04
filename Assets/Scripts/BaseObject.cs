using System.Collections;
using System.Collections.Generic;
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

    protected void PlayAudio(int index){
        audioSource.PlayOneShot(audioClips[index]);
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
