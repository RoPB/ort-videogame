using System;
using UnityEngine;

public abstract class IReaction : MonoBehaviour
{
    public abstract void React(Collider2D collision);
}

