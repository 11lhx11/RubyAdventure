using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UiManager : MonoBehaviour
{
    public static UiManager instance { get; private set; }
    public  Image healthBar;
    public Text bulletBag;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateBulletNum(int currentBullet, int maxBullet)
    {
        if (bulletBag != null) bulletBag.text = currentBullet.ToString() + '/' + maxBullet.ToString();
    }
    public  void UpdateHealthBar(int currentHealth, int maxHealth)
    {
       if(healthBar!=null) healthBar.fillAmount = (float)currentHealth / (float)maxHealth;
    }
}
