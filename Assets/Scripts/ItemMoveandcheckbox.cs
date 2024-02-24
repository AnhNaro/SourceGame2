using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMoveandcheckbox : MonoBehaviour
{
    Rigidbody2D rd;
    void Start()
    {
        rd=GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        rd.velocity = Vector2.left * Speed.intance.Sppee;
    }
}
