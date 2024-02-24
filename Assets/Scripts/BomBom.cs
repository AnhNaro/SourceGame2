using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomBom : MonoBehaviour
{
    public float SpeedBom;
    [SerializeField] Transform Limit;
    [SerializeField] Transform Limit2;
    Vector2 target;
    public GameData gameData;
    [SerializeField] AudioSource audioSource;
    float Timebom;
    int aa;
    public int DamBom() => aa=gameData.Attack;

    void Start()
    {
        target=Limit2.position;
    }
    private void Update()
    {
        if (Time.timeScale == 0)
        {
            audioSource.Stop();
        }
       if(Vector3.Distance(transform.position, Limit.position) < 0.1f)
        {
            target = Limit2.position;
        }
       if(Vector3.Distance(transform.position,Limit2.position) < 0.1f)
        {
            target = Limit.position;
        }
        
       transform.position=Vector3.MoveTowards(transform.position, target, SpeedBom*Time.deltaTime);
        if(gameObject.activeSelf)
        {
            Timebom += Time.deltaTime;
        }
        if (Timebom > 5)
        {
            gameObject.SetActive(false);
            Timebom = 0;
        }
    }

}
