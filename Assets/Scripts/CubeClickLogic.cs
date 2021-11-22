using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CubeClickLogic : MonoBehaviour
{
    public GameObject Cube;
    private Transform CubeTransf;
    [SerializeField]
    private Vector3 maxCubeSize = new Vector3(10f, 10f, 10f);
    private MeshRenderer _cubeMesh;
    [SerializeField]
    public Text CubesScore;
    public Text ScoreMulValue;
    public Text CurrentLvl;
    public Text CubesPerClick;
    public Text UpgCubePerClickPrice;
    public Text UpgExpPerClickPrice;
    [SerializeField]
    private Text LightCount;
    private int _scoreMultiply;
    public Level level;
    [SerializeField]
    private int _expPerClick;

    private float _autoclickerWaitTime = 0.5f;

    public Image BarMask;
    public Image ExpUpgradeArrow;
    public Image CubePerClickUpgradeArrow;




    void Start()
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

        level = new Level(1);
        level.experience = PlayerPrefs.GetInt("CurrentExp");
        level.currentLevel = level.GetLevelForXP(level.experience);
        _expPerClick = PlayerPrefs.GetInt("ExpPerClick");
        CurrentLvl.text = "LVL: <color=#F3E70D>" + level.currentLevel.ToString() + "</color>";
        CubeTransf = Cube.GetComponent<Transform>();
        _cubeMesh = Cube.GetComponent<MeshRenderer>();
        CubesScore.text = "<size=48>" + Math.Round(PlayerPrefs.GetFloat("Score"),1) + "</size>";
        CubesPerClick.text = "<size=34>" + Math.Round(PlayerPrefs.GetFloat("CubePerClick"), 1).ToString() + "</size>";
        UpgCubePerClickPrice.text = Math.Round(PlayerPrefs.GetFloat("CubePerClickUpgradePrice")).ToString();
        UpgExpPerClickPrice.text = Math.Round(PlayerPrefs.GetFloat("ExpUpgradePrice")).ToString();
        LightCount.text = "" + PlayerPrefs.GetInt("Light");
    }


    void Update()
    {
        CubesScore.text = "<size=48>" + Math.Round(PlayerPrefs.GetFloat("Score"),1) + "</size>";
        CubesPerClick.text = "<size=34>" + Math.Round(PlayerPrefs.GetFloat("CubePerClick"), 1).ToString() + "</size>";
        LightCount.text = "" + PlayerPrefs.GetInt("Light");
        UpgCubePerClickPrice.text = Math.Round(PlayerPrefs.GetFloat("CubePerClickUpgradePrice")).ToString();
        UpgExpPerClickPrice.text = Math.Round(PlayerPrefs.GetFloat("ExpUpgradePrice")).ToString();
        CurrentLvl.text = "LVL: <color=#F3E70D>" + level.currentLevel.ToString() + "</color>";
        ScoreMulValue.color = _cubeMesh.material.color;
        ScoreMulValue.text = "<size=" + (42 + _scoreMultiply * 8) + ">X" + _scoreMultiply + "</size>";
        CubeTransf.Rotate(new Vector3(9f, 13f, 11.5f) * Time.deltaTime);
        ChangeCubeScoreAndColor();
        ProgressBar bar = new ProgressBar(level.experience,level.GetXPForLevel(level.currentLevel), level.GetXPForLevel(level.currentLevel + 1), BarMask);
        bar.GetCurrentFill();
        ExpUpgradeArrow.gameObject.SetActive(isUpgradeAvaliable("ExpUpgradePrice"));
        CubePerClickUpgradeArrow.gameObject.SetActive(isUpgradeAvaliable("CubePerClickUpgradePrice"));

        if (PlayerPrefs.GetString("AutoClicker") == "Yes" && (PlayerPrefs.GetString("CourotineStarted") != "Yes"))
        {
            StartCoroutine("AutoClicker");
            PlayerPrefs.SetString("CourotineStarted", "Yes");
        }
    }

    private void ChangeCubeScoreAndColor()
    { 
        if (CubeTransf.localScale.x < 2.3)
        {
            _cubeMesh.material.color = Color.Lerp(_cubeMesh.material.color, Color.red, 3f * Time.deltaTime);
            _scoreMultiply = 1;
        }
        else if (CubeTransf.localScale.x < 3.3)
        {
            _cubeMesh.material.color = Color.Lerp(_cubeMesh.material.color, Color.green, 3f * Time.deltaTime);
            _scoreMultiply = 2;
        }
        else if (CubeTransf.localScale.x < 4.5)
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
        PlayerPrefs.SetInt("CurrentExp", level.experience);
    }

    public void AddLight()
    {
        int LvlBeforeExpAdd = level.currentLevel;

        AddExp();

        if (LvlBeforeExpAdd < level.currentLevel)
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
