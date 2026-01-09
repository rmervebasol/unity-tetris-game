using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NewBehaviourScript : MonoBehaviour
{
    public float baslangıçAlpha = 1f;
    public float bitisAlpha = 0f;

    public float beklemesuresi = 0f;
    public float fadeSuresi = 1f;//geçiş süresi

    private void Start()
    {
        GetComponent<CanvasGroup>().alpha = baslangıçAlpha;

        StartCoroutine(FadeRoutineFNC());
    }

    IEnumerator FadeRoutineFNC()
    {
        yield return new WaitForSeconds(fadeSuresi);
        GetComponent<CanvasGroup>().DOFade(bitisAlpha,fadeSuresi);
        
    }



}
