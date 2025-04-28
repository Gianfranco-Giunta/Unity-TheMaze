using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject CanvasMenu;
    public GameObject CanvasOption;
    public GameObject CanvasTutorial;
    public GameObject CanvasCredits;
  
    public Text completionTimesText;
    public Text totalMonstersText;

    void Start(){

        Cursor.lockState = CursorLockMode.None;
        PlayerPrefs.SetInt("InvertLook",0);

        List<float> completionTimes = SaveScore.GetCompletionTimes();
        int totalMonstersKilled = SaveScore.GetTotalMonstersKilled();
        completionTimesText.text = "Migliori 5 tempi \ndi sopravvivenza:\n\n";
        for (int i = 0; i < Mathf.Min(5, completionTimes.Count); i++)
        {
            completionTimesText.text += completionTimes[i] + " secondi\n";
        }
        totalMonstersText.text = "Totale mostri uccisi: \n\n" + totalMonstersKilled;
    }
    
    public void StartGame(){

        GameManager.Instance.StartGame();

    }

    public void OpenTutorial(){
        CanvasTutorial.SetActive(true);
        CanvasMenu.SetActive(false);
    }


    public void OpenOptions(){
        CanvasOption.SetActive(true);
        CanvasMenu.SetActive(false);
    }

    public void LoadGame(){
        GameManager.Instance.LoadGame();
    }
    public void OpenCredits(){
        CanvasCredits.SetActive(true);
        CanvasMenu.SetActive(false);
    }

    public void ResetStats(){
        SaveScore.ResetStatistics();
    }

    public void ExitGame(){
        Application.Quit();
    }

}
