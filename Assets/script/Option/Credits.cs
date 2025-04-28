using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public GameObject CanvasMenu;
    public GameObject CanvasCredits;

    public void Indietro(){
       CanvasCredits.SetActive(false);
       CanvasMenu.SetActive(true);
    }

}
