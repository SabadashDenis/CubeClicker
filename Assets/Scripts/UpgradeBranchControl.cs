using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class UpgradeBranchControl : MonoBehaviour
{
    [SerializeField]
    private GameObject UpgBranch;
    [SerializeField]
    private Text LightCount;
    [SerializeField]
    private GameObject CheckedAutoClicker;
    [SerializeField]
    private GameObject CheckedExpIncrease;
    [SerializeField]
    private GameObject CheckedSellPriceIncrease;
    [SerializeField]
    private GameObject CheckedCubePerClickIncrease;
    [SerializeField]
    private GameObject CheckedCubePerClickForEveryClick;
    [SerializeField]
    private GameObject CheckerSellPriceForEveryClick;
    [SerializeField]
    private Level level;

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
        upgList.Add(new Upgrades("AutoClicker", 50, PlayerPrefs.GetString("AutoClicker") == "Yes" ? true : false));
        upgList.Add(new Upgrades("ExpIncrease", 100, PlayerPrefs.GetString("ExpIncrease") == "Yes" ? true : false));
        upgList.Add(new Upgrades("SellPriceIncrease", 150, PlayerPrefs.GetString("SellPriceIncrease") == "Yes" ? true : false));
        upgList.Add(new Upgrades("CubePerClickIncrease", 200, PlayerPrefs.GetString("CubePerClickIncrease") == "Yes" ? true : false));
        upgList.Add(new Upgrades("CPCForEveryClick", 250, PlayerPrefs.GetString("CPCForEveryClick") == "Yes" ? true : false));
        upgList.Add(new Upgrades("SPForEveryClick", 250, PlayerPrefs.GetString("SPForEveryClick") == "Yes" ? true : false));


        //обнуление апгрейдов
        //PlayerPrefs.SetString("AutoClicker", "No");
        //PlayerPrefs.SetString("ExpIncrease", "No");
        //PlayerPrefs.SetString("SellPriceIncrease", "No");
        //PlayerPrefs.SetString("CubePerClickIncrease", "No");
        //PlayerPrefs.SetString("CPCForEveryClick", "No");
        //PlayerPrefs.SetString("SPForEveryClick", "No");

        //Checked image initialize
        CheckedAutoClicker.SetActive(PlayerPrefs.GetString("AutoClicker") != "Yes" ? false : true);
        CheckedExpIncrease.SetActive(PlayerPrefs.GetString("ExpIncrease") != "Yes" ? false : true);
        CheckedSellPriceIncrease.SetActive(PlayerPrefs.GetString("SellPriceIncrease") != "Yes" ? false : true);
        CheckedCubePerClickIncrease.SetActive(PlayerPrefs.GetString("CubePerClickIncrease") != "Yes" ? false : true);
        CheckedCubePerClickForEveryClick.SetActive(PlayerPrefs.GetString("CPCForEveryClick") != "Yes" ? false : true);
        CheckerSellPriceForEveryClick.SetActive(PlayerPrefs.GetString("SPForEveryClick") != "Yes" ? false : true);

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
            CheckedAutoClicker.SetActive(PlayerPrefs.GetString("AutoClicker") != "Yes" ? false : true);
        }
    }

    public void UpgExpEncomeIncrease()
    {
        if (PlayerPrefs.GetString("ExpIncrease") != "Yes" && PlayerPrefs.GetInt("Light") >= upgList[1].Cost)
        {
            PlayerPrefs.SetString("ExpIncrease", "Yes");
            PlayerPrefs.SetInt("Light", PlayerPrefs.GetInt("Light") - upgList[1].Cost);
            LightCount.text = "" + PlayerPrefs.GetInt("Light");
            CheckedExpIncrease.SetActive(PlayerPrefs.GetString("ExpIncrease") != "Yes" ? false : true);
        }
    }

    public void UpgSellPriceIncrease()
    {
        if (PlayerPrefs.GetString("SellPriceIncrease") != "Yes" && PlayerPrefs.GetInt("Light") >= upgList[2].Cost)
        {
            PlayerPrefs.SetString("SellPriceIncrease", "Yes");
            PlayerPrefs.SetInt("Light", PlayerPrefs.GetInt("Light") - upgList[2].Cost);
            LightCount.text = "" + PlayerPrefs.GetInt("Light");
            CheckedSellPriceIncrease.SetActive(true);

            PlayerPrefs.SetFloat("SellPrice", PlayerPrefs.GetFloat("SellPrice") * 1.5f);
        }
    }

    public void UpgCubePerClickIncrease()
    {
        if (PlayerPrefs.GetString("CubePerClickIncrease") != "Yes" && PlayerPrefs.GetInt("Light") >= upgList[3].Cost)
        {
            PlayerPrefs.SetString("CubePerClickIncrease", "Yes");
            PlayerPrefs.SetInt("Light", PlayerPrefs.GetInt("Light") - upgList[3].Cost);
            LightCount.text = "" + PlayerPrefs.GetInt("Light");
            CheckedCubePerClickIncrease.SetActive(true);

            PlayerPrefs.SetFloat("CubePerClick", PlayerPrefs.GetFloat("CubePerClick") * 2);
        }
    }

    public void UpgCPCForEveryClick()
    {
        if (PlayerPrefs.GetString("CPCForEveryClick") != "Yes" && PlayerPrefs.GetInt("Light") >= upgList[4].Cost)
        {
            PlayerPrefs.SetString("CPCForEveryClick", "Yes");
            PlayerPrefs.SetInt("Light", PlayerPrefs.GetInt("Light") - upgList[4].Cost);
            LightCount.text = "" + PlayerPrefs.GetInt("Light");
            CheckedCubePerClickForEveryClick.SetActive(true);
        }
    }

    public void UpgSPForEveryClick()
    {
        if (PlayerPrefs.GetString("SPForEveryClick") != "Yes" && PlayerPrefs.GetInt("Light") >= upgList[5].Cost)
        {
            PlayerPrefs.SetString("SPForEveryClick", "Yes");
            PlayerPrefs.SetInt("Light", PlayerPrefs.GetInt("Light") - upgList[5].Cost);
            LightCount.text = "" + PlayerPrefs.GetInt("Light");
            CheckerSellPriceForEveryClick.SetActive(true);
        }
    }
    
    public void EndGame()
    {
        Debug.Log(PlayerPrefs.GetInt("Light"));
        Debug.Log(PlayerPrefs.GetInt("Gold"));
        Debug.Log(PlayerPrefs.GetInt("Score"));

        if (PlayerPrefs.GetInt("Light") >= 2500 && PlayerPrefs.GetFloat("Gold") >= 2000000 && PlayerPrefs.GetFloat("Score") >= 500000)
        {
            PlayerPrefs.SetInt("CurrentExp", 1);
            PlayerPrefs.SetInt("Light", 0);
            PlayerPrefs.SetInt("ExpPerClick", 1);
            PlayerPrefs.SetFloat("CubePerClick", 1);
            PlayerPrefs.SetFloat("Gold", 0);
            PlayerPrefs.SetFloat("CubePerClickUpgradePrice", 10);
            PlayerPrefs.SetFloat("ExpUpgradePrice", 10);
            PlayerPrefs.SetFloat("CubePriceUpgradePrice", 10);
            PlayerPrefs.SetFloat("Score", 0);
            PlayerPrefs.SetFloat("SellPrice", 0.3f);
            PlayerPrefs.SetString("AutoClicker", "No");
            PlayerPrefs.SetString("ExpIncrease", "No");
            PlayerPrefs.SetString("SellPriceIncrease", "No");
            PlayerPrefs.SetString("CubePerClickIncrease", "No");
            PlayerPrefs.SetString("CPCForEveryClick", "No");
            PlayerPrefs.SetString("SPForEveryClick", "No");
            PlayerPrefs.SetString("CourotineStarted", "No");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            //CheckedAutoClicker.SetActive(false);
            //CheckedExpIncrease.SetActive(false);
            //CheckedSellPriceIncrease.SetActive(false);
            //CheckedCubePerClickIncrease.SetActive(false);
            //CheckedCubePerClickForEveryClick.SetActive(false);
            //CheckerSellPriceForEveryClick.SetActive(false);
        }

        Debug.Log(PlayerPrefs.GetInt("CurrentExp"));
        Debug.Log(PlayerPrefs.GetInt("Light"));
        Debug.Log(PlayerPrefs.GetInt("ExpPerClick"));
        Debug.Log(PlayerPrefs.GetFloat("CubePerClick"));
        Debug.Log(PlayerPrefs.GetFloat("Gold"));
        Debug.Log(PlayerPrefs.GetFloat("CubePerClickUpgradePrice"));
        Debug.Log(PlayerPrefs.GetFloat("ExpUpgradePrice"));
        Debug.Log(PlayerPrefs.GetFloat("CubePriceUpgradePrice"));
        Debug.Log(PlayerPrefs.GetFloat("Score"));
        Debug.Log(PlayerPrefs.GetFloat("SellPrice"));
        Debug.Log(PlayerPrefs.GetString("AutoClicker"));
        Debug.Log(PlayerPrefs.GetString("ExpIncrease"));
        Debug.Log(PlayerPrefs.GetString("SellPriceIncrease"));
        Debug.Log(PlayerPrefs.GetString("CubePerClickIncrease"));
        Debug.Log(PlayerPrefs.GetString("CPCForEveryClick"));
        Debug.Log(PlayerPrefs.GetString("SPForEveryClick"));
    }
}
