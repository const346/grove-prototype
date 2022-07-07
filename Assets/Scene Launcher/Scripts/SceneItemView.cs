using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneItemView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI nameText;

    [SerializeField]
    private TextMeshProUGUI indexText;

    public int BuildIndex { get; set; }
    public string ScenePath { get; set; }

    public void LoadScene()
    {
        var launcher = FindObjectOfType<Launcher>();
        launcher.LoadScene(BuildIndex);
    }

    private void Start()
    {
        if (nameText)
        {
            var name = System.IO.Path.GetFileNameWithoutExtension(ScenePath);
            var path = System.IO.Path.GetDirectoryName(ScenePath);

            nameText.text = $"{path}/<b>{name}</b>";
        }

        if (indexText)
        {
            indexText.text = BuildIndex.ToString();
        }
    }
}