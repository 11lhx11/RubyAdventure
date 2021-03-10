using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rbody;

    private Vector2 lookDirection;

    private int maxhealth = 5;//最大生命值
    private int currenthealth;//当前生命值
    private int minhealth = 0;//最小 生命值

    //子弹设置
    private int maxBulletNum = 99;
    private int minBulletNum = 0;
    private int currentBulletNum;

    private float invisibleTime = 2f;//无敌时间
    private float invisibleTimer;//倒计时
    private bool isInvisible;   //是否处于无敌状态

    private Animator anim;

    public GameObject bulletpreab;//子弹

    //音效
    public AudioClip launchclip;
    public AudioClip hitclip;
    // Start is called before the first frame update
    void Start()
    {
        currenthealth = 2;
        currentBulletNum = 2;
        UiManager.instance.UpdateBulletNum(currentBulletNum, maxBulletNum);
        rbody = GetComponent<Rigidbody2D>();

        invisibleTimer = 0;
        isInvisible = false;

        anim = GetComponent<Animator>();
        lookDirection = new Vector2(1, 0);

        UiManager.instance.UpdateHealthBar(currenthealth, maxhealth);
    }

    // Update is called once per frame
    void Update()
    {
       

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        float isMove = 0;
        Vector2 tempVector = new Vector2(x, y);
        if (tempVector.x != 0 || tempVector.y != 0)
        { 
            lookDirection = tempVector;
            isMove = 1;
        }
        anim.SetFloat("moveX", lookDirection.x);
        anim.SetFloat("moveY", lookDirection.y);
        anim.SetFloat("speed", isMove);

        Vector2 position = rbody.position;
        //position.x += x*speed*Time.deltaTime;
        //position.y += y*speed*Time.deltaTime;
        position += tempVector * speed * Time.deltaTime;
        rbody.MovePosition(position);

        if (Input.GetKeyDown(KeyCode.J)&&currentBulletNum>0)
        {
            changeBulletNum(-1);
            GameObject bullet = Instantiate(bulletpreab, rbody.position,Quaternion.identity);
            BulletController bc = bullet.GetComponent<BulletController>();
            if (bc != null)
            {
                bc.Move(lookDirection, 300);
            }
            AudioManager.instance.AudioPlay(launchclip);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit2D hit = Physics2D.Raycast(rbody.position, lookDirection, 2f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NpcController nc = hit.collider.GetComponent<NpcController>();
                if (nc != null) nc.ShowDialog();
                Debug.Log("玩家与NPC交互");
            }
        }
        //无敌倒计时
        if (isInvisible) {
            invisibleTimer -= Time.deltaTime;
            if (invisibleTimer < 0) {
                isInvisible = false;
            }
        }
    }
    public int getCurrenthealth()
    {
        return currenthealth;
    }
    public int getMaxhealth()
    {
        return maxhealth;
    }
    public int getMaxBulletNum()
    {
        return maxBulletNum;
    }

    public int getCurrnetBulletNum()
    {
        return currentBulletNum;
    }

    public void changeBulletNum(int b)
    {
        currentBulletNum = Mathf.Clamp(currentBulletNum + b, minBulletNum, maxBulletNum);
        UiManager.instance.UpdateBulletNum(currentBulletNum, maxBulletNum);
    }
    public void changehealth(int h)
    {
        if (h < 0)
        {
            if (isInvisible) return;
            isInvisible = true; invisibleTimer = invisibleTime;
            AudioManager.instance.AudioPlay(hitclip);
        }
        int temp = currenthealth + h;
        Debug.Log("当前玩家生命值:" + currenthealth +"/"+ maxhealth);
        if (h > 0) currenthealth = temp < maxhealth ? temp : maxhealth;
        else currenthealth = temp <= minhealth ? minhealth:temp;
        UiManager.instance.UpdateHealthBar(currenthealth, maxhealth);
        Debug.Log("当前玩家生命值:" + currenthealth +"/"+ maxhealth);

    }
}

