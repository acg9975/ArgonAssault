﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    int score;

    TMP_Text scoreText;

    void Start()
    {
        scoreText = GetComponent<TMP_Text>();
        scoreText.text = "0";
    }

    public void increaseScore(int amountToIncrease) {
        score += amountToIncrease;
        Debug.Log($"Score is now: {score}");
        scoreText.text = score.ToString();
    }
}
