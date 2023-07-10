using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunGroup : MonoBehaviour
{
    public List<GameObject> guns;
    private int _numberOfGuns = 1;
    private bool Selected
    {
        get
        {
            return Selected;
        }
        set
        {
            Selected = value;
            if (Selected)
            {
                SetNumberOfGuns(_numberOfGuns);
            }
            else
            {
                SetNumberOfGuns(0);
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        SetNumberOfGuns(_numberOfGuns);
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

    void SetNumberOfGuns(int numberOfGuns)
    {
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
}
