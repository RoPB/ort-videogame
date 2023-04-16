using System;
using TMPro;
using UnityEngine;

public class PlayerLifesPanel : MonoBehaviour
{
    public TextMeshProUGUI playerLifesLabel;
    public TextMeshProUGUI playerLifesValue;

    // Use this for initialization
    private void Start()
    {
        if (GameManager.Instance.gameState == GameState.Playing)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);

        playerLifesLabel.text = "Lifes";
        GameManager.Instance.PlayerLifesChanged += GameManager_PlayerLifesChanged;
    }

    private void GameManager_PlayerLifesChanged(object sender, PlayerLifes e)
    {
        playerLifesValue.text = "X" + GameManager.Instance.playerLifes.currentLifes;
    }

    private void OnDestroy()
    {
        GameManager.Instance.PlayerLifesChanged -= GameManager_PlayerLifesChanged;
    }
}

