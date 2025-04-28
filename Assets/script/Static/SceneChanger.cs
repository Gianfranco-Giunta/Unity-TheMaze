using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneChanger
{
     public static void ChangeSceneGame()
    {
        Cursor.visible = false;
        SceneManager.LoadScene("Partita");
    }

    public static void ChangeSceneMenu()
    {
        Cursor.visible = true;
        SceneManager.LoadScene("Menu");
    }
}
