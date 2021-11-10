using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingGuide : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene(int _idx)
    {
        GameManager.Instance.ChangeScene(_idx);
    }
}
