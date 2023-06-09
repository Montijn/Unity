using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComboMeter : MonoBehaviour
{
    public PlayerController player;
    public TMP_Text comboCounter;

    private void Start()
    {
        comboCounter.text = player.getComboCounter().ToString();
    }

    private void Update()
    {
        comboCounter.text = player.getComboCounter().ToString();
    }
}
