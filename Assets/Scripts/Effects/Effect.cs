using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : MonoBehaviour
{
    public abstract void PlayEffect(Collider2D collider, Collision2D collision);
    public abstract void StopEffect();
}
