using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawenemy : MonoBehaviour
{
    public List<Transform> Spawntran=new List<Transform>();
    public List<Pooling> Enemyspawpoo = new List<Pooling>();
    public List<GameObject> Itemspaw=new List<GameObject>();
    [Space,Header("Tree")]
    [SerializeField] Transform Poittree;
    [SerializeField] Transform Poitflower;
    public List<Pooling> tree=new List<Pooling>();
    public List<Pooling> flower=new List<Pooling>();
    float i;
    float j=1.5f;
    float timeitem = 0;
    float timepawitem;
    float timetree;
    float timeflower;
    [Space,Header("WarningGame")]
    float TimeWarning;
    float TimeStone;
    [SerializeField] GameObject WarningUi;
    public Pooling PooWarningStone;
    Vector2 Goc;
    private void Awake()
    {
        Goc=transform.position; 
    }
    void Update()
    {
        if (indexBoss >= 5&&GameManager.Instance.Conditiontiemspawboss) //Panel Mvp
        {
            GameManager.Instance.GemPaneMVP.gameObject.SetActive(true);
            GameManager.Instance.ConditionSpawChain = false;
            GameManager.Instance.ConditionplayGame = false;
        }

        if (!GameManager.Instance.ConditionplayGame) return;
        //chain
        timepawitem += Time.deltaTime;
        i += Time.deltaTime;
        if (i > j && timepawitem<60 && GameManager.Instance.ConditionSpawChain)
        {
            Spawenemy1();
            i = 0;
        }
        // item
        if (timepawitem >=60)
        {
            timeitem += Time.deltaTime;
            if (timeitem >= j)
            {
                Spawitems();
                timeitem = -1;
            }
        }
        if (timepawitem >=63)
        {
            timepawitem = 0;
            timeitem = 0;
        }
        if (j <= 0.8f)
        {
            j = 0.8f;
        }
        //boss
        if(GameManager.Instance.Conditiontiemspawboss) SpawBoss += Time.deltaTime;
        if (SpawBoss >= 90)
        {
            GameManager.Instance.Conditiontiemspawboss = false;
            indexBoss++;
            if (indexBoss >= 6) return;
            j -= 0.1f;
            txtNameBoss.gameObject.SetActive(true);  
            GameManager.Instance.ConditionSpawChain = false;
            if (indexBoss == 4)
            {
                transform.position=new Vector2(transform.position.x+3,transform.position.y);
            }
            else
            {
                transform.position = Goc;
            }
            SpawEnemyboss();
            SpawBoss = 0;
        }
        //Spaw tree
        timetree += Time.deltaTime;
        if (timetree > 2f)
        {
            Spawtree();
            timetree = 0;
        }
        timeflower += Time.deltaTime;
        if (timeflower > 1.6f)
        {
            Spawflower();
            timeflower = 0; 
        }
        //Warning
        if (GameManager.Instance.Conditiontiemspawboss)
        {
            TimeWarning += Time.deltaTime;
        }
        if (TimeWarning > 26)
        {
            if (!GameManager.Instance.Conditiontiemspawboss) return;
            dd = Spawntran[Random.Range(0, Spawntran.Count)];
            CCC= Instantiate(WarningUi,new Vector2(dd.position.x-20,dd.position.y), Quaternion.identity);
            CCC.SetActive(true);
            CheckWarUi = true;
            TimeWarning = 0;
        }
        if(CheckWarUi) TimeStone += Time.deltaTime;

        if (TimeStone >= 3)
        {
            if (!GameManager.Instance.Conditiontiemspawboss) return;
            CCC.gameObject.SetActive(false);
            GameObject kk = PooWarningStone.GetEsp();
            kk.transform.position = dd.position;
            kk.SetActive(true);
            kk.TryGetComponent(out Rigidbody2D rd);
            rd.velocity = Vector2.left *160;
            TimeStone = 0;
            TimeWarning = 0;
            CheckWarUi = false;
        }
        //Spaw tên enemy boss

        switch (indexBoss)
        {
            case 0:
                {
                    txtNameBoss.text = "Ong Nổi Loạn";
                    break;
                }
            case 1:
                {
                    txtNameBoss.text = "Slim Nổi Giận";
                    break;
                }
            case 2:
                {
                    txtNameBoss.text = "Củ Cải Đột Biến";
                    break;  
                }
                case 3:
                {
                    txtNameBoss.text = "Bóng Ma Địa Ngục";
                    break;
                }
                case 4:
                {
                    txtNameBoss.text = "Anh Hùng Ngân Hà Đen";
                    break;
                }
            case 5:
                {
                    txtNameBoss.text = "Boss Cuối";
                    break;
                }
        }
    }
    Transform dd;
    GameObject CCC;
    bool CheckWarUi=false;
    [SerializeField] Text txtNameBoss;
  void Spawenemy1()
    {
        Transform spaw= Spawntran[Random.Range(0,Spawntran.Count)]; 
        Pooling SS = Enemyspawpoo[Random.Range(0, Enemyspawpoo.Count)];
        GameObject clone= SS.GetEsp();
        clone.SetActive(true);
        clone.transform.position=spaw.position;
    }
    void Spawitems()
    {
        Transform spaw = Spawntran[Random.Range(0, Spawntran.Count)];
        int a=Random.Range(0,Itemspaw.Count);
        GameObject clon = Instantiate(Itemspaw[a], spaw.position, Quaternion.identity);
         clon.gameObject.SetActive(true);   
    }
    [Space, Header("BossGame")]
    public List<GameObject> Boss = new List<GameObject>();
    float SpawBoss;
     int indexBoss=-1;
    void SpawEnemyboss()
    {
        GameObject Cc = Instantiate(Boss[indexBoss], transform.position, Quaternion.identity);
        Cc.SetActive(true);
    }
    void Spawtree()
    {
        Pooling aa = tree[Random.Range(0, tree.Count)];
        GameObject clone = aa.GetEsp();
        clone.SetActive(true);
        clone.transform.position = Poittree.position;
    }
    void Spawflower()
    {
        Pooling aa = flower[Random.Range(0, flower.Count)];
        GameObject clone = aa.GetEsp();
        clone.SetActive(true);
        clone.transform.position = Poitflower.position;
    }
}
