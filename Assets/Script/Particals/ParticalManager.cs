using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalManager : MonoBehaviour
{
    public ParticleSystem[] tumEfektler;

    private void Start()
    {
        //tumEfektler değişkenine Particle systemden çektiğimiz efektleri ekliyoruz.
        tumEfektler = GetComponentsInChildren<ParticleSystem>();
    }


    public void EfectPlayFNC()
    {
        foreach (ParticleSystem efect in tumEfektler)
        {
            efect.Stop();
            efect.Play();
        }
    }
}
