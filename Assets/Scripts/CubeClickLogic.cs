using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CubeClickLogic : MonoBehaviour
{
    [SerializeField]
    private GameObject Cube;
    private Transform CubeTransf;
    [SerializeField]
    private Vector3 maxCubeSize;
    private MeshRenderer _cubeMesh;
    [SerializeField]
    private Text CubesScore;
    [SerializeField]
    private Text ScoreMulValue;
    [SerializeField]
    private Text CurrentLvl;
    [SerializeField]
    private Text CubesPerClick;
    [SerializeField]
    private Text UpgCubePerClickPrice;
    [SerializeField]
    private Text UpgExpPerClickPrice;
    [SerializeField]
    private Text LightCount;
    private int _scoreMultiply;
    [SerializeField]
    private Level level;
    [SerializeField]
    private int _expPerClick;

    [SerializeField]
    private float _autoclickerWaitTime = 0.5f;

    [SerializeField]
    private Image BarMask;
    [SerializeField]
    private Image ExpUpgradeArrow;
    [SerializeField]
    private Image CubePerClickUpgradeArrow;




    void Start()
    {
        //Инициализация начальных значений PlayerPrefs
        InitializeStartValues();

        //сброс до начальных значений
        //PlayerPrefs.SetInt("CurrentExp", 1);
        //PlayerPrefs.SetInt("Light", 0);
        //PlayerPrefs.SetInt("ExpPerClick", 1);
        //PlayerPrefs.SetFloat("CubePerClick", 1);
        //PlayerPrefs.SetFloat("Gold", 0);
        //PlayerPrefs.SetFloat("CubePerClickUpgradePrice", 10);
        //PlayerPrefs.SetFloat("ExpUpgradePrice", 10);
        //PlayerPrefs.SetFloat("CubePriceUpgradePrice", 10);
        //PlayerPrefs.SetFloat("Score", 0);
        //PlayerPrefs.SetFloat("SellPrice", 0.3f);

        CubeTransf = Cube.GetComponent<Transform>();
        _cubeMesh = Cube.GetComponent<MeshRenderer>();
        
        level = new Level(1);
        level.Experience = PlayerPrefs.GetInt("CurrentExp");
        level.CurrentLevel = level.GetLevelForXP(level.Experience);
        _expPerClick = PlayerPrefs.GetInt("ExpPerClick");

        ShowStats();
    }

    void Update()
    {
        ShowStats();
        //CubeChange
        CubeTransf.Rotate(new Vector3(9f, 13f, 11.5f) * Time.deltaTime);
        ChangeCubeScoreAndColor();
        //Bar fill
        ProgressBar bar = new ProgressBar(level.Experience,level.GetXPForLevel(level.CurrentLevel), level.GetXPForLevel(level.CurrentLevel + 1), BarMask);
        bar.GetCurrentFill();
        //Check upgrades
        ExpUpgradeArrow.gameObject.SetActive(isUpgradeAvaliable("ExpUpgradePrice"));
        CubePerClickUpgradeArrow.gameObject.SetActive(isUpgradeAvaliable("CubePerClickUpgradePrice"));
        //Autoclicker 
        if (PlayerPrefs.GetString("AutoClicker") == "Yes" && (PlayerPrefs.GetString("CourotineStarted") != "Yes"))
        {
            StartCoroutine("AutoClicker");
            PlayerPrefs.SetString("CourotineStarted", "Yes");
        }
    }
    public void InitializeStartValues()
    {
        if (PlayerPrefs.GetInt("ExpPerClick") == 0)
            PlayerPrefs.SetInt("ExpPerClick", 1);
        if (PlayerPrefs.GetFloat("CubePerClick") == 0)
            PlayerPrefs.SetFloat("CubePerClick", 1);
        if (PlayerPrefs.GetFloat("ExpUpgradePrice") == 0)
            PlayerPrefs.SetFloat("ExpUpgradePrice", 10);
        if (PlayerPrefs.GetFloat("CubePerClickUpgradePrice") == 0)
            PlayerPrefs.SetFloat("CubePerClickUpgradePrice", 10);
        if (PlayerPrefs.GetFloat("CubePriceUpgradePrice") == 0)
            PlayerPrefs.SetFloat("CubePriceUpgradePrice", 10);
        PlayerPrefs.SetString("CourotineStarted", "No");
    }
    private void ShowStats()
    {
        CurrentLvl.text = "LVL: <color=#F3E70D>" + level.CurrentLevel.ToString() + "</color>";
        CubesScore.text = "<size=48>" + Math.Round(PlayerPrefs.GetFloat("Score"), 1) + "</size>";
        CubesPerClick.text = "<size=34>" + Math.Round(PlayerPrefs.GetFloat("CubePerClick"), 1).ToString() + "</size>";
        UpgCubePerClickPrice.text = Math.Round(PlayerPrefs.GetFloat("CubePerClickUpgradePrice")).ToString();
        UpgExpPerClickPrice.text = Math.Round(PlayerPrefs.GetFloat("ExpUpgradePrice")).ToString();
        LightCount.text = "" + PlayerPrefs.GetInt("Light");
        ScoreMulValue.text = "<size=" + (42 + _scoreMultiply * 8) + ">X" + _scoreMultiply + "</size>";
        ScoreMulValue.color = _cubeMesh.material.color;
    }

    private void ChangeCubeScoreAndColor()
    { 
        if (CubeTransf.localScale.x < maxCubeSize.x * 0.55)
        {
            _cubeMesh.material.color = Color.Lerp(_cubeMesh.material.color, Color.red, 3f * Time.deltaTime);
            _scoreMultiply = 1;
        }
        else if (CubeTransf.localScale.x < maxCubeSize.x * 0.75)
        {
            _cubeMesh.material.color = Color.Lerp(_cubeMesh.material.color, Color.green, 3f * Time.deltaTime);
            _scoreMultiply = 2;
        }
        else if (CubeTransf.localScale.x < maxCubeSize.x * 0.9)
        {
            _cubeMesh.material.color = Color.Lerp(_cubeMesh.material.color, Color.cyan, 3f * Time.deltaTime);
            _scoreMultiply = 3;
        }
        else
        {
            _cubeMesh.material.color = Color.Lerp(_cubeMesh.material.color, Color.yellow, 3f * Time.deltaTime);
            _scoreMultiply = 5;
        }

    }

    private void FixedUpdate()
    {
        if (CubeTransf.localScale.x > 2)
            CubeTransf.localScale -= new Vector3(0.02f, 0.02f, 0.02f);
    }

    //Activating autoclicker courotine
    IEnumerator AutoClicker()
    {
        yield return new WaitForSeconds(_autoclickerWaitTime);
        OnMouseDown();
        StartCoroutine("AutoClicker");
        yield return null;
    }

    private void OnMouseDown()
    {
        Cube.GetComponent<AudioSource>().Play();
        if (CubeTransf.localScale.x < maxCubeSize.x && CubeTransf.localScale.y < maxCubeSize.y && CubeTransf.localScale.z < maxCubeSize.z)
            CubeTransf.localScale += new Vector3(0.2f, 0.2f, 0.2f);
        PlayerPrefs.SetFloat("Score", PlayerPrefs.GetFloat("Score") + PlayerPrefs.GetFloat("CubePerClick") * _scoreMultiply);
        if (PlayerPrefs.GetString("CPCForEveryClick") == "Yes")
            PlayerPrefs.SetFloat("CubePerClick", PlayerPrefs.GetFloat("CubePerClick") + 0.01f);
        if (PlayerPrefs.GetString("SPForEveryClick") == "Yes")
            PlayerPrefs.SetFloat("SellPrice", PlayerPrefs.GetFloat("SellPrice") + 0.001f);
        //добавление exp + light при повышении уровня
        AddLight();
    }

    public void AddExp()
    {
        if(PlayerPrefs.GetString("ExpIncrease") == "Yes")
        level.AddExp(PlayerPrefs.GetInt("ExpPerClick"));

        level.AddExp(PlayerPrefs.GetInt("ExpPerClick"));
        PlayerPrefs.SetInt("CurrentExp", level.Experience);
    }

    public void AddLight()
    {
        int LvlBeforeExpAdd = level.CurrentLevel;

        AddExp();

        if (LvlBeforeExpAdd < level.CurrentLevel)
        {
            PlayerPrefs.SetInt("Light", PlayerPrefs.GetInt("Light") + LvlBeforeExpAdd);
            LightCount.text = "" + PlayerPrefs.GetInt("Light");
        }
    }

    private bool isUpgradeAvaliable(string prefName)
    {
        if (PlayerPrefs.GetFloat("Gold") >= PlayerPrefs.GetFloat(prefName))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void EncreaseExpPerClick()
    {
        if (isUpgradeAvaliable("ExpUpgradePrice"))
        {
            _expPerClick++;
            PlayerPrefs.SetInt("ExpPerClick", _expPerClick);
            PlayerPrefs.SetFloat("Gold", PlayerPrefs.GetFloat("Gold") - PlayerPrefs.GetFloat("ExpUpgradePrice"));
            PlayerPrefs.SetFloat("ExpUpgradePrice", PlayerPrefs.GetFloat("ExpUpgradePrice") * 3);
            UpgExpPerClickPrice.text = Math.Round(PlayerPrefs.GetFloat("ExpUpgradePrice")).ToString();
        }
    }

    public void EncreaseCubePerClick()
    {
        if (isUpgradeAvaliable("CubePerClickUpgradePrice"))
        {
            PlayerPrefs.SetFloat("CubePerClick", PlayerPrefs.GetFloat("CubePerClick") * 1.3f);
            PlayerPrefs.SetFloat("Gold", PlayerPrefs.GetFloat("Gold") - PlayerPrefs.GetFloat("CubePerClickUpgradePrice"));
            PlayerPrefs.SetFloat("CubePerClickUpgradePrice", PlayerPrefs.GetFloat("CubePerClickUpgradePrice") * 3);
            UpgCubePerClickPrice.text = Math.Round(PlayerPrefs.GetFloat("CubePerClickUpgradePrice")).ToString();
        }
    }
}
