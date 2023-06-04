using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public StageDatabase stageDB;

    public Text nameText;
    public SpriteRenderer stageSprite;

    private int selectedOption = 0;
    // Start is called before the first frame update
    void Start()
    {
        UpdateStage(selectedOption);
    }

    public void NextOption()
    {
        selectedOption++;

        if(selectedOption >= stageDB.StageCount)
        {
            selectedOption = 0;
        }

        UpdateStage(selectedOption);
    }

    public void PreviousOption()
    {
        selectedOption--;

        if(selectedOption < 0)
        {
            selectedOption = stageDB.StageCount - 1;
        }

        UpdateStage(selectedOption);
    }

    private void UpdateStage(int selectedOption)
    {
        Stage stage = stageDB.GetStage(selectedOption);
        stageSprite.sprite = stage.stageSprite;
        nameText.text = stage.stageName;
    }
}
