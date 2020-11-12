using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    #region static instance
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                if (instance == null)
                {
                    instance = new GameObject("Spawned Audiomanager", typeof(AudioManager)).GetComponent<AudioManager>();
                }
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    #endregion

    #region AudioSources
    private AudioSource musicSource;
    private AudioSource musicSource2;
    private AudioSource sfxSource;
 
    #endregion

    private void Awake()
    {

        DontDestroyOnLoad(this.gameObject);

        //créer les audiosources
        musicSource = this.gameObject.AddComponent<AudioSource>();
        musicSource2 = this.gameObject.AddComponent<AudioSource>();
        sfxSource = this.gameObject.AddComponent<AudioSource>();

        Debug.Log(musicSource);
        // loop les musiques
        musicSource.loop = true;
        musicSource2.loop = true;



    }

    //player one is 1 player 2 is -1
    public void PlayMusic(AudioClip musicClip)
    {
        Debug.Log(musicClip);
        Debug.Log(musicSource);
        musicSource.clip = musicClip;
        musicSource.Play();
        musicSource.panStereo = 1;
    }
    public void PlayMusic2(AudioClip musicClip)
    {
        musicSource2.clip = musicClip;
        musicSource2.Play();
        musicSource2.panStereo = -1;
    }

    public void PlaySFX(AudioClip clip, float volume)
    {
        sfxSource.PlayOneShot(clip,volume);

    }

}
