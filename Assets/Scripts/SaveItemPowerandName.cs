using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SaveItemPowerandName : MonoBehaviour
{
    public static SaveItemPowerandName instance;
    [SerializeField] Text txtBom;
    [SerializeField] Text txtSamset;
    [SerializeField] Text txtCauvong;
    [SerializeField] Text txtKhien;
    [Header("TextpowerShopBy")]
    [SerializeField] Text txtamountBom;
    [SerializeField] Text txtamountKhien;
    [SerializeField] Text txtamountCauvong;
    [SerializeField] Text txtamountSamset;
    [Header("ButtonShopby")]
    [SerializeField] Button btnbyBom;
    [SerializeField] Button btnbyCauvong;
    [SerializeField] Button btnbySamset;
    [SerializeField] Button btnbyKhien;
    [SerializeField] Text txtCointShop;
    [Header("UiKhongDuVang")]
    [SerializeField] GameObject ComunicateConditionCoint;
    [Header("SaveName_And_Code")]
    [SerializeField] TextMeshProUGUI txtNameTuongDa;
    [SerializeField] TMP_InputField Code;
    public Button Savecode;
    [SerializeField] TMP_InputField Mvp;
    [SerializeField] Button OKLoadSceneMVP;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        btnbyBom.onClick.AddListener(ByBom);
        btnbyCauvong.onClick.AddListener(ByCauvong);
        btnbyKhien.onClick.AddListener(ByKhien);
        btnbySamset.onClick.AddListener(BySamset);
        Savecode.onClick.AddListener(hck);
        OKLoadSceneMVP.onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            GameManager.Instance.PanelCongVangtext();
        });
    }
    private void Start()
    {
        Mvp.text = Name;
    }
    private void Update()
    {
        Name=Mvp.text;
        txtNameTuongDa.text = Name;
        txtBom.text=Bom.ToString();
        txtCauvong.text=Cauvong.ToString();
        txtKhien.text=Khien.ToString();
        txtSamset.text=Samset.ToString();
        //txtShop
        txtamountBom.text = Bom.ToString();
        txtamountCauvong.text = Cauvong.ToString();
        txtamountKhien.text = Khien.ToString();
        txtamountSamset.text= Samset.ToString();    
        txtCointShop.text = GameManager.Instance.Coint.ToString();
    }
    string Name
    {
        get => PlayerPrefs.GetString("name", "NGUYEN DINH ANH");
        set => PlayerPrefs.SetString("name", value);
    }
    int Bom
    {
        get => PlayerPrefs.GetInt("bom1",6);
        set => PlayerPrefs.SetInt("bom1", value);
    }
     int Samset
    {
        get => PlayerPrefs.GetInt("samset1",6);
        set => PlayerPrefs.SetInt("samset1", value);
    }
     int Khien
    {
        get => PlayerPrefs.GetInt("khien1", 6);
        set => PlayerPrefs.SetInt("khien1", value);
    }
     int Cauvong
    {
        get => PlayerPrefs.GetInt("cauvong1", 6);
        set => PlayerPrefs.SetInt("cauvong1", value);
    }
    //Get item value
    public int Getbom() => Bom;
    public int GetKhien() => Khien;
    public int GetSamset() => Samset;
    public int GetCauvong() => Cauvong;
    //Hck
    public void hck()
    {
        if (Code.text == "QT" && SaveCode == 0)
        {
        Khien+= 1000;
        Cauvong += 1000;
        Samset+= 1000;  
        Bom += 1000;
            SaveCode++;
        }
    }
    //Add value
    public void Addbom()
    {
        Bom++;
    }
    public void Addsamset()
    {
        Samset++;
    }
    public void Addkhien()
    {
        Khien++;
    }
    public void Addcauvong()
    {
        Cauvong++;  
    }
    // Less Power
    public void Lessbom()
    {
        Bom--;
    }
    public void Lesssamset()
    {
        Samset--;
    }
    public void Lesskhien()
    {
        Khien--;
    }
    public void Lesscauvong()
    {
        Cauvong--;
    }
    //By and Less
    void ByBom()
    {
        if (GameManager.Instance.Coint > 0)
        {
        Bom++;
        GameManager.Instance.Coint--;
        }
        else
        {
            ComunicateConditionCoint.gameObject.SetActive(true);
        }
    }
    void ByKhien()
    {
        if(GameManager.Instance.Coint > 0)
        {
        Khien++;
        GameManager.Instance.Coint--;
        }
        else
        {
            ComunicateConditionCoint.gameObject.SetActive(true);
        }
    }
    void BySamset()
    {
        if (GameManager.Instance.Coint > 0)
        {
        Samset++;
        GameManager.Instance.Coint--;
        }
        else
        {
            ComunicateConditionCoint.gameObject.SetActive(true);
        }
    }
    void ByCauvong()
    {
        if (GameManager.Instance.Coint >1)
        {
        Cauvong++;
        GameManager.Instance.Coint-=2;
        }
        else
        {
            ComunicateConditionCoint.gameObject.SetActive(true);
        }
    }
    int SaveCode
    {
        get => PlayerPrefs.GetInt("codeI", 0);
        set=>PlayerPrefs.SetInt("codeI", value);
    }
}
