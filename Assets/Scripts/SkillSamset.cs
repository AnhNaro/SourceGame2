using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSamset : MonoBehaviour
{
    public Pooling PooSamset;
    public LayerMask Enemy;
    [Range(0, 100)]
    [SerializeField] float radius;
    Rigidbody2D rd;
    Transform G;
    Vector2 Direction;
    private void Start()
    {
        rd= GetComponent<Rigidbody2D>();    
    }
    private void FixedUpdate()
    {
        rd.velocity = Direction * 60*Time.deltaTime;
    }
    private void Update()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, radius,Enemy);
        if(col != null)
        {
             G= col.transform;
            Direction = G.position - transform.position;
        }
        if (col == null)
        {
            Direction = Vector2.zero;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
