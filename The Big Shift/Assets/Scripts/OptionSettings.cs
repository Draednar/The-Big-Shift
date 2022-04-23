using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class OptionSettings : MonoBehaviour
{
    // Start is called before the first frame update
    public string Master, SFX, Music;

    public void SetWindowMode(int value)
    {
        switch (value)
        {
            case 0: Screen.fullScreen = true;
                Debug.Log("0");
                break;

            case 1: Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                Debug.Log("1");
                break;

            case 2: Screen.fullScreenMode = FullScreenMode.Windowed;
                Debug.Log("2");
                break;
        }
    }


    public void SetMasterVolume(float value)
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName(Master, value);
    }

    public void SetSFXvolume(float value)
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName(SFX, value);
    }

    public void SetMusicVolume(float value)
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName(Music, value);
    }

}
