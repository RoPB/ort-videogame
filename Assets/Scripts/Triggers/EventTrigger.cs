using UnityEngine;
using System.Collections;

public class EventTrigger : GenericTrigger
{
	void OnEnable()
	{
		Execute(null);	
	}
}

