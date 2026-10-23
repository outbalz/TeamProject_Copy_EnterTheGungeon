using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;


public class GunUIControoller : MonoBehaviour
{
    #region
    [SerializeField] private GameObject _crosshair;
    [SerializeField] private TextMeshProUGUI _ammoText;
    #endregion


   

    private void Start()
    {
        SetCrosshairAcrive(true);
    }

    // 크로스헤어 
    public void SetCrosshairAcrive(bool Active)
    {
        if (_crosshair != null)
        {
            _crosshair.SetActive(Active);
        }
    }

    // 탄약 UI
    public void AmmoUI(int currentAmmo, int maxAmmo, int totalAmmo)
    {
        if (_ammoText != null)
        {
            _ammoText.text = $"{currentAmmo} / {totalAmmo}";
        }

    }

}
