using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Globalization;

public static class SaveScore
{
    private const string TimesKey="C";
    private const string TotalMonstersKilledKey="M";

    public static void  AddCompletionTimes(float time){
         var times = GetCompletionTimes();       
         times.Add(time);
         times.Sort((a, b) => b.CompareTo(a));
         SaveCompletionTimes(times);
    }

    public static List<float> GetCompletionTimes()
    {
        string timesString = PlayerPrefs.GetString(TimesKey, "");
        var times = new List<float>();

        if (!string.IsNullOrEmpty(timesString))
        {
            string[] timeStrings = timesString.Split('.');
            foreach (var timeStr in timeStrings)
            {
                if (float.TryParse(timeStr, out float result))
                {
                    times.Add(result);
                }
            }
        }
        return times;

    }

    private static void SaveCompletionTimes(List<float> times)
    {

        string timesString = string.Join("." , times);
        Debug.Log(timesString);
        PlayerPrefs.SetString(TimesKey, timesString);
        PlayerPrefs.Save();
    }

    public static void KillMonster()
    {
        int currentKills = PlayerPrefs.GetInt(TotalMonstersKilledKey, 0);
        currentKills++;
        PlayerPrefs.SetInt(TotalMonstersKilledKey, currentKills);
        PlayerPrefs.Save();
    }

    public static int GetTotalMonstersKilled()
    {
        return PlayerPrefs.GetInt(TotalMonstersKilledKey, 0);
    }

    public static void ResetStatistics()
    {
        PlayerPrefs.DeleteAll(); // Rimuove tutti i dati salvati
        Debug.Log("Game statistics reset!");
    }
   
}

