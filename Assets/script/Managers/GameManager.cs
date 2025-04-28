using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    private float gameDuration;

    public static GameManager Instance;
    private int difficulty;
    public float delayBeforeLoading = 0.1f;
    public bool LoadNew;
    
    void Awake()
    {
       if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    void Start(){
        gameDuration=0F;

         if (PlayerPrefs.HasKey("Difficulty")){
             difficulty= PlayerPrefs.GetInt("Difficulty");
         }
         else {
            difficulty=0;
         }
    }

    void Update(){
        gameDuration+=Time.deltaTime;
    }

    public void StartGame(){

        LoadNew= false;

        SceneChanger.ChangeSceneGame();

    }

    public void LoadGame()
    {
        if (!PlayerPrefs.HasKey("PlayerHealth"))
        {
            Debug.Log("Nessun salvataggio disponibile!");
        }
        else{
            LoadNew=true;
            SceneChanger.ChangeSceneGame();
            }
    }

    public bool GetBool()
    {
        return LoadNew;
    }

    public void SetDifficulty(int diff){
        difficulty= diff;
    }

    public int GetDifficulty()
    {
        return difficulty;
    }


    public void EndGame()
    {

        SaveScore.AddCompletionTimes(gameDuration);
        PlayerPrefs.SetInt("Difficulty", difficulty);
        Debug.Log(SaveScore.GetTotalMonstersKilled());
        
        Debug.Log("Tempo di gioco salvato" + gameDuration);

        Debug.Log("Fine del gioco! Ritorno al menu principale...");
        SceneChanger.ChangeSceneMenu();
    }
}
