using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acativate : MonoBehaviour
{
    Rigidbody2D rb;
    public bool activate;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        activate = false;
        rb.velocity = new Vector3(0,2,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
