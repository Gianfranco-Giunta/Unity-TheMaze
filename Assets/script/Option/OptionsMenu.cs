using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public GameObject CanvasMenu;
    public GameObject CanvasOption;
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    private bool isInvertLook;

    void Start()
    {
        float volume;
        audioMixer.GetFloat("Volume", out volume);
        volumeSlider.value = ConvertDecibelToSliderValue(volume);
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float sliderValue){
        float volume= ConvertSliderValueToDecibel(sliderValue);
        audioMixer.SetFloat("Volume", volume);
    }

    private float ConvertDecibelToSliderValue(float decibel)
    {
        return Mathf.InverseLerp(-80f, 0f, decibel);
    }

    private float ConvertSliderValueToDecibel(float sliderValue)
    {
        return Mathf.Lerp(-80f, 0f, sliderValue);
    }


    public void SetLowQuality(){
        QualitySettings.SetQualityLevel(0);
    }

    public void SetDifficultyLow(){
        GameManager.Instance.SetDifficulty(0);
    }

    public void SetDifficultyHigh(){
        GameManager.Instance.SetDifficulty(1);
    }

    public void SetHighQuality(){
        QualitySettings.SetQualityLevel(5);
    }

    public void ChangeControlConfig(){
        isInvertLook = !isInvertLook; 
        Debug.Log(isInvertLook);
        PlayerPrefs.SetInt("InvertLook", isInvertLook? 1 : 0);
        PlayerPrefs.Save();
    }

    public bool IsInvertLook()
    {
        return isInvertLook;
    }

    public void Indietro(){
        CanvasOption.SetActive(false);
        CanvasMenu.SetActive(true);
    }

}
