using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossAtack : MonoBehaviour
{
    public GameData gameData;
    Atribuildhandle handleboss;
    [SerializeField] float speed;
    Vector2 Target;
    [SerializeField] Transform A;
    [SerializeField] Transform B;
    [SerializeField] Transform Poitshoot;
    public LayerMask Player;
    [Range(0, 100)]
    [SerializeField] float far;
    public Pooling poobulletboss;
    [SerializeField] float SpeedBullet;
    [SerializeField] Text Hpenemy;
    [SerializeField] GameObject fxdeath;
    [SerializeField] GameObject CointEnemy;
    [SerializeField] AudioSource audioDeathofEnemy;
    [SerializeField] AudioSource audioAttack;
    private void Awake()
    {
        handleboss = new Atribuildhandle(gameData);
    }
    void Start()
    {
        Target = B.position;
        handleboss.Init();
    }

    bool Checkplayer;// Update is called once per frame
    float timeshoot;
    float timetrigger;
    void Update()
    {
        Hpenemy.text = handleboss.Curenhp.ToString();
        Debug.Log("Hpenemy " + handleboss.Curenhp);
        timeshoot += Time.deltaTime;
        Checkplayer = Physics2D.Raycast(transform.position, Vector2.left, far, Player);
            if (Vector2.Distance(transform.position, Target) < 0.1f)
            {
                Target = B.position;
            }
        if (Vector2.Distance(transform.position, Target) < 0.1f)
        {
            Target = A.position;
        }
        transform.position = Vector3.MoveTowards(transform.position, Target, speed * Time.deltaTime);
        if (Checkplayer && timeshoot>=1)
        {
            audioAttack.Play();
            Atack();
            timeshoot = 0;
        }
        if (handleboss.Curenhp <= 0)
        {
            Spawfx();
            handleboss.Curenhp = 0;
            GameManager.Instance.ConditionSpawChain = true;
            GameManager.Instance.Conditiontiemspawboss = true;
            GameManager.Instance.TinhDiemCongVang++;
            gameObject.SetActive(false);
        }
        timetrigger += Time.deltaTime;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position,transform.position+Vector3.left*far);
    }
    void Atack()
    {
        GameObject clone = poobulletboss.GetEsp();
        clone.gameObject.SetActive(true);
        clone.TryGetComponent(out Rigidbody2D rd);
        rd.velocity = Vector2.left * SpeedBullet;
        clone.transform.position=Poitshoot.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Samset"))
        {
            collision.TryGetComponent(out DameWepon aa);
            handleboss.TakenDame(aa.GetDame());
            collision.gameObject.SetActive(false);
        }
        if (collision.CompareTag("Bom2"))
        {
            collision.TryGetComponent(out BomBom bb);
            handleboss.TakenDame(bb.DamBom());
        }
        if (collision.CompareTag("Danphep"))
        {
            collision.TryGetComponent(out DameWepon b);
            handleboss.TakenDame(b.GetDame());
            collision.gameObject.SetActive(false);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Bom2"))
        {
            if (timetrigger > 1)
            {
                collision.TryGetComponent(out BomBom ab);
                handleboss.TakenDame(ab.DamBom());
                timetrigger = 0;
            }
        }
        
    }
    void Spawfx()
    {
        audioDeathofEnemy.Play();
        GameObject Cop = Instantiate(fxdeath, transform.position, Quaternion.identity);
        Cop.SetActive(true);
        GameObject coi=Instantiate(CointEnemy,transform.position, Quaternion.identity); 
        coi.SetActive(true);
        GameObject coi2 = Instantiate(CointEnemy, transform.position, Quaternion.identity);
        coi2.SetActive(true);
    }
}
