using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : MonoBehaviour
{
    public static Speed intance;
    public int Sppee;
    float countadd;
    private void Awake()
    {
        if (intance == null)
        {
            intance = this;
        }
        else
        {
         DestroyImmediate(this);
        }
    }
    private void Update()
    {
        if (!GameManager.Instance.ConditionplayGame) return;
        countadd += Time.deltaTime;
        if (countadd > 20)
        {
            Sppee+=2;
            countadd = 0;
        }
        if (Sppee >= 90)
        {
            Sppee = 90;
        }

    }
}