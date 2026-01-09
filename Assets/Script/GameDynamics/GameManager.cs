using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
     SponnerManager sponner;
     BoardManager board;
     
     private  ShapeManager aktifSekil;
     [Header("Sayaçlar")]//bu kısma başlık verdik sadece unitydede görünüyor.
     [Range(0.01f,1f)] //unitydeki spon süresine scroll ikonu ekledik
     [SerializeField] private float asağiInmeSüresi = .5f; //.5f sanide bir hareket ediyor.
     private float asağiInmeSayaç;
     private float asagiInmeLevelSayac;
     [Range(0.02f,1f)] 
     [SerializeField]private float sagSolTuşaBasmaSüresi = 0.25f;
     private float sagSolTusaBasmaSayaç;
     [Range(0.02f,1f)] 
     [SerializeField]private float sagSolDönmeSüresi = 0.25f;
     private float sagSolDönmeSayaç;
     [Range(0.02f,1f)] 
     [SerializeField]private float asağiTuşaBasmaSüresi = 0.25f;
     private float asağiTuşaBasmaSayaç;

     public bool gameOver = false;

     public bool saatYonumu = true;
     public İconAçKapatManager rotateIcon;
     
     
     public GameObject gameOverPanel;
     
     private ScoreManager scoreManager;
     
     private TakipShapeManager takipShape;
     
     private ShapeManager eldekiSekil;//hold kısmında tuttuğumuz şekil
     //

     public Image eldekiSekilImg;//holder paneldeki img kısmını temsil eder

     private bool eldekiDeğiştirilebilirmi = true;

     public ParticalManager[] seviyeatlamaEfektleri = new ParticalManager[5];
     public ParticalManager[] gameOverEfektleri = new ParticalManager[5];

     private void Awake()
     {
         //sponner=GameObject.FindGameObjectWithTag("Sponner").GetComponent<SponnerManager>();

         board = GameObject.FindObjectOfType<BoardManager>(); //2
         sponner=GameObject.FindObjectOfType<SponnerManager>();
         scoreManager = GameObject.FindObjectOfType<ScoreManager>();
          
         //“Sahnede TakipShapeManager script’i hangi objede varsa,
         //onu bul ve takipShape değişkenine ata.”
         takipShape=GameObject.FindObjectOfType<TakipShapeManager>();
     }

     private void Start()
     {
         OyunaBaslaFNC();
     }

     public void OyunaBaslaFNC()    //nesnelere ulaşma yöntemleri
     {
          
          sponner.HepsiniNulYapFNC();
         
          

          if (sponner)
          {
              if (aktifSekil == null)
              {
                  aktifSekil = sponner.RandomSekilOluşturFNC();
                  aktifSekil.transform.position = VectoruIntYapFNC(aktifSekil.transform.position);
              }

              if (eldekiSekil==null)
              {
                  eldekiSekil = sponner.EldekiShapeOlusturFNC();

                  if (eldekiSekil.name==aktifSekil.name)
                  {
                      Destroy(eldekiSekil.gameObject);
                      eldekiSekil = sponner.EldekiShapeOlusturFNC();
                      
                      eldekiSekilImg.sprite = eldekiSekil.shapeSekil;
                      eldekiSekil.gameObject.SetActive(false);

                  }
                  else
                  {
                      eldekiSekilImg.sprite = eldekiSekil.shapeSekil;
                      eldekiSekil.gameObject.SetActive(false);
                  }
                  
                 
              }
              
          }

          if (gameOverPanel)
          {
              gameOverPanel.SetActive(false);
          }

          asagiInmeLevelSayac = asağiInmeSüresi;
     }

     private void Update()
     {
         //sürekli tetikleme yapıcaz sponnera
         //öncelikle board nullmu yani boşmu ony kontrol edicez
         if (!board || !sponner||!aktifSekil|| gameOver||!scoreManager)
         {
             return;
         }

         GirisKontrolFNC();
     }

     private void LateUpdate()
     {
         //sürekli tetikleme yapıcaz sponnera
         //öncelikle board nullmu yani boşmu ony kontrol edicez
         if (!board || !sponner||!aktifSekil|| gameOver||!scoreManager||!takipShape)
         {
             return;
         }
         if (takipShape)
         {
             takipShape.TakipShapeOluşturFNC(aktifSekil,board);
         }
     }

     private void GirisKontrolFNC()
     {
         //basılı turma ve basma işlemi
         if (Input.GetKey("right")&&Time.time>sagSolTusaBasmaSayaç||Input.GetKeyDown("right"))
         {
             aktifSekil.SağaHareketFNC();
             sagSolTusaBasmaSayaç = Time.time + sagSolTuşaBasmaSüresi;
             if (!board.GeçerliPozisyondamı(aktifSekil))
             {
                 SoundManager.instance.SesEfektiCikar(3);//hata sesi veriyoruz
                 aktifSekil.SolaHareketFNC();
             }
             else
             {
                 SoundManager.instance.SesEfektiCikar(1);//hareket etme sesi
             }
             
         }
         else if (Input.GetKey("left")&&Time.time>sagSolTusaBasmaSayaç||Input.GetKeyDown("left"))
         {
             aktifSekil.SolaHareketFNC();
             sagSolTusaBasmaSayaç = Time.time + sagSolTuşaBasmaSüresi;
             if (!board.GeçerliPozisyondamı(aktifSekil))
             {
                 SoundManager.instance.SesEfektiCikar(3);
                 aktifSekil.SağaHareketFNC();
             }
             else
             {
                 SoundManager.instance.SesEfektiCikar(1);
             }
         }
         //uo kısmı shapei döndürmek için
         else if (Input.GetKeyDown("up")&&Time.time>sagSolDönmeSayaç)
         {
             aktifSekil.SağaDönFNC();
             sagSolDönmeSayaç = Time.time + sagSolDönmeSüresi;
             if (!board.GeçerliPozisyondamı(aktifSekil))
             {
                 SoundManager.instance.SesEfektiCikar(3);
                 aktifSekil.SolaDönFNC();
             }
             else
             {
                 saatYonumu = !saatYonumu;
                 if (rotateIcon)
                 {
                     rotateIcon.IconAçKapatFNC(saatYonumu);
                 }
                 
                 SoundManager.instance.SesEfektiCikar(1);
             }
         }
         //down kısmı shapi aşşağı doğru giderken hızlandırma kısmı
         else if (((Input.GetKey("down")&&Time.time>asağiTuşaBasmaSayaç))||Time.time>asağiInmeSayaç)
         {
             
                 asağiInmeSayaç = Time.time + asagiInmeLevelSayac;
                 asağiTuşaBasmaSayaç = Time.time + asağiTuşaBasmaSüresi;
             
                 if (aktifSekil) //aktif sekilde nulldan farklıysa aktif şeklin aşağı hareketini başlatıyoruz
                 {
                     aktifSekil.AsagiHareketFNC();


                     if (!board.GeçerliPozisyondamı(aktifSekil))
                     {
                         if (board.dısariTaştımıFNC(aktifSekil))
                         {
                             aktifSekil.YukarıHareketFNC();
                             gameOver = true;
                             SoundManager.instance.SesEfektiCikar(2);

                             if (gameOverPanel)
                             {
                                 StartCoroutine(GameOverRoutineFNC());
                             }
                             
                             //SoundManager.instance.SesEfektiCikar(2);
                         }
                         else
                         {
                             YerlestiFNC();
                         }
                     }
                 }
             
         }


         
     }

     private void YerlestiFNC()
     {
         if (aktifSekil)
         {
             sagSolTusaBasmaSayaç=Time.time;
             asağiTuşaBasmaSayaç = Time.time;
             sagSolDönmeSayaç=Time.time;
         
         
             aktifSekil.YukarıHareketFNC();
         
             aktifSekil.YerlesmeEfektiCikarFNC();
         
             board.ŞekliIzgaraİçineAlFNC(aktifSekil);
             // SoundManager.instance.SesEfektiCikar(0);

             eldekiDeğiştirilebilirmi = true;


             if (sponner)
             {
                 aktifSekil = sponner.RandomSekilOluşturFNC();
             
             
                 eldekiSekil = sponner.EldekiShapeOlusturFNC();

                 if (eldekiSekil.name==aktifSekil.name)
                 {
                     Destroy(eldekiSekil.gameObject);
                     eldekiSekil = sponner.EldekiShapeOlusturFNC();
                      
                     eldekiSekilImg.sprite = eldekiSekil.shapeSekil;
                     eldekiSekil.gameObject.SetActive(false);

                 }
                 else
                 {
                     eldekiSekilImg.sprite = eldekiSekil.shapeSekil;
                     eldekiSekil.gameObject.SetActive(false);
                 }
             
             }

             if (takipShape)//takip shape varsa kaldırıyoruz blok yerleştikten sonra
             {
                 takipShape.ResetFNC();
             }
         
         
         
             StartCoroutine(board.TumSatirlarıTemizleFNC());
             

             if (board.tamamlananaSatir > 0)
             {
                 scoreManager.SatirSkoruFNC(board.tamamlananaSatir);

                 if (scoreManager.levelGeçildimi)
                 {
                     asagiInmeLevelSayac = Mathf.Clamp(asağiInmeSüresi - (scoreManager.level - 1) * 0.1f, 0.05f, asağiInmeSüresi);
                    

                     StartCoroutine(SeviyeGecRoutineFNC());
                 }
             
             
                 if (board.tamamlananaSatir > 1)
                 {
                     SoundManager.instance.VocalSesÇıkar();
                 }
                 SoundManager.instance.SesEfektiCikar(0);
             }
         }
        
     }

     Vector2 VectoruIntYapFNC(Vector2 vector)
     {
         //tam sayıya çeviriyoruz vector kordinatlarını
         return new Vector2(Mathf.Round(vector.x),Mathf.Round(vector.y));
         
     }

     public void RotationIconYönüFNC()
     {
         saatYonumu = !saatYonumu;
         aktifSekil.SaatYonunedönsünFNC(saatYonumu);
         if (!board.GeçerliPozisyondamı(aktifSekil))
         {
             aktifSekil.SaatYonunedönsünFNC(!saatYonumu);
             //SoundManager.instance.SesEfektiCikar(2);
         }
         else   //sınırlar içindeysek
         {
             if (rotateIcon)
             {
                 rotateIcon.IconAçKapatFNC(saatYonumu);
             }
         }
         
     }

     public void EldekiSekliDeğiştirFNC()
     {
         if (eldekiDeğiştirilebilirmi)
         {
             eldekiDeğiştirilebilirmi = false;
             
             aktifSekil.gameObject.SetActive(false);//aktif sekli kapatıyoruz çünkü butona basınca değişecekya o yuzden
             eldekiSekil.gameObject.SetActive(true);//eldeki sekli aktif hale getiriyoruz
             
             eldekiSekil.transform.position=aktifSekil.transform.position;
             aktifSekil = eldekiSekil;
         }

         if (takipShape)
         {
             takipShape.ResetFNC();
         }
     }

     IEnumerator SeviyeGecRoutineFNC()
     {
         yield return new WaitForSeconds(.2f);
         int sayac = 0;

         while (sayac < seviyeatlamaEfektleri.Length)
         {
             seviyeatlamaEfektleri[sayac].EfectPlayFNC();
             yield return new WaitForSeconds(.1f);
             sayac++;
         }
     }

     IEnumerator GameOverRoutineFNC()
     {
         //patlama efektlerinin gerçekleştikten
         //sonra bizim gameover ekranının çıkmasını istiyoruz
         yield return new WaitForSeconds(.2f);
         int sayac = 0;

         while (sayac < gameOverEfektleri.Length)
         {
             gameOverEfektleri[sayac].EfectPlayFNC();
             yield return new WaitForSeconds(.1f);
             sayac++;
         }
         yield return new WaitForSeconds(1f);
         
         
         
         if (gameOverPanel)
         {
             gameOverPanel.transform.localScale=Vector3.zero;
             gameOverPanel.SetActive(true);

             gameOverPanel.transform.DOScale(1, .5f).SetEase(Ease.OutBack);
         }
         
     }
     

}
