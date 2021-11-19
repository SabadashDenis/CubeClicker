using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using System;

public class UpgradeBranchControl : MonoBehaviour
{
    [SerializeField]
    private GameObject UpgBranch;
    [SerializeField]
    private Text LightCount;
    [SerializeField]
    private GameObject CheckedAutoClicker;

    class Upgrades
    {
        private string name;
        public string Name
        {
            get { return name; }
        }
        private int cost;
        public int Cost
        {
            get { return cost; }
        }
        private bool isUpgraded;
        public bool IsUpgraded
        {
            get { return isUpgraded; }
            set { isUpgraded = value; }
        }

        public Upgrades(string _name, int _cost, bool _isUpgraded)
        {
            name = _name;
            cost = _cost;
            isUpgraded = _isUpgraded;
        }
    }

    List<Upgrades> upgList = new List<Upgrades>();
    private void Start()
    {
        //Инициализация апгрейдов
        upgList.Add(new Upgrades("AutoClicker", 50, Convert.ToBoolean(PlayerPrefs.GetInt("AutoClicker"))));


        //обнуление апгрейдов
        PlayerPrefs.SetString("AutoClicker", "No");
    }

    public void ShowUpgBranch()
    {
        UpgBranch.SetActive(true);
    }
    public void CloseUpgBranch()
    {
        UpgBranch.SetActive(false);
    }
    public void UpgAutoClicker()
    {
        if (PlayerPrefs.GetString("AutoClicker") != "Yes" && PlayerPrefs.GetInt("Light") >= upgList[0].Cost)
        {
            PlayerPrefs.SetString("AutoClicker", "Yes");
            PlayerPrefs.SetInt("Light", PlayerPrefs.GetInt("Light") - upgList[0].Cost);
            LightCount.text = "" + PlayerPrefs.GetInt("Light");
            CheckedAutoClicker.SetActive(true);
        }
    }
}
