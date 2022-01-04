using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class BlendToCSV : MonoBehaviour
{
    [HideInInspector][SerializeField] AudioSource motionAudio;
    [HideInInspector][SerializeField] float delayTime;

    [SerializeField] Animation[] motions;
    [SerializeField] List<SkinnedMeshRenderer> skinnedMeshes;

    int cur_motionCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        SetBlendshapeMesh();
        MotionClipToCSV();
        //StartCoroutine(PlaySound(delayTime));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetBlendshapeMesh()
    {
        foreach(var motion in motions)
        {
            var _skinnedMeshes = motion.GetComponentsInChildren<SkinnedMeshRenderer>();
            var _blendMesh = from _mesh in _skinnedMeshes where _mesh.sharedMesh.blendShapeCount > 0 select _mesh;
            skinnedMeshes.Add(_blendMesh.First());
            motion.gameObject.SetActive(false);
        }
    }
    IEnumerator PlaySound(float _delayTime)
    {
        yield return new WaitForSeconds(_delayTime);
        motionAudio.Play();
    }

    public void MotionClipToCSV()
    {
        if (cur_motionCount >= motions.Length) return;
        
        StartCoroutine(co_MotionClipToCSV(cur_motionCount));
    }

    IEnumerator co_MotionClipToCSV(int idx)
    {
        motions[cur_motionCount].gameObject.SetActive(true);

        int _count = skinnedMeshes[idx].sharedMesh.blendShapeCount;
        string _csv = string.Empty;
        
        for (int i = 0; i < _count; i++)
        {
            var _blendName = skinnedMeshes[idx].sharedMesh.GetBlendShapeName(i);
            _blendName = _blendName.Split('.')[1];
            _csv += _blendName;
            if (i < _count - 1)
                _csv += ",";
            else
                _csv += "\n";
        }

        yield return new WaitUntil(() => motions[idx].isPlaying);

        while (motions[idx].isPlaying)
        {
            yield return new WaitForFixedUpdate();
            for (int i = 0; i < _count; i++)
            {
                var _blendWeight = skinnedMeshes[idx].GetBlendShapeWeight(i);
                _csv += string.Format("{0:0.####}", _blendWeight * 0.01);
                if (i < _count - 1)
                    _csv += ",";
                else
                    _csv += "\n";
            }
        }

        var _filePath = Application.dataPath + "/Resources/" + motions[idx].gameObject.name + ".csv";
        StartCoroutine(WriteCSV(_filePath, _csv));

        Debug.Log(_csv);
    }

    IEnumerator WriteCSV(string _filePath, string _csv)
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

        yield return new WaitForSeconds(2f);

        motions[cur_motionCount].gameObject.SetActive(false);
        cur_motionCount++;
        MotionClipToCSV();
    }
}
