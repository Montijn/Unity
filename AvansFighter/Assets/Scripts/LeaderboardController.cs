using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardController : MonoBehaviour
{
    public TMP_Text highscore;
    void Start()
    {
        highscore.text = PlayerPrefs.GetInt("highscore").ToString();
    }

}
