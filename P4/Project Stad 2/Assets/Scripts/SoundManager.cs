using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour {
    public Slider volumeSlider;
	// Use this for initialization
	void Start () {
        volumeSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ValueChangeCheck()
    {
        AudioListener.volume = volumeSlider.value;
    }
}
