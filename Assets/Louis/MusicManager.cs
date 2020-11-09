using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip Music1;
    public AudioClip Music2;
    public AudioManager audioManager;
    void Start()
    {
        audioManager = AudioManager.Instance;
        audioManager.PlayMusic(Music1);
        audioManager.PlayMusic2(Music2);

    }


}
