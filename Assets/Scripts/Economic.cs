﻿using System;
using UnityEngine;
using UnityEngine.UI;

public class Economic : MonoBehaviour
{
    public Text Gold;
    [SerializeField]
    private float _sellPriceIncrease = 1.2f;
    public Text CubePrice;
    public Text UpgradePrice;
    public Image UpgradeArrow;

    void Start()
    {
        if (PlayerPrefs.GetFloat("CubePriceUpgradePrice") == 0)
        PlayerPrefs.SetFloat("CubePriceUpgradePrice", 10);

        Gold.text = "<size=48>" + PlayerPrefs.GetFloat("Gold").ToString() + "</size>";
        if (PlayerPrefs.GetFloat("SellPrice") == 0)
            PlayerPrefs.SetFloat("SellPrice", 0.3f);
        CubePrice.text = "<size=34>" + Math.Round(PlayerPrefs.GetFloat("SellPrice"), 2).ToString() + "</size>";
        UpgradePrice.text = Math.Round(PlayerPrefs.GetFloat("CubePriceUpgradePrice")).ToString();
    }

    void Update()
    {
        Gold.text = "<size=48>" + Math.Round(PlayerPrefs.GetFloat("Gold"), 1).ToString() + "</size>";
        CubePrice.text = "<size=34>" + Math.Round(PlayerPrefs.GetFloat("SellPrice"), 2).ToString() + "</size>";
        UpgradePrice.text = Math.Round(PlayerPrefs.GetFloat("CubePriceUpgradePrice")).ToString();
        UpgradeArrow.gameObject.SetActive(isSellPriceUpgAvaliable());
    }

    private bool isSellPriceUpgAvaliable()
    {
        if (PlayerPrefs.GetFloat("Gold") >= PlayerPrefs.GetFloat("CubePriceUpgradePrice"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SellCube()
    {
        PlayerPrefs.SetFloat("Gold", PlayerPrefs.GetFloat("Gold") + PlayerPrefs.GetFloat("Score") * PlayerPrefs.GetFloat("SellPrice"));
        PlayerPrefs.SetFloat("Score", 0);
    }

    public void IncreaseSellPrice()
    {
        if (isSellPriceUpgAvaliable())
        {
            PlayerPrefs.SetFloat("SellPrice", PlayerPrefs.GetFloat("SellPrice") * _sellPriceIncrease);
            _sellPriceIncrease += 0.02f;
            CubePrice.text = "<size=34>" + Math.Round(PlayerPrefs.GetFloat("SellPrice"), 2).ToString() + "</size>";
            PlayerPrefs.SetFloat("Gold", PlayerPrefs.GetFloat("Gold") - PlayerPrefs.GetFloat("CubePriceUpgradePrice"));
            PlayerPrefs.SetFloat("CubePriceUpgradePrice", PlayerPrefs.GetFloat("CubePriceUpgradePrice") * 3);
        }
    }

}
