using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushController : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem collectEffect;//拾取特效

    public AudioClip collectclip;//拾取音效
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
            if (pc.getCurrenthealth() != pc.getMaxhealth())
            {
                pc.changehealth(1);
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
