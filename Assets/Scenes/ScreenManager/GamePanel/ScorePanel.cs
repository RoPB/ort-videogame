using UnityEngine;
using System.Collections;
using TMPro;

public class ScorePanel: MonoBehaviour
{
	public TextMeshProUGUI scoreLabel;
	public TextMeshProUGUI scoreValue;

    private void Awake()
    {
        if (GameManager.Instance.gameState == GameState.Playing)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }

    // Use this for initialization
    private void Start()
	{
        scoreLabel.text = "Score";
    }

    private void Update()
    {
        scoreValue.text = "" + Mathf.Round(GameManager.Instance.currentScore);
    }

}

