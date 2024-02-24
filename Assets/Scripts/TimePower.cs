using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class TimePower : MonoBehaviour
{
    [SerializeField] Button powerButton;
    [SerializeField] int powerTime;
    float TimeCount;
    [SerializeField] TextMeshProUGUI textTimeItem;
    int powerCount;
    private void Start()
    {
        powerCount = powerTime;
        TimeCount = powerTime;

    }
    private void Update()
    {
        textTimeItem.text = powerTime.ToString();
        if (powerButton.gameObject.activeInHierarchy == false)
        {
            TimeCount -= Time.deltaTime;
            powerTime = (int)TimeCount;
        }
        if (TimeCount <= 0)
        {
            powerButton.gameObject.SetActive(true);
            TimeCount = powerCount;

        }
    }
    public void eneKhien()
    {
        if (SaveItemPowerandName.instance.GetKhien() <= 0)
        {
            powerButton.gameObject.SetActive (true);
        }
    }
    public void eneBom()
    {
        if (SaveItemPowerandName.instance.Getbom() <= 0)
        {
            powerButton.gameObject.SetActive (true);
        }
    }
    public void eneSamset()
    {
        if (SaveItemPowerandName.instance.GetSamset() <= 0)
        {
            powerButton.gameObject.SetActive(true);
        }
    }
    public void eneCauvong()
    {
        if (SaveItemPowerandName.instance.GetCauvong() <= 0)
        {
            powerButton.gameObject.SetActive(true);
        }
    }
}
