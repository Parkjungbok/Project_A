using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSound : MonoBehaviour
{
    [SerializeField] AudioClip BGM;    
    [SerializeField] AudioClip button;

    private void Start()
    {
        Manager.Sound.PlayBGM(BGM);
        
    }

    public void BtnAudio()
    {
        Manager.Sound.PlaySFX(button);
    }
}
