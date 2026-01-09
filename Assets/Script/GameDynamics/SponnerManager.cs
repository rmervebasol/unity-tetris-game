using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SponnerManager : MonoBehaviour
{
   // bu dizinin içine tüm elemanları atıcaz bu diziden de rastgele bir eleman 
   //seçeceğiz
     [SerializeField]ShapeManager[] tumSekiller;

     [SerializeField] private Image[] sekilImages = new Image[2];

     private ShapeManager[] siradakiSekiller=new ShapeManager[2];


     

     public ShapeManager RandomSekilOluşturFNC()
   {
      ShapeManager şekil = null;

      şekil = SiradakkiSekliAlFNC();
      şekil.gameObject.SetActive(true);
      şekil.transform.position=transform.position;
      

      if (şekil != null)
      {
         return şekil;
      }
      else
      {
         print("dizi boş!");
         return null;
      }
   }

  

   public void HepsiniNulYapFNC()
   {
      for (int i = 0; i < siradakiSekiller.Length; i++)
      {
         siradakiSekiller[i]=null;
      }
      SirayıDoldurFNC();
   }

   void SirayıDoldurFNC()
   {
      for (int i = 0; i < siradakiSekiller.Length; i++)
      {
         if (!siradakiSekiller[i])//sekiller nullden farklıysa
         {
            siradakiSekiller[i] = Instantiate(RastgeleSekilOlusturFNC(),transform.position,Quaternion.identity) as ShapeManager;
            siradakiSekiller[i].gameObject.SetActive(false);
            sekilImages[i].sprite = siradakiSekiller[i].shapeSekil;


         }
      }

      StartCoroutine(SekilImageAcRoutine());

   }

   IEnumerator SekilImageAcRoutine()
   {
      for (int i = 0; i < sekilImages.Length; i++)
      {
         sekilImages[i].GetComponent<CanvasGroup>().alpha = 0f;
         sekilImages[i].GetComponent<RectTransform>().localScale=Vector3.zero;
         
      }
      
      yield return new WaitForSeconds(.1f);
      //sonraki kısmındaki shapelerin açılma animasyonu
      //sırayla açılan animasyonlarda bu tekniği izleyebilirsiniz
      //DOTween kütüphanesini kullandık biz burada
      int sayaç = 0;
      while (sayaç<sekilImages.Length)
      {
         //fade kısmında görünürlüğü ayarladık
         sekilImages[sayaç].GetComponent<CanvasGroup>().DOFade(1, .6F);
         //scale kısmında boyutu ayarladık
         sekilImages[sayaç].GetComponent<RectTransform>().DOScale(1, .6f).SetEase(Ease.OutBack);
         sayaç++;

         yield return new WaitForSeconds(.4f);
      }
      
      
      
      
      
      
      
      
   }
   
   
   
   
   
   ShapeManager RastgeleSekilOlusturFNC()
   {
      int randomSekil = Random.Range(0, tumSekiller.Length);

      if (tumSekiller[randomSekil])
      {
         return tumSekiller[randomSekil];
      }
      else
      {
         return null;
      }
   }

   ShapeManager SiradakkiSekliAlFNC()
   {
      ShapeManager sonrakiSekil = null;

      if (siradakiSekiller[0])
      {
         sonrakiSekil = siradakiSekiller[0];
      }

      for (int i = 1; i < siradakiSekiller.Length; i++)
      {
         siradakiSekiller[i-1]=siradakiSekiller[i];
         sekilImages[i - 1].sprite = siradakiSekiller[i - 1].shapeSekil;
      }
      siradakiSekiller[siradakiSekiller.Length-1] = null;
      SirayıDoldurFNC();
      return sonrakiSekil;
   }

   public ShapeManager EldekiShapeOlusturFNC()
   {
      ShapeManager eldekiSekil = null;
      eldekiSekil=Instantiate(RastgeleSekilOlusturFNC(),transform.position,Quaternion.identity) as ShapeManager;
      eldekiSekil.transform.position=transform.position;
      return eldekiSekil;
      
   }
   
   
}
