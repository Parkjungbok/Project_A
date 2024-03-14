using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titlesound : MonoBehaviour
{
    [SerializeField] AudioClip Titleaudio;
    [SerializeField] AudioClip button;

    private void Start()
    {
        Manager.Sound.PlayBGM(Titleaudio);
    }

    public void BtnAudio()
    {
        Manager.Sound.PlaySFX(button);
    }
}
