using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    [SerializeField]
    public Stage selectedStage;

    public void OpenScene()
    {
        SceneManager.LoadScene(selectedStage.stageName);
    }
}
