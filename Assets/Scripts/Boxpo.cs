using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxpo : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            collision.gameObject.SetActive(false);  
        }
        if (collision.CompareTag("Danphep"))
        {
            collision.gameObject.SetActive(false);  
        }
        if (collision.CompareTag("Samset"))
        {
            collision.gameObject.SetActive(false);  
        }
    }
}
