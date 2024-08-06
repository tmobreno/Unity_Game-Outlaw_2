using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BuildLevel : MonoBehaviour
{
    private int width, height;

    private void Start()
    {
        BuildBlocks();
    }

    public void BuildBlocks()
    {
        string fileContents = ReadFromFile();
        if (fileContents == null) { return; }
        string[] blocks = ParseFile(fileContents);

        width = int.Parse(blocks[0]);
        height = int.Parse(blocks[1]);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                int ind = 2 + (height * i) + j;
                int blockID = int.Parse(blocks[ind]);
                if (blockID != 0) { SpawnObject(i, j, LevelObjects.Instance.FindObject(blockID)); }
            }
        }
    }

    private void SpawnObject(int i, int j, GameObject obj)
    {
        float x = ((-0.25f * width) + 0.25f) + (0.5f * i);
        float y = ((-0.25f * height) + 0.25f) + (0.5f * j);
        GameObject g = Instantiate(obj, new Vector2(x, y), this.transform.rotation);
        g.transform.parent = this.gameObject.transform;
    }

    // Use LevelObject method Instance, and pass in "/User_Levels" as parameter
    private string ReadFromFile()
    {
        string path = Application.dataPath + "/User_Levels/" + LevelObjects.Instance.CurrentLevel + ".txt";
        if (File.Exists(path)) { return File.ReadAllText(path); }
        return null;
    }

    private string[] ParseFile(string input)
    {
        string[] values = input.Split('_');
        return values;
    }
}
