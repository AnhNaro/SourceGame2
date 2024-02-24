using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class RunBoss : MonoBehaviour
{
    public Pooling poobulet;
    public GameObject Khien;
    public GameObject Skill;
    [SerializeField] Transform poitkhien;
    public List<Transform> poitshoot=new List<Transform>();
    [SerializeField] Transform poitSkill1;
    public GameData data;
    Atribuildhandle handle;
    float time;
    float timeEnable;
    bool checktime=true;
    bool checkCircel=true;
    [SerializeField] float Speed;
    [SerializeField] GameObject Fxdeath;
    [SerializeField] GameObject CointEnemy;   
    public int ValueAddx;
    public int ValueAddy;
    [SerializeField] Text txthpbb;
    [SerializeField] AudioSource audioAttack;
    [SerializeField] AudioSource audiodeath;
    private void Start()
    {
        handle = new Atribuildhandle(data);
        handle.Init();
    }
    GameObject clon;
    float timebossattack;
    float timebossSkill;
    bool TT=false;
    void Update()
    {
        txthpbb.text=handle.Curenhp.ToString(); 
        Debug.Log("HpBoss " + handle.Curenhp);
        if (handle.Curenhp < 0)
        {
            handle.Curenhp = 0;
            Spawfxdeath();
            GameManager.Instance.ConditionSpawChain = true;
            GameManager.Instance.Conditiontiemspawboss = true;
            if (clon != null)
            {
                clon.gameObject.SetActive(false);
            }
            if (Copy2 != null)
            {
                Copy2.gameObject.SetActive(false);
            }
            GameManager.Instance.TinhDiemCongVang++;
            audiodeath.Play();
            gameObject.SetActive(false);
        }
        if (checktime)
        {
            time += Time.deltaTime;
        }
        if (time > 3&&checktime)
        {
            checkCircel = true;
             clon = Instantiate(Khien,poitkhien.position,Quaternion.identity);
            clon.SetActive(true);
            checktime = false;
            TT = true;//xet dam va cham
            time = 0;
        }
        if (!checktime)
        {
            timebossSkill += Time.deltaTime;
            timeEnable += Time.deltaTime;
            if (timeEnable > 10)
            {
                checkCircel = false;
                clon.gameObject.SetActive(false);
                TT = false;
            }
            if(timeEnable > 17)
            {
                checktime = true;
                timeEnable = 0;
            }
        }
        if(timebossSkill > 20 && !checkCircel)// Sound phan than chi thuat
        {
            if (GameManager.Instance.ConditionSpawbossCopy) return;
            BossSkill();
            timebossSkill = 0;
        }
        timebossattack += Time.deltaTime;
        if(timebossattack > 1)
        {
            audioAttack.Play();
            BossAttack();
            timebossattack = 0;
        }
        timetrigger += Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Danphep"))
        {
            if (!TT)
            {
                collision.TryGetComponent(out DameWepon a);
                handle.TakenDame(a.GetDame());
                collision.gameObject.SetActive(false);
            }
        }
        if (collision.CompareTag("Samset"))
        {
           if(!TT)
            {
                collision.TryGetComponent(out DameWepon aa);
                handle.TakenDame(aa.GetDame());
                collision.gameObject.SetActive(false);
            }
        }
        if (collision.CompareTag("Bom2"))
        {
           if(!TT)
            {
                collision.TryGetComponent(out BomBom bb);
                handle.TakenDame(bb.DamBom());
            }
        }
    }
    void BossAttack()
    {
        Transform aa = poitshoot[Random.Range(0, poitshoot.Count)];
        GameObject Copy = poobulet.GetEsp();
        Copy.SetActive(true);
        Copy.TryGetComponent(out Rigidbody2D rd);
        rd.velocity = Vector2.left*Speed;
        Copy.transform.position = aa.position;
    }
    GameObject Copy2;
    void BossSkill()
    {
        Copy2 = Instantiate(Skill, poitSkill1.position, Quaternion.identity);
        Copy2.SetActive(true);
        Copy2.TryGetComponent(out Rigidbody2D rd);
        rd.velocity = Vector2.left*Speed;
    }
    float timetrigger;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Bom2"))
        {
            if (timetrigger > 1 && !TT)
            {
                collision.TryGetComponent(out BomBom ab);
                handle.TakenDame(ab.DamBom());
                timetrigger = 0;
            }
        }
    }
    void Spawfxdeath()
    {
        GameObject cop= Instantiate(Fxdeath,new Vector2(transform.position.x-ValueAddx,transform.position.y-ValueAddy),Quaternion.identity);
        cop.SetActive(true);
        GameObject coi = Instantiate(CointEnemy, transform.position, Quaternion.identity);
        coi.SetActive(true);
        GameObject coi2= Instantiate(CointEnemy, transform.position, Quaternion.identity);
        coi2.SetActive(true);
    }

}
