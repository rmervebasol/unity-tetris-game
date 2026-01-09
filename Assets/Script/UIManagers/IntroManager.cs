using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class İntroManagement : MonoBehaviour
{
    public GameObject[] sayilar;

    public GameObject sayilarTransform;//baştaki transformun dönmesi için
    
    //intro mangerda game manager nesnesi tanımlıyoruz
    private GameManager gameManager;

    private void Awake()
    {
        gameManager=GameObject.FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        StartCoroutine(SayilariAcRoutine());
    }
    IEnumerator SayilariAcRoutine()
    {
        yield return new WaitForSeconds(.1f);
        //Parent objeyi 0 dereceye doğru 0.3 saniyede döndürerek animasyon yapar
        // Ease.OutBack = hafif geri gelip ileri giden yumuşak animasyon efekti
        sayilarTransform.GetComponent<RectTransform>().DORotate(Vector3.zero, .3f).SetEase(Ease.OutBack);
        sayilarTransform.GetComponent<CanvasGroup>().DOFade(1, .3f);
        
        yield return new WaitForSeconds(.2f);
        int sayac = 0;
        while (sayac < sayilar.Length)
        {
            //sayilar[sayac].GetComponent<RectTransform>().DOLocalMoveY(0, .5f);
            sayilar[sayac].GetComponent<RectTransform>().DOAnchorPosY(0, .5f);

            sayilar[sayac].GetComponent<CanvasGroup>().DOFade(1, .5f);

            sayilar[sayac].GetComponent<RectTransform>().DOScale(2f, .3f).SetEase(Ease.OutBounce).OnComplete(()=>
                    sayilar[sayac].GetComponent<RectTransform>().DOScale(1f,.3f).SetEase(Ease.InBack));
            
            yield return new WaitForSeconds(1.5f);

            sayac++;

           //sayilar[sayac - 1].GetComponent<RectTransform>().DOLocalMoveY(150f, .5f);
           sayilar[sayac - 1].GetComponent<RectTransform>().DOAnchorPosY(150f, .5f);

           
           
            sayilar[sayac - 1].GetComponent<CanvasGroup>().DOFade(0, .3f);
            yield return new WaitForSeconds(.1f);
        }
        
        sayilarTransform.GetComponent<CanvasGroup>().DOFade(0, .5f).OnComplete(() =>
            {
               sayilarTransform.transform.parent.gameObject.SetActive(false);
               //oyunu başlatma yapılıcak

               gameManager.OyunaBaslaFNC();
            }
            
            
            
            );
        


    }
    
    
    
    

}
