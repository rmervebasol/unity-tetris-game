using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioClip[] musicClips;
    [SerializeField] private AudioSource musicSource;

    [SerializeField] private AudioSource[] sesEfektleri;
    [SerializeField] private AudioSource[] vocalClips;
    
    
    private AudioClip rastgeleMusicClip;

    public bool musicCalsınmı = true;
    public bool efektCalsinmi = true;
    
    public İconAçKapatManager musicIcon;
    public İconAçKapatManager FxIcon;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        rastgeleMusicClip = RastgeleClipSec(musicClips);
        BackgroundMusicCal(rastgeleMusicClip);
        
        musicIcon.IconAçKapatFNC(musicCalsınmı);
        FxIcon.IconAçKapatFNC(efektCalsinmi);
    }

    public void VocalSesÇıkar()
    {
        AudioSource source=vocalClips[Random.Range(0, vocalClips.Length)];
        source.Stop();
        source.Play();
    }

    public void SesEfektiCikar(int hangiSes)
    {
        if (efektCalsinmi&&hangiSes<sesEfektleri.Length)
        {
           sesEfektleri[hangiSes].Stop();
           sesEfektleri[hangiSes].Play();
           Debug.Log("SES ÇAĞRISI GELDİ! Index: " + hangiSes + " | Frame: " + Time.frameCount);
        }
    }

    AudioClip RastgeleClipSec(AudioClip[] clips)
    {
        AudioClip rastgeleClip=clips[Random.Range(0, clips.Length)];
        return rastgeleClip;
    }
    public void BackgroundMusicCal(AudioClip musicClip)
    {
        if (!musicClip||!musicSource || !musicCalsınmı)
        {
            return;
        }
        musicSource.clip = musicClip;
        musicSource.Play();
    }

    void MusicGüncelleFNC()
    {
        if (musicSource.isPlaying != musicCalsınmı)
        {
            if (musicCalsınmı)
            {
                rastgeleMusicClip = RastgeleClipSec(musicClips);
                BackgroundMusicCal(rastgeleMusicClip);
            }
            else
            {
                musicSource.Stop();
            }
        }
    }

    public void MusicAcKapaFNC()
    {
        musicCalsınmı = !musicCalsınmı;
        MusicGüncelleFNC();
        musicIcon.IconAçKapatFNC(musicCalsınmı);
    }

    public void FXAcKapatFNC()
    {
        efektCalsinmi = !efektCalsinmi;
        //efekti tru is false false ise true yapar
        FxIcon.IconAçKapatFNC(efektCalsinmi);
    }
}