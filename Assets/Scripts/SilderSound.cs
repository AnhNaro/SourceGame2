using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SilderSound : MonoBehaviour
{
    public Slider slider;
    public AudioSource audio;
    [SerializeField] Button SoundOn;
    [SerializeField] Button SoundOff;
    private void Awake()
    {
        SoundOn.onClick.AddListener(() =>
        {
            audio.Pause();
            Savebool = 0;
            SoundOn.gameObject.SetActive(false);
            SoundOff.gameObject.SetActive(true);
        });
        
        SoundOff.onClick.AddListener(() =>
        {
            audio.Play();
            Savebool= 1;
            SoundOn.gameObject.SetActive(true);
            SoundOff.gameObject.SetActive(false);
        });

    }
    void Start()
    {
        audio.volume = Save;
        slider.value = audio.volume;
        slider.onValueChanged.AddListener((i) =>
        {
            audio.volume = i;
            Save = i;
        });
        if (Savebool == 0)
        {
            audio.Stop();
            SoundOn.gameObject.SetActive(false);
            SoundOff.gameObject.SetActive(true);
        }
        else
        {
            audio.Play();
            SoundOn.gameObject.SetActive(true);
            SoundOff.gameObject.SetActive(false);
        }
    }
    public float Save
    {
        get => PlayerPrefs.GetFloat("Sound",0.46f);
        set => PlayerPrefs.SetFloat("Sound", value);
    }
    int Savebool
    {
        get => PlayerPrefs.GetInt("bool", 1);
        set => PlayerPrefs.SetInt("bool", value);
    }
}
