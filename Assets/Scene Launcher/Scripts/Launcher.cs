using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviour
{
    [SerializeField]
    private BackUI backTemplate;

    [SerializeField]
    private GameObject itemsContainer;

    [SerializeField]
    private SceneItemView itemTemplate;

    private void Start()
    {
        var current = SceneManager.GetActiveScene();
        int count = SceneManager.sceneCountInBuildSettings;
        for (int buildIndex = 0; buildIndex < count; buildIndex++)
        {
            var path = SceneUtility.GetScenePathByBuildIndex(buildIndex);
            if (current.path != path)
            {
                var item = Instantiate(itemTemplate, itemsContainer.transform);
                
                item.BuildIndex = buildIndex;
                item.ScenePath = path;

                Debug.Log($"scene index: {buildIndex}, path: {path}");
            }
        }
    }

    public void LoadScene(int buildIndex)
    {
        if (backTemplate)
        {
            var current = SceneManager.GetActiveScene();
            var backItem = Instantiate(backTemplate);
            backItem.BuildIndex = current.buildIndex;

            DontDestroyOnLoad(backItem);
        }

        SceneManager.LoadScene(buildIndex);
    }
}
