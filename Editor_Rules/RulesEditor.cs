using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class RulesEditor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void WriteToFile()
    {
        string folderPath = Application.dataPath + "/User_Rules";
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string s = ConvertToString();

        string path = Application.dataPath + "/User_Rules/" + RulesEditorUI.Instance.GetSaveName() + ".txt";
        File.WriteAllText(path, s);
    }

    private string ConvertToString()
    {
        string converted = "";

        foreach (Slider s in RulesEditorUI.Instance.GetSliders())
        {
            string sliderText = s.transform.GetChild(0).GetComponentInChildren<Text>().text;
            string result = Regex.Replace(sliderText, @"\s+", "");
            converted += result + "=" + (s.value / 2) + '\n';
        }

        return converted;
    }
}
