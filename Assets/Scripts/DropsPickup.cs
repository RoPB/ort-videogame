using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropsPickup : MonoBehaviour
{
    [SerializeField]
    [Range(0, 100)]
    private int dropChance = 10;

    [SerializeField]
    [Range(0, 10)]
    private int healthWeight = 0;
    public GameObject healthPickup;
    [SerializeField]
    [Range(0, 10)]
    private int shieldWeight = 0;
    public GameObject shieldPickup;
    [SerializeField]
    [Range(0, 10)]
    private int extraWeaponWeight = 0;
    public GameObject extraWeaponPickup;
    [SerializeField]
    [Range(0, 10)]
    private int attackSpeedWeight = 0;
    public GameObject attackSpeedPickup;
    [SerializeField]
    [Range(0, 10)]
    private int missileGunWeight = 0;
    public GameObject missileGunPickup;

    private GameManager _gameManager;

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        if (Random.Range(0, 100) < dropChance)
        {
            var totalWeight = healthWeight + shieldWeight + extraWeaponWeight + attackSpeedWeight;
            var random = Random.Range(0, totalWeight);
            if (random < healthWeight)
            {
                Instantiate(healthPickup, transform.position, Quaternion.identity);
                return;
            }
            random -= healthWeight;
            if (random < shieldWeight)
            {
                Instantiate(shieldPickup, transform.position, Quaternion.identity);
                return;
            }
            random -= shieldWeight;
            if (random < extraWeaponWeight)
            {
                Instantiate(extraWeaponPickup, transform.position, Quaternion.identity);
                return;
            }
            random -= extraWeaponWeight;
            if (random < attackSpeedWeight)
            {
                Instantiate(attackSpeedPickup, transform.position, Quaternion.identity);
                return;
            }

            Instantiate(missileGunPickup, transform.position, Quaternion.identity);
        }
    }

    public void DropExtraWeaponPickup()
    {
        Debug.Log("DROPEA ARMA");
        if (_gameManager == null)
            _gameManager = GameManager.Instance;
        //var x = (_gameManager.GetSceneMaxX() - _gameManager.GetSceneMinX()) / 2f;//(Esto debiera funcionar)
        var x = 0;
        Instantiate(extraWeaponPickup, new Vector2(x, _gameManager.GetSceneMaxY()), Quaternion.identity);
    }
}
