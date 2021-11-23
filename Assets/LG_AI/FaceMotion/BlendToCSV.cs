using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BlendToCSV : MonoBehaviour
{
    [HideInInspector][SerializeField] AudioSource motionAudio;
    [HideInInspector][SerializeField] float delayTime;

    [SerializeField] Animation motion;
    [SerializeField] SkinnedMeshRenderer skinnedMesh;

    // Start is called before the first frame update
    void Start()
    {
        MotionClipToCSV();
        //StartCoroutine(PlaySound(delayTime));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PlaySound(float _delayTime)
    {
        yield return new WaitForSeconds(_delayTime);
        motionAudio.Play();
    }

    public void MotionClipToCSV()
    {
        StartCoroutine(co_MotionClipToCSV());
    }

    IEnumerator co_MotionClipToCSV()
    {
        int _count = skinnedMesh.sharedMesh.blendShapeCount;
        string _csv = string.Empty;
        
        for (int i = 0; i < _count; i++)
        {
            var _blendName = skinnedMesh.sharedMesh.GetBlendShapeName(i);
            _blendName = _blendName.Split('.')[1];
            _csv += _blendName;
            if (i < _count - 1)
                _csv += ",";
            else
                _csv += "\n";
        }

        yield return new WaitUntil(() => motion.isPlaying);

        while (motion.isPlaying)
        {
            yield return new WaitForFixedUpdate();
            for (int i = 0; i < _count; i++)
            {
                var _blendWeight = skinnedMesh.GetBlendShapeWeight(i);
                _csv += string.Format("{0:0.####}", _blendWeight * 0.01);
                if (i < _count - 1)
                    _csv += ",";
                else
                    _csv += "\n";
            }
        }

        var _filePath = Application.dataPath + "/Resources/" + motion.gameObject.name + ".csv";
        WriteCSV(_filePath, _csv);

        Debug.Log(_csv);
    }

    void WriteCSV(string _filePath, string _csv)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(Path.GetDirectoryName(_filePath));

        if (!directoryInfo.Exists)
        {
            directoryInfo.Create();
        }

        FileStream fileStream
            = new FileStream(_filePath, FileMode.OpenOrCreate, FileAccess.Write);

        StreamWriter writer = new StreamWriter(fileStream, System.Text.Encoding.UTF8);

        writer.WriteLine(_csv);
        writer.Close();
    }
}
