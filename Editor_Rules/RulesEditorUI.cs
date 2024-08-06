using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class RulesEditorUI : MonoBehaviour
{
    public static RulesEditorUI Instance;

    [SerializeField] private GameObject components;

    [SerializeField] private InputField saveInput;

    [SerializeField] private List<Slider> sliders;

    [SerializeField] private GameObject sliderGroup;
    [SerializeField] private Slider paramSlider;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CreateSliders();
    }
    public string GetSaveName()
    {
        return saveInput.text;
    }

    public void ExitEditor()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("DefaultGameScene");
    }

    public void SetVisible(bool s)
    {
        components.SetActive(s);
        if (s == false) EditorUI.Instance.SetVisible(!s);
    }

    private void CreateSliders()
    {
        int coordinate = 0;

        UDictionary<PlayerStatNames, float> get = LevelObjects.Instance.GetRules();

        foreach (var item in get)
        {
            CreateSingleSlider(item.Key, coordinate);
            coordinate -= 75;
        }
    }

    private void CreateSingleSlider(PlayerStatNames name, int coordinate)
    {
        Vector2 t = paramSlider.transform.position;
        Slider s = Instantiate(paramSlider, new Vector2(t.x, t.y + coordinate), this.transform.rotation);
        sliders.Add(s);
        s.transform.SetParent(sliderGroup.transform, false);

        if (name == PlayerStatNames.BulletSpeed || name == PlayerStatNames.MoveSpeed) s.minValue = 1;

        string result = FormString(name.ToString());
        s.transform.GetChild(0).GetComponentInChildren<Text>().text = result;

        s.onValueChanged.AddListener(delegate { AdjustSlider(s); });

        AdjustSlider(s);
    }

    private string FormString(string input)
    {
        string result = Regex.Replace(input, "(?<!^)([A-Z])", "   $1");
        return result;
    }

    public List<Slider> GetSliders()
    {
        return sliders;
    }

    private void AdjustSlider(Slider slider)
    {
        slider.transform.GetChild(1).GetComponentInChildren<Text>().text = (slider.value / 2).ToString();
    }
}
