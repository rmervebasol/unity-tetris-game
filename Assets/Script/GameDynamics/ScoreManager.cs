using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int score;
    private int satirlar;
    public int level=1;

    public int seviyedekiSatirSayisi = 5;//yıkması gereken satır sayısı 

    private int minSatir = 1;
    private int maxSatir = 4;
    
    public TextMeshProUGUI satirTxt;
    public TextMeshProUGUI levelTxt;
    public TextMeshProUGUI scoreTxt;

    public bool levelGeçildimi = false;

    private void Start()
    {
        ResetFNC();
    }
    

    public void ResetFNC()
    {
        level = 1;
        satirlar = seviyedekiSatirSayisi * level;
        TextGuncelleFNC();
    }

    public void SatirSkoruFNC(int n)
    {
        levelGeçildimi = false;
        n=Mathf.Clamp(n,minSatir,maxSatir);//oyuncu 10 satır yıksa bile 1 ile 4 arasındakilere değer verilecek sadece

        switch (n)
        {
            case 1:
                score += 30 * level;
                break;
            case 2:
                score += 50 * level;
                break;
            case 3:
                score += 150 * level;
                break;
            case 4:
                score += 500 * level;
                break;
        }

        satirlar -= n;
        if (satirlar <= 0)
        {
            LevelAtlaFNC();
        }
        
        TextGuncelleFNC();
    }

    void TextGuncelleFNC()
    {
        if (scoreTxt)
        {
            scoreTxt.text = BaşaSıfırEkleFNC(score, 5);
        }

        if (levelTxt)
        {
            levelTxt.text = level.ToString();
        }

        if (satirTxt)
        {
            satirTxt.text = satirlar.ToString();
        }
    }

    string BaşaSıfırEkleFNC(int skor, int rakamSayisi)
    {
        string skorStr = skor.ToString();

        while (skorStr.Length < rakamSayisi)
        {
            skorStr = "0" + skorStr;
        }

        return skorStr;
    }
    
    public void LevelAtlaFNC()
    {
        level++;
        satirlar = seviyedekiSatirSayisi * level;
        levelGeçildimi = true;
    }

}
