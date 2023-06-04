using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StageDatabase : ScriptableObject
{
    public Stage[] stages;

    public int StageCount
    {
        get
        {
            return stages.Length;
        }
    }

    public Stage GetStage(int index)
    {
        return stages[index];
    }
}
