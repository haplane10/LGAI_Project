using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IntroScenario : MonoBehaviour
{
    [SerializeField] ScenarioSO scenario;
    [SerializeField] TextMeshProUGUI scenarioTextMesh;
    [SerializeField] GameObject cameraPermmision;
    int scenarioIdx = 0;

    // Start is called before the first frame update
    void Start()
    {
        scenarioTextMesh.text = scenario.Speeches[scenarioIdx];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveNextScenario()
    {
        scenarioIdx++;

        if (scenarioIdx > scenario.Speeches.Length) return;

        if (scenarioIdx == scenario.Speeches.Length)
        {
            scenarioIdx = scenario.Speeches.Length;
            cameraPermmision.SetActive(true);
        }

        if (scenarioIdx < scenario.Speeches.Length)
        {
            scenarioTextMesh.text = scenario.Speeches[scenarioIdx];
        }
    }
}
