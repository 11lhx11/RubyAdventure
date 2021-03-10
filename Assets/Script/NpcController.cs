using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NpcController : MonoBehaviour
{
    public GameObject dialog;
    public GameObject tip;

    private float communTime=5f;
    private float communTimer;
    // Start is called before the first frame update
    void Start()
    {
        dialog.SetActive(false);
        tip.SetActive(true);
        communTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(communTimer>=0)communTimer -= Time.deltaTime;
        if (communTimer < 0)
        {
            dialog.SetActive(false);
            tip.SetActive(true);
        }
    }

    public void ShowDialog()
    {
        dialog.SetActive(true);
        tip.SetActive(false);
        communTimer = communTime;
    }
}
