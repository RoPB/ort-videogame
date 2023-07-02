using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableButton : Disable
{
    public Sprite imageWhenDisable;
    private Sprite _spriteBeforeDisable;
    private Color _textColorBeforeDisable;
    private Button _button;
    private Image _image;
    private TMPro.TMP_Text _text;

    public void Start()
    {
        _button = GetComponent<Button>();
        _image = GetComponentInChildren<Image>();
        _text = GetComponentInChildren<TMPro.TMP_Text>();
        _spriteBeforeDisable = _image.sprite;
        _textColorBeforeDisable = _text.color;
    }

    public override void OnDisabled(bool enabled)
    {
        _button.enabled = enabled;
        _image.sprite = enabled ? _spriteBeforeDisable : imageWhenDisable;
        _text.color = enabled ? _textColorBeforeDisable : Color.gray;
    }
}
