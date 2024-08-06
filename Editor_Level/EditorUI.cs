using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EditorUI : MonoBehaviour
{
    public static EditorUI Instance;

    [SerializeField] private GameObject components;

    [SerializeField] private Slider widthSlider, heightSlider;

    [SerializeField] private InputField saveInput;

    [SerializeField] private GameObject[] topButtons;
    [SerializeField] private Button blockButton;

    [SerializeField] private Editor editor;

    private int topPage;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CreateButtons();
    }

    public void SetVisible(bool s)
    {
        if (s == false) editor.ResetGrid();
        if (s == true) editor.InitializeGrid();
        components.SetActive(s);
        if (s == false) RulesEditorUI.Instance.SetVisible(!s);
    }

    public int GetWidth()
    {
        return (int)widthSlider.value;
    }

    public int GetHeight()
    {
        return (int)heightSlider.value;
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

    public void SwitchPage(int change)
    {
        topPage += change;
        if (topPage > 2) topPage = 0;
        if (topPage < 0) topPage = 2;
        int i = 0;
        foreach (GameObject g in topButtons)
        {
            bool set = (i == topPage) ? true : false;
            g.SetActive(set);
            i++;
        }
    }

    private void CreateButtons()
    {
        int coordinate = -75;
        int count = 0;
        int page = 0;
        foreach(GameObject g in LevelObjects.Instance.GetObjects())
        {
            if (g.GetComponent<EmptyBlock>() != null) continue;

            CreateBlockButton(g, page, coordinate);

            coordinate -= 75;
            count += 1;

            if (count > 4) { 
                page += 1;
                count = 0;
                coordinate = -75;
            }
        }

        SwitchPage(0);
    }

    private void CreateBlockButton(GameObject g, int page, int coordinate)
    {
        Vector2 t = blockButton.transform.position;
        Button b = Instantiate(blockButton, new Vector2(t.x, t.y + coordinate), this.transform.rotation);
        b.transform.SetParent(topButtons[page].transform, false);
        b.GetComponentInChildren<Text>().text = g.GetComponent<EditorObject>().GetObjectName().ToString() + "   Block";
        b.onClick.AddListener(delegate { editor.SetObject(g); });
    }
}
