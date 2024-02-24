using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    Rigidbody2D rd;
    void Start()
    {
     rd=GetComponent<Rigidbody2D>();   
    }
    private void FixedUpdate()
    {
        rd.velocity = Vector2.left *Speed.intance.Sppee;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            gameObject.SetActive(false);    
        }
    }
}