using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmenyController : MonoBehaviour
{
    public float speed=3f;
    public bool isVertical;
    private Vector2 movedirection;
    private Rigidbody2D rbody;

    private Animator ani;
    private float changeDirectionTime = 2f;
    private float changeDirectionTimer;

    public ParticleSystem parts;

    //音效
    public AudioClip fixclip;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        changeDirectionTimer = 0;
        rbody = GetComponent<Rigidbody2D>();
        movedirection = isVertical ? Vector2.up : Vector2.right;

       
    }

    // Update is called once per frame
    void Update()
    {
        changeDirectionTimer -= Time.deltaTime;
        if (changeDirectionTimer < 0)
        {
            movedirection *= -1;
            changeDirectionTimer = changeDirectionTime;
        }
        Vector2 position = rbody.position;
        position += movedirection * speed * Time.deltaTime;
        //position.y += movedirection.y * speed * Time.deltaTime;
        rbody.MovePosition(position);

        ani.SetFloat("moveX",movedirection.x);
        ani.SetFloat("moveY", movedirection.y);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController pc = other.gameObject.GetComponent<PlayerController>();
        if (pc != null)
            pc.changehealth(-1);
    }

    public void Fixed()
    {
        if (parts.isPlaying) parts.Stop();
        movedirection = isVertical ? Vector2.up : Vector2.right;
        ani.SetBool("fix", true);
        AudioManager.instance.AudioPlay(fixclip);
    }
}
