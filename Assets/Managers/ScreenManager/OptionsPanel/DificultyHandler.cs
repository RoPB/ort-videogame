using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using System;

public class DificultyHandler : MonoBehaviour
{
	public Sprite enableImage;
    public Sprite disabledImage;

    // Use this for initialization
    void Start()
	{
		var initialDifficulty = (int)GameManager.Instance.GetDifficulty();
        var buttons = this.gameObject.GetComponentsInChildren<Button>();
        buttons.ToList().ElementAt(initialDifficulty).image.sprite = enableImage;
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
		var buttons = this.gameObject.GetComponentsInChildren<Button>();
		foreach(var button in buttons)
		{
			button.image.sprite = disabledImage;

        }
        buttonPressed.image.sprite = enableImage;
        var currentIndex = buttons.ToList().IndexOf(buttonPressed);
        GameManager.Instance.SetDifficulty((GameDifficulty)currentIndex);
    }
}

