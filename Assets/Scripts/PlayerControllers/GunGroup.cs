using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunGroup : MonoBehaviour
{
    private KeyCode shootKey = KeyCode.Space;
    [SerializeField]
    [Range(0, 1)]
    private float attackSpeed = 0.5f;
    private float _lastAttackTime = 0;
    public List<GameObject> guns;
    private int _numberOfGuns = 1;

    public WeaponType weaponType = WeaponType.Laser;

    public bool initialWeapon = false;
    private bool _selected = false;
    public bool Selected
    {
        set
        {
            _selected = value;
            if (_selected)
            {
                SetNumberOfGuns(_numberOfGuns);
            }
            else
            {
                SetNumberOfGuns(0);
            }
        }
    }

    public bool CanShoot => _selected && Time.time - _lastAttackTime > attackSpeed;

    // Start is called before the first frame update
    void InitGunGroup()
    {
        _numberOfGuns = 1;
        Selected = initialWeapon;
    }

    void IncreaseNumberOfGuns()
    {
        if (_numberOfGuns < 3)
        {
            _numberOfGuns++;
            SetNumberOfGuns(_numberOfGuns);
        }
    }

    void DecreaseNumberOfGuns()
    {
        if (_numberOfGuns > 1)
        {
            _numberOfGuns--;
            SetNumberOfGuns(_numberOfGuns);
        }
    }

    void ChangeGun(WeaponType weaponType)
    {
        Selected = this.weaponType == weaponType;
    }

    void SetNumberOfGuns(int numberOfGuns)
    {
        if (!_selected) numberOfGuns = 0;
        switch (numberOfGuns)
        {
            case 1:
                guns[0].SetActive(false);
                guns[1].SetActive(true);
                guns[2].SetActive(false);
                break;
            case 2:
                guns[0].SetActive(true);
                guns[1].SetActive(false);
                guns[2].SetActive(true);
                break;
            case 3:
                guns[0].SetActive(true);
                guns[1].SetActive(true);
                guns[2].SetActive(true);
                break;
            default:
                guns[0].SetActive(false);
                guns[1].SetActive(false);
                guns[2].SetActive(false);
                break;
        }
    }

    void Update()
    {
        if (!_selected) return;

        if (Input.GetKey(shootKey) && CanShoot)
        {
            _lastAttackTime = Time.time;
            var aSrc = GetComponent<AudioSource>();
            if (aSrc != null)
            {
                aSrc.pitch = Random.Range(0.8f, 1.2f);
                aSrc.Play();
            }
            BroadcastMessage("Shoot", null, SendMessageOptions.DontRequireReceiver);
        }
    }
}
