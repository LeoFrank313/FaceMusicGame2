using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartControll : MonoBehaviour
{
    public GameObject aniObject;
    public GameObject faces;

    private Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        ani = aniObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("finish"))
        {
            GameManager.instance.GameStart = true;
            faces.SetActive(true);
        }

    }
}
