using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControllers : MonoBehaviour
{
    public Slider _musicSlider;
    
    public void ToggleMusic()
    {
        AudioMenu.Instance.ToggleMusic();

    }
    public void MusicVolume()
    {
        AudioMenu.Instance.MusicVolume(_musicSlider.value);
    }

}
