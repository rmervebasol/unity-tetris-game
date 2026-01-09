using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeManager : MonoBehaviour
{
    [SerializeField] private bool donebilirmi;

    public Sprite shapeSekil;

    GameObject[] yerlesmeEfektleri;

    

    private void Start()
    {
        //efektlere tag ismi verdik bu isimşe efektleri kod kullanarak buluyoruz
        //efektlerimizi yerleştirdik.
        yerlesmeEfektleri=GameObject.FindGameObjectsWithTag("yerlesmeEfekt");
    }

    public void YerlesmeEfektiCikarFNC()
    {
        //efekti terisin olduğu kordinatlara yerleştirmemiz gerekiyor.
        int sayac = 0;
        foreach (Transform child in gameObject.transform)
        {
            if (yerlesmeEfektleri[sayac])
            {
                yerlesmeEfektleri[sayac].transform.position=new Vector3(child.position.x,child.position.y,0f);
                
                //her efektin içindeki partical manageri buluyoruz.
                ParticalManager particalManager=yerlesmeEfektleri[sayac].GetComponent<ParticalManager>();
                //eğer varsa efekti çalıştıracağız.
                if (particalManager)
                {
                    particalManager.EfectPlayFNC();
                }
            }

            sayac++;
        }
    }


    public void SolaHareketFNC()
    {
        transform.Translate(Vector3.left,Space.World);//sola hareket edecek
        
    }
    public void SağaHareketFNC()
    {
        transform.Translate(Vector3.right,Space.World);//sağa hareket edecek
        
    }

    public void AsagiHareketFNC()
    {
        transform.Translate(Vector3.down,Space.World);
       
    }

    public void YukarıHareketFNC()
    {
        transform.Translate(Vector3.up,Space.World);
    }

    public void SağaDönFNC()
    {
        if (donebilirmi)
        {
            transform.Rotate(0,0,-90);
        }
    }

    public void SolaDönFNC()
    {
       transform.Rotate(0,0,90); 
    }

    IEnumerator HareketRoutıne()
    {
        while (true)
        {
            AsagiHareketFNC();
            yield return new WaitForSeconds(.25f);//corotuinein 0.25 sn beklemesini söyler
            
        }
    }

    public void SaatYonunedönsünFNC(bool saatYonumu)
    {
        if (saatYonumu)
        {
            SağaDönFNC();
        }
        else
        {
            SolaDönFNC();
        }
    }
}
