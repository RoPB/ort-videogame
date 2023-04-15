using UnityEngine;
using System.Collections;
using TMPro;

public class GameScoreScreenManager: MonoBehaviour
{

	public TextMeshProUGUI scoreLabel;
	public TextMeshProUGUI scoreValue;

    // Use this for initialization
    private void Start()
	{
		scoreLabel.text = "Score";
    }

    private void Update()
    {
        scoreValue.text = "" + GameManager.Instance.getCurrentScore();
    }

}

