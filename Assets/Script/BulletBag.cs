using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBag : MonoBehaviour
{
    public ParticleSystem collectEffect;//拾取特效
    public AudioClip collectclip;//拾取音效
    public int bulletNum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController pc = other.GetComponent<PlayerController>();
        if (pc != null)
        {
            if (pc.getCurrnetBulletNum() < pc.getMaxBulletNum())
            {
                pc.changeBulletNum(bulletNum);
                //生成特效
                Instantiate(collectEffect, transform.position, Quaternion.identity);
                //触发拾取声音
                AudioManager.instance.AudioPlay(collectclip);
                Destroy(this.gameObject);
            }
            Debug.Log("tank触碰到了灌木丛");
        }
    }
}
