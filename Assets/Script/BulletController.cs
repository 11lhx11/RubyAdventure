using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody2D rbody;

    //音效
    public AudioClip hitclip;

    // Start is called before the first frame update
    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(Vector2 direction, float force)
    {
        rbody.AddForce(force*direction);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        EmenyController ec = other.gameObject.GetComponent<EmenyController>();
        if (ec != null)
            ec.Fixed();
        AudioManager.instance.AudioPlay(hitclip);
        Destroy(this.gameObject);
    }
}
