using UnityEngine;
using System.Collections;
using TMPro;

public class LevelPanel : MonoBehaviour
{
    public TextMeshProUGUI levelLabel;
    public TextMeshProUGUI levelValue;
    public GameObject progressPlaceHolderBar;
    public GameObject progressBar;

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
        levelLabel.text = "Level";
        progressBar.transform.localScale = new Vector3(0, progressBar.transform.localScale.y, progressBar.transform.localScale.z);
    }

    private void Update()
    {
        levelValue.text = "" + GameManager.Instance.currentLevel;
        var progressBarXScale = progressPlaceHolderBar.transform.localScale.x * GameManager.Instance.levelProgress;
        progressBar.transform.localScale = new Vector3(progressBarXScale, progressBar.transform.localScale.y,0);
    }
}

