using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    public AudioSource attackPlayer;
    public AudioSource Back;
    public AudioSource attackGoblin;
    public AudioSource attackBoss;

    void Awake()
    {
         Instance = this;

    }

    void Start(){
        Back.Play();
    }

    public void PlayAttackPlayer()
    {
        attackPlayer.Play();
    }

    public void PlayAttackGoblin()
    {
        attackGoblin.Play();
    }

    public void PlayAttackBoss()
    {
        attackBoss.Play();
    }
}
