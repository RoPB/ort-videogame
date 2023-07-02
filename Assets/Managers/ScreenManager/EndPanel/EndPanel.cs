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
        scoreLabel.text = ""+GameManager.Instance.currentScore;
    }

    public void Save()
    {
        GameManager.Instance.GameEnded(playerInputField.text ?? System.Guid.NewGuid().ToString(), scoreLabel.text);
    }

    public void DontSave()
    {
        GameManager.Instance.GameEnded(null, scoreLabel.text);
    }
}

