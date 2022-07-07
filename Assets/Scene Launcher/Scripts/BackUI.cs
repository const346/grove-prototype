using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackUI : MonoBehaviour
{
    public int BuildIndex { get; set; }

    public void LoadScene()
    {
        SceneManager.LoadScene(BuildIndex);
        Destroy(gameObject);
    }
}
