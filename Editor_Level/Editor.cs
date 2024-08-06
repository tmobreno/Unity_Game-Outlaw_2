using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Editor : MonoBehaviour
{
    private int width;
    private int height;

    private GameObject[,] Objects;
    private GameObject selectedObject;

    [SerializeField] private GameObject obj;
    [SerializeField] private GameObject defaultObject;

    private void Start()
    {
        width = EditorUI.Instance.GetWidth();
        height = EditorUI.Instance.GetHeight();

        InitializeGrid();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider != null && hit.collider.CompareTag("Space"))
            {
                Vector2 t = hit.collider.transform.position;
                int i = (int)((t.x - ((-0.25f * width) + 0.25f)) * 2);
                int j = (int)((t.y - ((-0.25f * height) + 0.25f)) * 2);
                ReplaceObject(i, j);
            }
        }

        if (Input.GetMouseButton(1))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider != null && hit.collider.CompareTag("Space"))
            {
                Vector2 t = hit.collider.transform.position;
                int i = (int)((t.x - ((-0.25f * width) + 0.25f)) * 2);
                int j = (int)((t.y - ((-0.25f * height) + 0.25f)) * 2);
                ReplaceObject(i, j, defaultObject);
            }
        }
    }

    public void ReplaceObject(int i, int j)
    {
        Destroy(Objects[i, j].gameObject);
        SpawnObject(i, j, selectedObject);
    }

    public void ReplaceObject(int i, int j, GameObject g)
    {
        Destroy(Objects[i, j].gameObject);
        SpawnObject(i, j, g);
    }

    private void SpawnObject(int i, int j, GameObject obj)
    {
        float x = ((-0.25f * width) + 0.25f) + (0.5f * i);
        float y = ((-0.25f * height) + 0.25f) + (0.5f * j);
        Objects[i, j] = Instantiate(obj, new Vector2(x, y), this.transform.rotation);
        Objects[i, j].gameObject.tag = "Space";
        Objects[i, j].transform.parent = this.transform;
    }

    public void SetObject(GameObject obj)
    {
        selectedObject = obj;
    }

    public void AdjustWidth()
    {
        ResetGrid();
        width = EditorUI.Instance.GetWidth();
        InitializeGrid();
    }

    public void AdjustHeight()
    {
        ResetGrid();
        height = EditorUI.Instance.GetHeight();
        InitializeGrid();
    }

    public void InitializeGrid()
    {
        selectedObject = defaultObject;

        Objects = new GameObject[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                SpawnObject(i, j, selectedObject);
            }
        }
    }

    public void ResetGrid()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Destroy(Objects[i, j].gameObject);
            }
        }
    }

    private string ConvertToString()
    {
        string converted = "";
        converted += width.ToString() + "_";
        converted += height.ToString() + "_";

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                converted += Objects[i, j].GetComponent<EditorObject>().GetObjectID() + "_";
            }
        }
        return converted;
    }

    public void WriteToFile()
    {
        string folderPath = Application.dataPath + "/User_Levels";
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string s = ConvertToString();

        string path = Application.dataPath + "/User_Levels/" + EditorUI.Instance.GetSaveName() + ".txt";
        File.WriteAllText(path, s);
    }
}
