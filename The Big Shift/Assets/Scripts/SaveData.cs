using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;

public class SaveData : MonoBehaviour
{
    //[SerializeField] TextAsset XmlFile;
    public enum Levels { Level_1, Level_2, Level_3, Level_4, Water_Level, Boss_Level }

    public Levels level;

    public Level[] levels { get; private set; }

    Level l;

    int index = 0;

    int t = 1;

    private void Start()
    {
        levels = new Level[6];
        index = (int)level;
    }

    private void OnEnable()
    {
        Movement.playerScore += SaveDataPlayer;
    }

    private void OnDisable()
    {
        Movement.playerScore -= SaveDataPlayer;
    }


    [System.Serializable]
    public class Level
    {
        public string name;
        public float time;
        public int deaths;
        public int unlock;
    }

    Level CreateLevelData()
    {
        Level l = new Level();

        l.name = "Default";
        l.time = 999999f;
        l.deaths = 0;
        l.unlock = 0;

        return l;

    }

    void SaveDataPlayer(string levelName, int i, float time, int deaths)
    {
        l = CreateLevelData(levelName, time, deaths, 1);

        ReadXmlFile();
        SaveXmlFile();

    }

    void ResetXml()
    {
        levels[0] = CreateLevelData();

        XmlDocument xml = new XmlDocument();

        XmlElement root = xml.CreateElement("List");

        for (int i = 0; i < levels.Length; i++)
        {
            XmlElement level = xml.CreateElement("level");
            root.AppendChild(level);

            XmlElement time = xml.CreateElement("Time");
            time.InnerText = levels[0].time.ToString();
            level.AppendChild(time);

            XmlElement deaths = xml.CreateElement("Deaths");
            deaths.InnerText = levels[0].deaths.ToString();
            level.AppendChild(deaths);

            XmlElement unlock = xml.CreateElement("Unlock");
            unlock.InnerText = levels[0].unlock.ToString();
            level.AppendChild(unlock);
        }

        xml.AppendChild(root);

        xml.Save(Application.dataPath + "/StreamingAssets/DataXml.text");

    }

    Level CreateLevelData(string n, float t, int d, int c)
    {
        Level l = new Level();

        l.name = "Taco";
        l.time = t;
        l.deaths = d;
        l.unlock = c;

        return l;

    }

    void SaveXmlFile()
    {

        XmlDocument xml = new XmlDocument();

        XmlElement root = xml.CreateElement("List");

        for (int i = 0; i < levels.Length; i++)
        {

            XmlElement level = xml.CreateElement("level");
            root.AppendChild(level);

            XmlElement time = xml.CreateElement("Time");
            time.InnerText = levels[i].time.ToString();
            level.AppendChild(time);

            XmlElement deaths = xml.CreateElement("Deaths");
            deaths.InnerText = levels[i].deaths.ToString();
            level.AppendChild(deaths);

            XmlElement unlock = xml.CreateElement("Unlock");
            unlock.InnerText = i == index ? l.unlock.ToString() : levels[i].unlock.ToString();

            if (i == index && l.time < levels[i].time)
            {
                time.InnerText = l.time.ToString();
                deaths.InnerText = l.deaths.ToString();
            }

            if (i == index + 1)
            {
                unlock.InnerText = t.ToString();
            }

            level.AppendChild(unlock);

        }

        xml.AppendChild(root);

        xml.Save(Application.dataPath + "/StreamingAssets/DataXml.text");

    }

    public void ReadXmlFile()
    {
        if (!File.Exists(Application.dataPath + "/StreamingAssets/DataXml.text"))
        {
            return;
        }

        XmlDocument xmlDocument = new XmlDocument();

        TextAsset textAsset = Resources.Load<TextAsset>("/DataXml");

        //xmlDocument.Load(textAsset.text);
        xmlDocument.Load(Application.dataPath + "/StreamingAssets/DataXml.text");


        string path = "//List/level";
        XmlNodeList nodeList = xmlDocument.SelectNodes(path);

        for (int i = 0; i < nodeList.Count; i++)
        {
            XmlNode Time = nodeList[i].FirstChild;
            XmlNode Deaths = Time.NextSibling;
            XmlNode unlock = Deaths.NextSibling;

            Level l = new Level();

            l.time = float.Parse(Time.InnerText);
            l.deaths = int.Parse(Deaths.InnerText);
            l.unlock = int.Parse(unlock.InnerText);

            levels[i] = l;
        }





    }
}
