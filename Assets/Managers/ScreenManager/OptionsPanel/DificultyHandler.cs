using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using System;

public class DificultyHandler : MonoBehaviour
{
    public GameObject warnLabelGameObject;
	public Sprite enableImage;
    public Sprite disabledImage;

    // Use this for initialization
    void Start()
	{
        Initialize();
        var initialDifficulty = (int)GameManager.Instance.GetDifficulty();
        var buttons = this.gameObject.GetComponentsInChildren<Button>();
        buttons.ToList().ElementAt(initialDifficulty).image.sprite = enableImage;
        
    }

    private void OnEnable()
    {
        if(GameManager.Instance!=null)
            warnLabelGameObject.SetActive(GameManager.Instance.gameState == GameState.PlayingOptions);
    }

    private void Initialize()
    {
        var buttons = this.gameObject.GetComponentsInChildren<Button>();
        foreach (var button in buttons)
        {
            button.image.sprite = disabledImage;

        }
    }

    public void ToggleDifficulty(Button buttonPressed)
	{
        if (GameManager.Instance.gameState == GameState.Options)
        {
            var buttons = this.gameObject.GetComponentsInChildren<Button>();
            foreach (var button in buttons)
            {
                button.image.sprite = disabledImage;

            }
            buttonPressed.image.sprite = enableImage;
            var currentIndex = buttons.ToList().IndexOf(buttonPressed);
            GameManager.Instance.SetDifficulty((GameDifficulty)currentIndex);
        }
		
    }
}

