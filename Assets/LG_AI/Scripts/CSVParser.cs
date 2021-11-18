using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class CSVParser : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMesh;

    List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
    private void Start()
    {
        list = CSVReader.Read("blendweights_Seq01");

        foreach (var l in list)
        {
            foreach (var _l in l)
            {
                Debug.Log($"{_l.Key} : {_l.Value}");
            }
            Debug.Log("");
        }
    }
}
