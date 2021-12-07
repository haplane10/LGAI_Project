using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class CSVParser : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMesh;
    public AudioSource audioSource;
    public Animator animator;

    List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
    private void Start()
    {
        list = CSVReader.Read("blendweights_Seq01");
        Debug.Log(list.Count);
    }

    public void OnPlayButtonClick()
    {
        StartCoroutine(PlayBlendShape(0.15f));
        StartCoroutine(PlaySound(0f));
    }

    IEnumerator PlayBlendShape(float _delayTime)
    {
        yield return new WaitForSeconds(_delayTime);
        //animator.SetBool("Speech", true);
        foreach (var l in list)
        {
            foreach (var _l in l)
            {
                var keyValue = _l.Key;
                
                if (keyValue.Contains("_L"))
                    keyValue = keyValue.Replace("_L", "Left");

                if (keyValue.Contains("_R"))
                    keyValue = keyValue.Replace("_R", "Right");

                var _idx = skinnedMesh.sharedMesh.GetBlendShapeIndex($"BS_Node.{keyValue}");
                
                skinnedMesh.SetBlendShapeWeight(_idx, float.Parse(_l.Value.ToString()) * 100);
            }
            yield return new WaitForFixedUpdate();
        }

        //animator.SetBool("Speech", false);
        for (int i = 0; i < skinnedMesh.sharedMesh.blendShapeCount; i++)
        {
            skinnedMesh.SetBlendShapeWeight(i, 0);
        }
    }

    IEnumerator PlaySound(float _delayTime)
    {
        yield return new WaitForSeconds(_delayTime);
        audioSource.Play();
    }
}
