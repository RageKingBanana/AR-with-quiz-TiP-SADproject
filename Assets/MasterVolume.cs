using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterVolume : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider sfxSlider;
    public Slider bgSlider;
    public Slider masterSlider;
    private float volumeSaved;
    void Start()
    {
        volumeSaved = PlayerPrefs.GetFloat("volume");
        masterSlider.value = volumeSaved;
    }

    // Update is called once per frame
    void Update()
    {
        if (sfxSlider.value > masterSlider.value)
            sfxSlider.value = masterSlider.value;
        if (bgSlider.value > masterSlider.value)
            bgSlider.value = masterSlider.value;
    }
}
