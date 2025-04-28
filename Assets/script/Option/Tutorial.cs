using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    public GameObject CanvasMenu;
    public GameObject CanvasTutorial;
    public GameObject Tutorial2;
    public GameObject Tutorial1;

    public void Avanti(){
        Tutorial1.SetActive(false);
        Tutorial2.SetActive(true);
    }

    public void Indietro1(){
       CanvasTutorial.SetActive(false);
       CanvasMenu.SetActive(true);
    }
    public void Indietro2(){
       Tutorial1.SetActive(true);
       Tutorial2.SetActive(false);
    }
}
