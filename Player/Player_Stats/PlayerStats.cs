using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private UDictionary<PlayerStatNames, float> Rules;

    private void Start()
    {
        Rules = LevelObjects.Instance.GetRules();
    }

    public UDictionary<PlayerStatNames, float> GetRules()
    {
        return Rules;
    }
}
