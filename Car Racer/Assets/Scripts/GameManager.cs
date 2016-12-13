using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.Collections.Generic;
//##########
using System.Xml;
using System.Text;

public class GameManager : MonoBehaviour
{
    public TextAsset XMLFile;

    void Start()
    {
        /* Load from xml file 
        http://unitynoobs.blogspot.co.il/2011/02/xml-loading-data-from-xml-file.html
        http://wiki.unity3d.com/index.php?title=Saving_and_Loading_Data:_XmlSerializer
        XmlDocument xmlDoc = new XmlDocument(); // xmlDoc is the new xml document.
        xmlDoc.LoadXml(XMLFile.text); // load the file.
        XmlNodeList levelsList = xmlDoc.GetElementsByTagName("arena");

        foreach (XmlNode levelInfo in levelsList)
        {
            if (levelInfo.Attributes["number"].Value == "1")
                print("yey1");
            else if (levelInfo.Attributes["number"].Value == "2")
                print("yey2");
            else print("noo");

            XmlNodeList levelcontent = levelInfo.ChildNodes;
            foreach (XmlNode levelsItens in levelcontent)
            {
              
            }
        }*/

        Instantiate(Resources.Load("Arenas/"+MenuScript.Levels[MenuScript.levelSelected]) as GameObject,
            Vector3.zero,
            new Quaternion(0, 0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
