using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Khienchan : MonoBehaviour
{

    float time;
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 7)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        if(time >= 9)
        {
            gameObject.SetActive(false);
            time = 0;
        }
    }
}
