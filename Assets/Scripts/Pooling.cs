using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour
{
    [SerializeField] GameObject Enemy;
    [SerializeField] int Size;
    List<GameObject> Esp = new List<GameObject>();
    private void Awake()
    {
        for (int i = 0; i < Size; i++)
        {
            GameObject clone = Instantiate(Enemy, this.transform);
            clone.gameObject.SetActive(false);
            Esp.Add(clone);
        }
    }

    public GameObject GetEsp()
    {
        foreach (GameObject go in Esp)
        {
            if (!go.activeInHierarchy)
            {
                return go;
            }
        }
        GameObject cc = Instantiate(Enemy, this.transform);
        cc.SetActive(false);
        Esp.Add(cc);
        return cc;
    }
    public void DisableGamobject()
    {
        gameObject.SetActive(false);    
    }
}
