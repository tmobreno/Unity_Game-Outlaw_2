using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private Text scoreTextLeft, scoreTextRight;
    private int[] scores = new int[2] { 0, 0 };

    public static InGameUI Instance;

    [SerializeField] private GameObject components;

    private void Awake()
    {
        Instance = this;
    }

    public void SetText()
    {
        scoreTextLeft.text = scores[0].ToString();
        scoreTextRight.text = scores[1].ToString();
    }

    public void IncrementScore(int index)
    {
        scores[index] += 1;
        SetText();
    }

    public void ResetScores()
    {
        scores[0] = 0;
        scores[1] = 0;
    }

    public void SetVisible(bool s)
    {
        components.SetActive(s);
    }
}
