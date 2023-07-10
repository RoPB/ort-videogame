using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMisions : MonoBehaviour
{
	public List<PlayerMision> playerMisions;

	public PlayerMision GetByIndex(int index)
	{
		return index < playerMisions.Count ? playerMisions[index]: null;
    }

    public void StopPlayerMisions()
    {
        foreach(var pm in playerMisions)
        {
            pm.enemies.Stop();
        }
        
    }
}

