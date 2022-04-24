using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.UI;
using System.IO;
using System.Xml;


public class OptionSettings : MonoBehaviour
{
    // Start is called before the first frame update
    public string Master, SFX, Music;
    [SerializeField] Button start;
    [SerializeField] Slider MasterVolume, SFXVolume, MusicVolume;
    [SerializeField] Dropdown ScreenMode, Resolution;

    private void Start()
    {
        start.Select();
        LoadOptionsData();


    }

    public void CloseOptions()
    {
        start.Select();
        SaveOptionsData();
    }

    public void OpenOptions()
    {
        MasterVolume.Select();
        LoadOptionsData();
    }

    public void SetWindowMode(int value)
    {
        switch (value)
        {
            case 0:
                Screen.fullScreen = true;
                break;

            case 1:
                Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
                break;

            case 2:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
        }
    }

    public void SetResolution(int value)
    {
        switch (value)
        {
            case 0:
                Screen.SetResolution(1024, 768, Screen.fullScreen);
                PlayerPrefs.SetInt("Res", 1);
                PlayerPrefs.SetFloat("Size", 17f);

                break;

            case 1:
                Screen.SetResolution(1280, 768, Screen.fullScreen);
                PlayerPrefs.SetInt("Res", 0);
                PlayerPrefs.SetFloat("Size", 13.5f);
                break;

            case 2:
                Screen.SetResolution(1400, 1050, Screen.fullScreen);
                PlayerPrefs.SetInt("Res", 1);
                PlayerPrefs.SetFloat("Size", 17f);
                break;

            case 3: Screen.SetResolution(1600, 900, Screen.fullScreen);
                PlayerPrefs.SetInt("Res", 0);
                PlayerPrefs.SetFloat("Size", 12.9f);
                break;

            case 4: Screen.SetResolution(1920, 1080, Screen.fullScreen);
                PlayerPrefs.SetInt("Res", 0);
                PlayerPrefs.SetFloat("Size", 12.9f);
                break;
        }

        PlayerPrefs.Save();
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

    void UpdateVolume()
    {
        SetMasterVolume(MasterVolume.value);
        SetSFXvolume(SFXVolume.value);
        SetMusicVolume(MusicVolume.value);
    }

    public void LoadOptionsData()
    {
        if (!File.Exists(Application.dataPath + "/StreamingAssets/Options.text"))
        {
            return;
        }

        XmlDocument xmlDocument = new XmlDocument();

        //xmlDocument.Load(textAsset.text);
        xmlDocument.Load(Application.dataPath + "/StreamingAssets/Options.text");

        string path = "//Options";
        XmlNodeList nodeList = xmlDocument.SelectNodes(path);

        XmlNode master = nodeList[0].FirstChild;
        XmlNode sfx = master.NextSibling;
        XmlNode music = sfx.NextSibling;
        XmlNode screen = music.NextSibling;
        XmlNode resolution = screen.NextSibling;

        MasterVolume.value = float.Parse(master.InnerText);
        SFXVolume.value = float.Parse(sfx.InnerText);
        MusicVolume.value = float.Parse(music.InnerText);
        ScreenMode.value = int.Parse(screen.InnerText);
        Resolution.value = int.Parse(resolution.InnerText);

        SetWindowMode(int.Parse(screen.InnerText));
        SetResolution(int.Parse(resolution.InnerText));

        UpdateVolume();

    }

    public void SaveOptionsData()
    {
        XmlDocument xml = new XmlDocument();

        XmlElement root = xml.CreateElement("Options");

        XmlElement Master = xml.CreateElement("Master");
        Master.InnerText = MasterVolume.value.ToString();
        root.AppendChild(Master);

        XmlElement sfx = xml.CreateElement("SFX");
        sfx.InnerText = SFXVolume.value.ToString();
        root.AppendChild(sfx);

        XmlElement music = xml.CreateElement("Music");
        music.InnerText = MusicVolume.value.ToString();
        root.AppendChild(music);

        XmlElement screen = xml.CreateElement("Screen");
        screen.InnerText = ScreenMode.value.ToString();
        root.AppendChild(screen);

        XmlElement resoultion = xml.CreateElement("Res");
        resoultion.InnerText = Resolution.value.ToString();
        root.AppendChild(resoultion);

        xml.AppendChild(root);

        xml.Save(Application.dataPath + "/StreamingAssets/Options.text");

        UpdateVolume();
    }

}
