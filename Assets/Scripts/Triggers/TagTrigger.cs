using System;
using System.Collections.Generic;
using UnityEngine;

public class TagTrigger : GenericTrigger
{
	private string _tag;

	public TagTrigger(string tag)
	{
		_tag = tag;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals(_tag))
        {
            Execute(collider);
        }
    }
}

