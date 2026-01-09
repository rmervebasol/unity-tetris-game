using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakipShapeManager : MonoBehaviour
{
    private ShapeManager takipShape=null;
    private bool dibeDeğdimi = false;
    public Color color = new Color(1f, 1f, 1f, .2f);


    public void TakipShapeOluşturFNC(ShapeManager gerçekShape, BoardManager board)
    {
        if (!takipShape)
        {
            takipShape = Instantiate(gerçekShape, gerçekShape.transform.position, gerçekShape.transform.rotation) as ShapeManager;

            takipShape.name = "TakipShape";
            
            SpriteRenderer[] tumSprite=takipShape.GetComponentsInChildren<SpriteRenderer>();

            foreach (SpriteRenderer sr in tumSprite)
            {
                sr.color = color;
            }
        }
        else
        {
           takipShape.transform.position=gerçekShape.transform.position; 
           takipShape.transform.rotation=gerçekShape.transform.rotation;
        }

        dibeDeğdimi = false;
        while (!dibeDeğdimi)
        {
            takipShape.AsagiHareketFNC();
            if (!board.GeçerliPozisyondamı(takipShape))
            {
                takipShape.YukarıHareketFNC();
                dibeDeğdimi = true;
            }
        }
    }
    
    public void ResetFNC()
    {
        Destroy(takipShape.gameObject);
    }
      

}
