using UnityEngine;
using System.Collections;
using TMPro;

public class GameLevelScreenManager : MonoBehaviour
{
    public TextMeshProUGUI levelLabel;
    public TextMeshProUGUI levelValue;
    public GameObject progressPlaceHolderBar;
    public GameObject progressBar;

    // Use this for initialization
    // Use this for initialization
    private void Start()
    {
        levelLabel.text = "Level";
        progressBar.transform.localScale = new Vector3(0, progressBar.transform.localScale.y, progressBar.transform.localScale.z);
    }

    private void Update()
    {
        levelValue.text = "" + GameManager.Instance.currentLevel;
        var progressBarXScale = progressPlaceHolderBar.transform.localScale.x * GameManager.Instance.levelProgress;
        Debug.Log(progressBarXScale);
        progressBar.transform.localScale = new Vector3(progressBarXScale, progressBar.transform.localScale.y,0);
    }
}

