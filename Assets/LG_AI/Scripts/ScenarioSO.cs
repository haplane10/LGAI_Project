using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Speech", order = 1)]
public class ScenarioSO : ScriptableObject
{
    [Multiline]
    public string[] Speeches;
}
