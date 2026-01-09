using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class İconAçKapatManager : MonoBehaviour
{

    public Sprite acikIcon;
    public Sprite kapaliIcon;

    private Image iconImg;

    public bool varsayilanIconDurumu=true;

    private void Start()
    {
        iconImg=GetComponent<Image>();//resim yükleyeceğimiz alanı seçtik

        iconImg.sprite = (varsayilanIconDurumu) ? acikIcon : kapaliIcon;
        

       
    }

    public void IconAçKapatFNC(bool iconDurumu)
    {
        if (!iconImg || !acikIcon || !kapaliIcon)
        {
            return;
        }
        else
        {
            iconImg.sprite = (iconDurumu) ? acikIcon : kapaliIcon;
        }
    }
}
