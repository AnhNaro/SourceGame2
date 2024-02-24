using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEvent : MonoBehaviour
{
    [SerializeField] AudioSource audioplayerfoost;
    [SerializeField] AudioSource audioclickExit;
    [SerializeField] AudioSource audioclickcollect;
    public void Soundfoostplay()
    {
        audioplayerfoost.Play();
    }
    public void SoundfoostStop()
    {
        audioplayerfoost.Stop();
    }
    public void audioExit()
    {
        audioclickExit.Play();
    }
    public void audioClick()
    {
        audioclickcollect.Play();
    }

}
