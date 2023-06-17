using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffect : Effect
{
    //private Sprite _sprite;

    //void Start()
    //{
    //  _sprite = this.gameObject.GetComponent<Sprite>();
    //}

    public override void PlayEffect()
    {
        gameObject?.SetActive(true);
    }

    public override void StopEffect()
    {
        gameObject?.SetActive(false);
    }
}
