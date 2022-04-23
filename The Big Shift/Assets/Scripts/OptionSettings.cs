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
    [SerializeField] Dropdown ScreenMode;


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

        MasterVolume.value = float.Parse(master.InnerText);
        SFXVolume.value = float.Parse(sfx.InnerText);
        MusicVolume.value = float.Parse(music.InnerText);

        ScreenMode.value = int.Parse(screen.InnerText);

        SetWindowMode(int.Parse(screen.InnerText));

        SetMasterVolume(MasterVolume.value);
        SetSFXvolume(SFXVolume.value);
        SetMusicVolume(MusicVolume.value);
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

        xml.AppendChild(root);

        xml.Save(Application.dataPath + "/StreamingAssets/Options.text");
    }

}
