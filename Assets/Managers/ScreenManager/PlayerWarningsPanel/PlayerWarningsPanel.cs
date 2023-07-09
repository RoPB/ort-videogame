using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using Unity.VisualScripting;

public class PlayerWarningsPanel : BasePanel
{
    public TMPro.TMP_Text msgLabel;
    public TMPro.TMP_Text descriptionLabel;

    private void Start()
    {
        AttachGameState(GameState.PlayingPlayerWarnings);
    }


    private void OnEnable()
    {
        if (GameManager.Instance != null)
        {
            msgLabel.text = GameManager.Instance.playerWarnMsg;
            descriptionLabel.text = GameManager.Instance.playerWarnMsgDescription;
        }
        
    }

    private void OnDisabled()
    {
        msgLabel.text = string.Empty;
        descriptionLabel.text = string.Empty;
    }

    private void OnDestroy()
    {
        DettachGameState();
    }

}

