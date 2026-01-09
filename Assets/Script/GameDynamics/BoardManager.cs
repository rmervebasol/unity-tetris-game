using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
   [SerializeField] public Transform tilePrefab;
   public int yukseklik = 22;
   public int genislik=10;
   
   private Transform[,] ızgara;

   public int tamamlananaSatir = 0;

//aynı anda 4 tane purticalmanager nesnesi tutabilen bir dizi oluşturuyoruz
//4 tane efekt yöneticisini bir dizide saklıyoruz yani.
   public ParticalManager[] satirEfektleri=new ParticalManager[4];

   private void Awake()
   {
      ızgara = new Transform[genislik, yukseklik];
   }
   
   
   
   
   private void Start()
   {
      BosKareleriOlusturFNC();
   }
   
   //herhangi bir karenin board içinde olup olmadığını kontrol etmem gerekiyor
   //çünkü sponner en alta gelince durmasını istiyorum bunun belli koşulları var
   //tetrisin içindeki tüm karelerin koordinatlarını göndericez eğer kordinatlarımız sınıelar içindeyse akmaya devam edecek değilse duracak.
   bool BoardİÇindemi(int x, int y)
   {
      return (x >= 0 && x < genislik && y >= 0);
   }
   
   

   public bool KareDolumu(int x, int y,ShapeManager shape)
   {
      return (ızgara[x, y] != null && ızgara[x, y].parent != shape.transform);
   }
   
   
   
   
   
   //bütün kareleri kontrol edecek fonksiyon
   public bool GeçerliPozisyondamı(ShapeManager shape)
   {
      foreach (Transform child in shape.transform)//göndermiş olduğumuz shapin bütün çocuklarını dolaş
      //gelecek şeklin karelerinin float bir noktada olmaması gerekiyor
      {
         //kordinatlarını ınte çeviriyoruz daha kolay kontrol edelim diye
         Vector2 pos = VectoruIntYapFNC(child.position);
         if (!BoardİÇindemi((int)pos.x, (int)pos.y))
         {
            return false;
         }

         if (pos.y < yukseklik)
         {
            if (KareDolumu((int)pos.x,(int)pos.y,shape))
            {
               return false;
            }
         }

         

      }

      return true;
   }

   void BosKareleriOlusturFNC()
   {
      if (tilePrefab != null)
      {
         for (int y = 0; y < yukseklik; y++)
         {
            for (int x = 0; x< genislik; x++)
            {
               Transform tile=Instantiate(original:tilePrefab,new Vector3(x,y,0),Quaternion.identity);
               tile.name="x "+x.ToString()+" ,"+" y "+y.ToString();
               tile.parent = this.transform;
            }
         }
      }
      else
      {
         print("tile prefab eklenmedi!!");
      }
     
   }

   public void ŞekliIzgaraİçineAlFNC(ShapeManager shape)
   {
      if (shape == null) //shape yoksa çık
      {
         return;
      }

      foreach (Transform child in shape.transform )
      {
         Vector2 pos=VectoruIntYapFNC(child.position);//pozisyuon bilgilerini ınt yapıyoruz.
         ızgara[(int)pos.x, (int)pos.y]=child;
      }
   }
//bu aralıktaki kodlar tamamlnan satırı kontrol edip yok etmek içindir
   bool satırTamanlandımıFNC(int y)
   {
      for (int x = 0; x < genislik; ++x)
      {
         if (ızgara[x, y] == null)
         {
            return false;
         }
      }

      return true;
   }

   void SatiriTemizleFNC(int y)
   {
      for (int x = 0; x < genislik; ++x)
      {
         if (ızgara[x, y] != null)
         {
          Destroy(ızgara[x,y].gameObject);  
         }

         ızgara[x, y] = null;
      }
   }

   public bool dısariTaştımıFNC(ShapeManager shape)
   {
      foreach (Transform child in shape.transform)
      {
         if (child.transform.position.y >= yukseklik - 1)
         {
            return true;
         }
      }

      return false;
   }

   void BirSatırAsağiIndirFNC(int y)
      {
         for (int x = 0; x < genislik; ++x)
         {
            if (ızgara[x, y] != null)
            {
               ızgara[x, y - 1] = ızgara[x, y];
               ızgara[x, y] = null;
               ızgara[x, y - 1].position += Vector3.down; //unity sahnesinde bir birim aşağı çekme
            }
         }
      }

      void TumSatırlarıAsağiIndirFNC(int baslangıçY)
      {
         for (int i = baslangıçY; i < yukseklik; i++)
         {
            BirSatırAsağiIndirFNC(i);
         }
      }

      public IEnumerator TumSatirlarıTemizleFNC()
      {
         tamamlananaSatir = 0;
      //önce efekti çalıştırıyoruz
         for (int y = 0; y < yukseklik; y++)
         {
            if (satırTamanlandımıFNC(y))
            {
               SatirEfektiniCalistirFNC(tamamlananaSatir,y);
               
               tamamlananaSatir++;
            }
         }

         yield return new WaitForSeconds(.5f);
         
        //sonra bloklar yok oluyor 
         for (int y = 0; y < yukseklik; y++)
         {
            if (satırTamanlandımıFNC(y))
            {
               
               SatiriTemizleFNC(y);
               TumSatırlarıAsağiIndirFNC(y + 1);
               yield return new WaitForSeconds(.2f);
               y--;
            }
         }
      }
      //buraya kadar.


      
   
   
   Vector2 VectoruIntYapFNC(Vector2 vector)
   {
      //tam sayıya çeviriyoruz vector kordinatlarını
      return new Vector2(Mathf.Round(vector.x), Mathf.Round(vector.y));

   }

   void SatirEfektiniCalistirFNC(int kacinciSatır, int y)
   {
      //if (satirEfekti)
      //{
      //  satirEfekti.transform.position =new Vector3(0, y,0);
      // satirEfekti.EfectPlayFNC();
         
      //}
      if (satirEfektleri[kacinciSatır])
      {
         satirEfektleri[kacinciSatır].transform.position = new Vector3(0, y, 0);
         satirEfektleri[kacinciSatır].EfectPlayFNC();
      }
      
   }

}
