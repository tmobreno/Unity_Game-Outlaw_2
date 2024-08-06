using UnityEngine;
using System.IO;
using System;

public class LevelObjects : MonoBehaviour
{
    public static LevelObjects Instance;

    [SerializeField] private GameObject[] Objects;

    [field: SerializeField]
    public string CurrentLevel { get; private set; }

    private PlayerStats Rules;

    [field: SerializeField]
    public string CurrentRules { get; private set; } // Just for tracking

    // Start is called before the first frame update
    void Awake()
    {
        Rules = this.GetComponent<PlayerStats>();

        CurrentLevel = "Default_Level";
        SetCurrentRules("Default_Rules");

        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetCurrentLevel(string level)
    {
        CurrentLevel = level;
    }

    public void SetCurrentToDefault()
    {
        CurrentLevel = "Default_Level";
        SetCurrentRules("Default_Rules");
    }

    public GameObject[] GetObjects()
    {
        return Objects;
    }

    public GameObject FindObject(int id)
    {
        foreach (GameObject obj in Objects)
        {
            if(obj.GetComponent<EditorObject>().GetObjectID() == id)
            {
                return obj;
            }
        }
        return Objects[0];
    }



    // Set Rules
    public UDictionary<PlayerStatNames, float> GetRules()
    {
        return Rules.GetRules();
    }

    public void SetCurrentRules(string rules)
    {
        string[] fileContents = ReadFromFile(rules);
        if (fileContents == null) { return; }

        CurrentRules = rules;

        foreach (string line in fileContents)
        {
            string[] values = line.Split('=');
            string name = values[0];
            string value = values[1];

            if (Enum.TryParse(name, out PlayerStatNames statName))
            {
                Rules.GetRules()[statName] = float.Parse(value);
            }
        }
    }

    private string[] ReadFromFile(string rules)
    {
        string path = Application.dataPath + "/User_Rules/" + rules + ".txt";
        if (File.Exists(path)) { return File.ReadAllLines(path); }
        return null;
    }
}
