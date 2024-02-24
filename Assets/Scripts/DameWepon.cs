using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DameWepon : MonoBehaviour
{
    public GameData gamedame;
    int dame;
    public int GetDame() => dame = gamedame.Attack;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Khi"))
        {
            gameObject.SetActive(false);    
        }
        if (collision.CompareTag("Finish"))
        {
            gameObject.SetActive(false);    
        }
    }
}
