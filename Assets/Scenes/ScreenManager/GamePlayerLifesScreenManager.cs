using System;
using TMPro;
using UnityEngine;

public class GamePlayerLifesScreenManager : MonoBehaviour
{
    public TextMeshProUGUI playerLifesLabel;
    public TextMeshProUGUI playerLifesValue;

    // Use this for initialization
    private void Start()
    {
        playerLifesLabel.text = "Lifes";
        GameManager.Instance.PlayerLifesChanged += GameManager_PlayerLifesChanged;
    }

    private void GameManager_PlayerLifesChanged(object sender, PlayerLifes e)
    {
        playerLifesValue.text = "X" + GameManager.Instance.playerLifes.currentLifes;
    }
}

