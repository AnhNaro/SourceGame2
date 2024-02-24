using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playercontroler : MonoBehaviour
{
    [Space,Header("playerAttack")]
    public Pooling playerattack;
    [SerializeField] Transform Poitattack;
    [SerializeField] float Speeddanphep;
    [Space]
    public GameData gameData;
    Atribuildhandle handle;
    Vector2 gavity;
    Rigidbody2D rd;
    [SerializeField] float Speedjump;
    [SerializeField] float luc;
    public LayerMask maskground;
    bool checkground;
    bool ConditionDoubleJump;
    [SerializeField] Transform poitcheckground;
    public Animator animator;
    [SerializeField] GameObject Circel;
    bool circelcheck=false;
    bool CountStart=false;
    float counttime;
    float counttimeCircel;
    [SerializeField] Button butSamset;
    [SerializeField] Button butKhi;
    [SerializeField] Button butBom;
    [SerializeField] Button butCauvong;
    [Space( )]
    [Header("HoiSinhplayer")]
    bool ConditionDeletchain=false;
    [SerializeField] Transform PoitdeletChain;
    [SerializeField] float Radius;
    public LayerMask MaskChain;
    [SerializeField] Text txtHpplayer;
    [SerializeField] Text txtScorePlayer;
    float Countscore;
    int Converserscore;
    [Header("Audio")]
    [SerializeField] AudioSource audiojump;
    [SerializeField] AudioSource audiodoublejump;
    [SerializeField] AudioSource audioAttackplayer;
    [SerializeField] AudioSource audioSkillKhien;
    [SerializeField] AudioSource audioSkillCauVong;
    [SerializeField] AudioSource audioSkillBom;
    [SerializeField] AudioSource audioSkillSamSet;
    [SerializeField] AudioSource audioCollect;
    private void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
        butSamset.onClick.AddListener(btnSamset);
        butKhi.onClick.AddListener(btnKhi);
        butBom.onClick.AddListener(btnBom);
        butCauvong.onClick.AddListener (btnCauvong);
        handle = new Atribuildhandle(gameData);
    }

    void Start()
    {
        gavity =new Vector2(0,-Physics2D.gravity.y);
        handle.Init();
    }
    private void FixedUpdate()
    {
        if (!GameManager.Instance.ConditionplayGame) return;
        checkground = Physics2D.OverlapBox(poitcheckground.position, new Vector2(3, 1), 0, maskground);
        animator.SetFloat("treeanim", 1);
        animator.SetBool("jump", !checkground);
        if (rd.velocity.y < 0)
        {
            animator.SetFloat("yanim", -1);
            rd.velocity -= gavity * luc * Time.fixedDeltaTime * 1.6f;
        }
        if (rd.velocity.y > 0)
        {
            rd.velocity -= gavity * luc * Time.fixedDeltaTime;
        }
    }
    private void Update()
    {
        if (cchp)
        {
            handle.Init();
            cchp = false;
        }
        if (!GameManager.Instance.ConditionplayGame) return;
        Countscore += Time.deltaTime;
        Converserscore = (int)Countscore;
        txtScorePlayer.text = Converserscore.ToString();
        txtHpplayer.text = handle.Curenhp.ToString();
        GameManager.Instance.Score = Converserscore;
        GameManager.Instance.txtScore.text = string.Format($"Điểm: {Converserscore}");
        GameManager.Instance.Diem.text= string.Format($"Điểm: {Converserscore}");
        if (handle.Curenhp <= 0)
        {
            gameObject.SetActive(false);
            GameManager.Instance.PlayerDeath = true;
            GameManager.Instance.PlayerDeath2 = true;
            handle.Curenhp = 1;
        }
        counttime += Time.deltaTime;
        if (counttime > .5f && !GameManager.Instance.Conditiontiemspawboss)
        {
            audioAttackplayer.Play();
            PlayerAttack();
            counttime = 0;
        }
        Debug.Log("hpplayer " + handle.Curenhp);
        Collider2D[] colii = Physics2D.OverlapCircleAll(PoitdeletChain.position, Radius, MaskChain);//deletchain
        if (ConditionDeletchain)
        {
            if (colii != null)
            {
                for (int i = 0; i < colii.Length; i++)
                {
                    colii[i].gameObject.SetActive(false);
                }
                ConditionDeletchain = false;
            }
        }
        if (counttimeCircel == 0)
        {
        CountStart = false;
        }
        if (circelcheck)
        {
            counttimeCircel += Time.deltaTime;
        }
        if (CountStart)
        {
            counttimeCircel = 0; 
        }
        if (counttimeCircel >= 9)
        {
            circelcheck = false;
            CountStart=false;
            counttimeCircel = 0;    
        }
    }
    public void MovePlayer()
    {
        if (checkground)
        {
            audiojump.Play();
            animator.SetFloat("yanim", 1);
            rd.velocity = new Vector2(rd.velocity.x, Speedjump);
            ConditionDoubleJump = true;
        }
        else
        {
            if (ConditionDoubleJump)
            {
             audiodoublejump.Play();    
             animator.SetFloat("yanim", 2);
             rd.velocity = new Vector2(rd.velocity.x, Speedjump);
             ConditionDoubleJump = false;
            }
          
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Chain"))
        {
            if (!circelcheck)
            {
                collision.TryGetComponent(out DameWepon b);
                handle.TakenDame(b.GetDame());
            }
        }
        if (collision.CompareTag("Skine"))
        {
            if (!circelcheck)
            {
                collision.TryGetComponent(out DameWepon c);
                handle.TakenDame(c.GetDame());
            }
        } 
        if (collision.CompareTag("Vong"))
        {
            if (!circelcheck)
            {
                collision.TryGetComponent(out DameWepon v);
                handle.TakenDame(v.GetDame());
            }
        }
        if (collision.CompareTag("Bullet"))
        {
            if (!circelcheck)
            {
                collision.TryGetComponent(out DameWepon ke);
                handle.TakenDame(ke.GetDame());
            }
        }
        if (collision.CompareTag("Itemcauvong"))
        {
            audioCollect.Play();
            SaveItemPowerandName.instance.Addcauvong();
            collision.gameObject.SetActive(false);
        }
        if (collision.CompareTag("Itemhp"))
        {
            audioCollect.Play();
            handle.Curenhp++;
            collision.gameObject.SetActive(false);
        }
        if (collision.CompareTag("Itembom"))
        {
            audioCollect.Play();
            SaveItemPowerandName.instance.Addbom();
            collision.gameObject.SetActive(false);
        }
        if (collision.CompareTag("Itemsamset"))
        {
            audioCollect.Play();
            SaveItemPowerandName.instance.Addsamset();
            collision.gameObject.SetActive(false);
        }
        if (collision.CompareTag("Itemkhien"))
        {
            audioCollect.Play();
            SaveItemPowerandName.instance.Addkhien();
            collision.gameObject.SetActive(false);
        }

    }
    void PlayerAttack()
    {
        GameObject ccc = playerattack.GetEsp();
        ccc.gameObject.SetActive(true);
        ccc.transform.position = Poitattack.position;
        ccc.TryGetComponent(out Rigidbody2D rd);
        rd.velocity = Vector2.right * Speeddanphep;
    }
    [Space,Header("Skillbutton")]
    public List<Transform> TfSamset = new List<Transform>();
    [SerializeField] GameObject Samset;
    [SerializeField] GameObject Bom;
    [SerializeField] GameObject Bom2;
    [SerializeField] Transform poitSpawbom;
    [SerializeField] Transform poitSpawbom2;
    public void btnSamset()
    {
        if (SaveItemPowerandName.instance.GetSamset() > 0)
        {
            audioSkillSamSet.Play();
            if (gameObject.activeInHierarchy)
            {
            SaveItemPowerandName.instance.Lesssamset();
            Transform a = TfSamset[Random.Range(0, TfSamset.Count)];
            for (int i = 0; i < TfSamset.Count; i++)
            {
                GameObject Clone = Instantiate(Samset, TfSamset[i].position, Quaternion.identity);
                Clone.SetActive(true);
            }
            }

        }
    }
    public void btnKhi()
    {
        if(SaveItemPowerandName.instance.GetKhien() > 0)
        {
            audioSkillKhien.Play();
            circelcheck = true;
            CountStart = true;
            GameObject cop=Instantiate(Circel,transform.position,Quaternion.identity); 
            cop.SetActive(true);
            cop.transform.SetParent(this.transform);
            if (gameObject.activeInHierarchy)
            {
            SaveItemPowerandName.instance.Lesskhien();
            }
        }
    }
    public void btnBom()
    {
        if (SaveItemPowerandName.instance.Getbom() > 0)
        {
            audioSkillBom.Play();
            if (gameObject.activeInHierarchy)
            {
            SaveItemPowerandName.instance.Lessbom();
            GameObject clobom = Instantiate(Bom, poitSpawbom.position, Quaternion.identity);
            clobom.SetActive(true);
            GameObject clobom2 = Instantiate(Bom2, poitSpawbom2.position, Quaternion.identity);
            clobom2.SetActive(true);
            }
        }
    }
    bool cchp;
    public void btnCauvong()
    {
        audioSkillCauVong.Play();
        if (SaveItemPowerandName.instance.GetCauvong() > 0)
        {
            GameManager.Instance.PlayerDeath2 = false;
            Time.timeScale = 1;
            if (gameObject.activeInHierarchy)
            {
            SaveItemPowerandName.instance.Lesscauvong();
            }
            ConditionDeletchain = true;
            GameManager.Instance.Player[GameManager.Instance.indextplayer].SetActive(true);
            cchp = true;
        }
    }
}
