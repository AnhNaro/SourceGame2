using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossCopy : MonoBehaviour
{
    public GameData gameData;
    Atribuildhandle hanboss;
    public SpriteRenderer spriteGoc;
    [SerializeField] GameObject Circelkhien;
    float timekhien;
    [SerializeField] Text txthherocopy;
    public GameObject fxtrieuhoi;
    private void Awake()
    {
        spriteGoc.color = Circelkhien.GetComponent<SpriteRenderer>().color;
    }
    private void Start()
    {
        GameManager.Instance.ConditionSpawbossCopy = true;
        hanboss = new Atribuildhandle(gameData);
        hanboss.Init();
        fxtrieuhoi.gameObject.SetActive(true);  
    }
    void Update()
    {
        txthherocopy.text=hanboss.Curenhp.ToString();   
        Circelkhien.TryGetComponent(out SpriteRenderer ss);
        timekhien += Time.deltaTime;
        if(timekhien > 0)
        {
            ss.color = spriteGoc.color;
            Circelkhien.SetActive(true);
        }
        if (timekhien > 5)
        {
            ss.color = Color.red;
        }
        if(timekhien > 6)
        {
            Circelkhien.SetActive(false);
        }
        if(timekhien > 12)
        {
          timekhien= 0;
        }
        if (hanboss.Curenhp < 0)
        {
            hanboss.Curenhp = 0;
            ss.color = spriteGoc.color;
            GameManager.Instance.ConditionSpawbossCopy = false;
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Danphep"))
        {
            if (!Circelkhien.activeInHierarchy)
            {
                collision.TryGetComponent(out DameWepon d);
                hanboss.TakenDame(d.GetDame());
                collision.gameObject.SetActive(false);
            }
            else if(Circelkhien.activeInHierarchy)
            {
                collision.gameObject.SetActive(false);
            }
        }
        if (collision.CompareTag("Samset"))
        {
            if (!Circelkhien.activeInHierarchy)
            {
                collision.TryGetComponent(out DameWepon ad);
                hanboss.TakenDame(ad.GetDame());
                collision.gameObject.SetActive(false);
            }
            else if (Circelkhien.activeInHierarchy)
            {
                collision.gameObject.SetActive(false);
            }
        }
        if (collision.CompareTag("Bom2"))
        {
            if (!Circelkhien.activeInHierarchy)
            {
                collision.TryGetComponent(out DameWepon sd);
                hanboss.TakenDame(sd.GetDame());
            }
        }
    }
    float timetrigger;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Bom2"))
        {
            if (timetrigger > 1 && !Circelkhien.activeInHierarchy)
            {   
                    collision.TryGetComponent(out DameWepon ed);
                    hanboss.TakenDame(ed.GetDame());
            }
        }
    }
}
