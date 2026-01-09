using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
   public bool oyunDurdumu = false;
   public GameObject pausePanel;

   private GameManager gameManager;

   private void Awake()
   {
      gameManager=FindObjectOfType<GameManager>();
   }

   private void Start()
   {
      if(pausePanel)
         pausePanel.SetActive(false);
   }

   private void Update()
   {
      if(Input.GetKeyDown(KeyCode.Escape))
         pausePanelAçKapat();
   }
   
   

   public void pausePanelAçKapat()
   {
      if (gameManager.gameOver)
         return;
      
      oyunDurdumu=!oyunDurdumu;

      if (pausePanel)
      {
         pausePanel.SetActive(oyunDurdumu);
         Time.timeScale = oyunDurdumu ? 0 : 1;
      }
   }

   public void YenidenOynaFNC()
   {
      Time.timeScale = 1;
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }



}
