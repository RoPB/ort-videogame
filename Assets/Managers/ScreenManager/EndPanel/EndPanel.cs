using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EndPanel : BasePanel
{
    public TMPro.TMP_Text scoreLabel;
    public TMPro.TMP_InputField playerInputField;

    private void Start()
    {
        AttachGameState(GameState.End);
    }

    private void OnDestroy()
    {
        DettachGameState();
    }

    public void OnEnable()
    {
        if (GameManager.Instance != null)
            scoreLabel.text = "" + GameManager.Instance.currentScore;
    }

    public void Save()
    {
        var name = playerInputField.text;
        var score = scoreLabel.text;
        GameManager.Instance.GameEnded(name, score);
    }

    public void DontSave()
    {
        GameManager.Instance.GameEnded(null, scoreLabel.text);
    }
}

