using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
     public static GameManager Instance;
    public bool ConditionplayGame=false;
    public bool ConditionSpawChain=true;
    public bool Conditiontiemspawboss=true;
    public bool ConditionSpawbossCopy=false;
    [SerializeField] Button btnPauseGame;
    [SerializeField] Button btnResumGame;
    [Header("txtPanelPlayerDeath")]
    [SerializeField] Text CointisDeath;
    [SerializeField] Text textCointAddDeathpanel;
    [SerializeField] Text txtHightScore;
    public Text txtScore;
    [Header("textHome")]
    [SerializeField] Text txtCoitnHome;
    [SerializeField] Text txtHour;
    [SerializeField] Text txtDay_Year;
    [Header("Thuong")]
    [SerializeField] Button btnGetThuong;
    DateTime day = DateTime.Now;
    [SerializeField] Button PlayGame;
    [Header("Typeplay")]
    [SerializeField] Button btnTypeSolo;
    [SerializeField] Button btnTypesPassgame;
    public List<Button> buttons = new List<Button>();
    [SerializeField] Text CommunicateType;
    [Header("GamePlay")]
    [SerializeField] GameObject PanelPlayerDeath;
    [SerializeField] GameObject PanelPlayerDeathend;
    [SerializeField] Button CancelLoad;
    [SerializeField] Button okpanelDeath;
    [SerializeField] Button OnGame;
    [SerializeField] Button OkLoadScene;
    [SerializeField] Button buttonHome;
    [SerializeField] Text CointGameplay;
    [SerializeField] AudioSource audioCondition;
    public bool PlayerDeath=false;
    public bool PlayerDeath2=false;
    public int TinhDiemCongVang=0;
    [Header("Thu Vang Tu Enemy")]
    [SerializeField] Transform PoitStart;
    public LayerMask MaskCoint;
    [SerializeField] float Radius;
    [SerializeField] float MoveCointE;
    [Header("panelMVPGame")]
    public GameObject GemPaneMVP;
    public Text Diem;
    [SerializeField] Text DiemcaoNhat;
    [Space()]
    [Header("LoadPlayer")]
    public List<GameObject> Player = new List<GameObject>();
    [SerializeField] Button btnloadLeft;
    [SerializeField] Button btnloadright;
    [SerializeField] Text txtnameplayer;
   public int indextplayer
    {
        get => PlayerPrefs.GetInt("in", 0);
        set=>PlayerPrefs.SetInt("in", value);
    }
    [SerializeField] Transform PoitSpawPlayer;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
            DestroyImmediate(Instance);

        btnPauseGame.onClick.AddListener(PauseGame);
        btnResumGame.onClick.AddListener(ResumGame);
        PlayGame.onClick.AddListener(Play);
        btnTypeSolo.onClick.AddListener(diskynang);
        btnTypesPassgame.onClick.AddListener(enablekynang);
        buttonHome.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1;
            Coint++;
        });
        CancelLoad.onClick.AddListener(() =>
        {
            PanelPlayerDeathend.SetActive(true);
        });
        okpanelDeath.onClick.AddListener(() =>
        {
            PlayerDeath = false;
        });
        OkLoadScene.onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            PanelCongVangtext();
            PlayerDeath = false;
        });
        btnloadLeft.onClick.AddListener(() =>
        {
            Player[indextplayer].SetActive(false);
            indextplayer--;
            if (indextplayer < 0)
            {
                indextplayer = 3;
            }
            ResoucesPlayer();
        });
        
        btnloadright.onClick.AddListener(() =>
        {
            Player[indextplayer].SetActive(false);
            indextplayer++;
            if (indextplayer >= 4)
            {
                indextplayer = 0;
            }
            ResoucesPlayer();
        });
    }
    private void Start()
    {
        Player[indextplayer].transform.position = PoitSpawPlayer.position;
        Player[indextplayer].SetActive(true);
    }
    private void Update()
    {
        if (PlayerDeath && SaveItemPowerandName.instance.GetCauvong()>0)
        {
            PanelPlayerDeath.SetActive(true);
            Time.timeScale = 0;
        }
        else if(PlayerDeath && SaveItemPowerandName.instance.GetCauvong()<=0)
        {
            PanelPlayerDeathend.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        if (PlayerDeath2)//dis time scale
        {
            OnGame.interactable = false;
        }
        else
        {
            OnGame.interactable = true;
        }
        SaveGift = day.DayOfYear + 1;
        txtHour.text = DateTime.Now.ToString("HH:mm tt");
        txtDay_Year.text = DateTime.Now.ToString("d/MM/yyyy");
        txtHightScore.text = string.Format($"Điểm Cao Nhất: {Score}");
        txtCoitnHome.text = Coint.ToString();
        CointisDeath.text = string.Format($"Vang: {Coint}");
        CointGameplay.text = Coint.ToString();
        //panel MVP
        DiemcaoNhat.text= string.Format($"Điểm Cao Nhất: {Score}");
        if (interbutton == 0)
        {
            btnGetThuong.interactable = true;
        }
        else
        {
            btnGetThuong.interactable=false;
        }
        if (SaveCanhTypeGame == 0)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].interactable = true;
                CommunicateType.text = ("Bạn Đang Ở Chế Độ: Vượt Cảnh.");
            }
        }
        else
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].interactable = false;
                CommunicateType.text = ("Bạn Đang Ở Chế Độ: SoLo.");
            }
        }
        Collider2D[] coli = Physics2D.OverlapCircleAll(PoitStart.position, Radius, MaskCoint);
        if (coli != null)
        {
            for (int i = 0; i < coli.Length; i++)
            {
                Collider2D co = coli[i];
                co.transform.position = Vector3.MoveTowards(co.transform.position, PoitStart.position, MoveCointE * Time.deltaTime);
                if (Vector3.Distance(co.transform.position, PoitStart.position) < 0.2f)
                {
                    co.gameObject.SetActive(false);
                    Coint++;
                    //sssound
                }
            }
        }
        switch (TinhDiemCongVang)
        {
            case 0:
                {
                    textCointAddDeathpanel.text = "+ 1 Vàng";
                    break;
                }
            case 1:
                {
                    textCointAddDeathpanel.text = "+ 3 Vàng";
                    break;
                }
            case 2:
                {
                    textCointAddDeathpanel.text = "+ 5 Vàng";
                    break;
                }
            case 3:
                {
                    textCointAddDeathpanel.text = "+ 7 Vàng";
                    break;
                }
            case 4:
                {
                    textCointAddDeathpanel.text = "+ 9 Vàng";
                    break;
                }
            case 5:
                {
                    textCointAddDeathpanel.text = "+ 13 Vàng";
                    break;
                }
            case 6:
                {
                    textCointAddDeathpanel.text = "+ 200 Vàng";
                    break;
                }
        }
        switch (indextplayer)
        {
            case 0:
                {
                    txtnameplayer.text = "NINJA ẾCH";
                    txtnameplayer.color = Color.green;
                    break;
                }
            case 1:
                {
                    txtnameplayer.text = "NINJA HỒNG";
                    txtnameplayer.color = Color.magenta;
                    break;
                }
                case 2:
                {
                    txtnameplayer.text = "NINJA NGÂN HÀ";
                    txtnameplayer.color = Color.cyan;
                    break;
                }
                case 3:
                {
                    txtnameplayer.text = "NINJA TỘC LỬA";
                    txtnameplayer.color = Color.red;
                    break;
                }
        }

    }
    public void ResoucesPlayer()//PlayerPlayGame
    {
        Player[indextplayer].transform.position = PoitSpawPlayer.position;
        Player[indextplayer].SetActive(true);
    }
   public void PanelCongVangtext()
    { 
        switch (TinhDiemCongVang)
        {
            case 0:
                {
                    Coint++;
                    break;
                }
                case 1:
                {
                    Coint+=3;
                    break;
                }
                case 2:
                {
                    Coint += 5;
                    break;
                }
            case 3:
                {
                    Coint += 7;
                    break;
                }
                case 4:
                {
                    Coint += 9;
                    break;
                }
                case 5:
                {
                    Coint += 13;
                    break;
                }
                case 6:
                {
                    Coint += 200;
                    break;
                }
        }
    }
    void PauseGame()
    {
        Time.timeScale = 0;
        audioCondition.Pause();
    }
     void ResumGame()
    {
        Time.timeScale = 1;
        audioCondition.Play();
    }
    public int Score
    {
        get => PlayerPrefs.GetInt("Score", 0);
        set
        {
            if (value > PlayerPrefs.GetInt("Score", 0))
            {
                PlayerPrefs.SetInt("Score", value);
            }
        }
    }
    public int Coint
    {
        get => PlayerPrefs.GetInt("cointI", 10);
        set => PlayerPrefs.SetInt("cointI", value);
    }
     int SaveGift
    {
        get => PlayerPrefs.GetInt("Day",day.DayOfYear);
        set
        {
            if(value>PlayerPrefs.GetInt("Day",day.DayOfYear))
            {
                PlayerPrefs.SetInt("Day", value);
                interbutton = 0;
            }
        }
    }
    public void GetGift()
    {
        Coint += 5;
        interbutton++;
    }
    public void Play()
    {
        ConditionplayGame = true;
    }
    int interbutton
    {
        get => PlayerPrefs.GetInt("button", 0);
        set=>PlayerPrefs.SetInt("button", value);   
    }
    public void diskynang()
    {
        SaveCanhTypeGame = 1;
    }
    public void enablekynang()
    {
        SaveCanhTypeGame = 0;
    }
    int SaveCanhTypeGame 
    {
        get => PlayerPrefs.GetInt("canh", 0);
        set => PlayerPrefs.SetInt("canh", value);
    }
}
