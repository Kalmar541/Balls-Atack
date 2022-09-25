using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionDemolition : MonoBehaviour
{
    public enum GameMode
    {
        idle,
        playing,
        levelEnd
    }
    static private MissionDemolition S;

    public TMPro.TextMeshProUGUI uitLevel;
    public TMPro.TextMeshProUGUI uitShots;
    public TMPro.TextMeshProUGUI uitButton;

    public Vector3 castlePos; // координаты для установки замка
    public GameObject[] castles; //все замки

    public int level;
    public int maxLevel;
    public int shotsTaken;

    public GameObject castle;
    public GameMode mode = GameMode.idle;

    public string showing = "Show Slingshot";
    // Start is called before the first frame update
    void Start()
    {
        S = this;
        level = 0;
        maxLevel = castles.Length;
        StartLevel();
    }
    void StartLevel()
    {// убрать предыдущий замок
        if (castle!=null)
        {
            Destroy(castle);
            Goal.goalMet = false;
        }
        //убрать снаряды
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject pTemp in gos)
        {
            Destroy(pTemp);
        }
        //создать новый замок
        castle = Instantiate(castles[level]);
        castle.transform.position = castlePos;
        shotsTaken = 0;
        mode = GameMode.playing;

        //установить камеру в начальную позицию

    }

    // Update is called once per frame
    void Update()
    {
        UpdateGUI();
        if ((mode == GameMode.playing)&& Goal.goalMet)
        {
            mode = GameMode.levelEnd;
            SwitchView("Show Both");
            // Начать новый уровень через 2 секунды
            Invoke("NextLevel", 2f);
            
        }
    }
    void UpdateGUI()
    {
        // Показать данные в элементах ПИ
        uitLevel.text = "Level: " + (level + 1) + " of " + maxLevel;
        uitShots.text = "Shots Taken: " + shotsTaken; }

    void NextLevel()
    {
        level++;
        if (level==maxLevel)
        {
            level = 0;
        }
        StartLevel();
    }
    public void SwitchView(string eView = "")
    {
        if (eView == "")
        {
            eView = uitButton.text;
        }
        showing = eView;
        switch (showing)
        {
            case "Show Slingshot":
                FollowCam.POI = null;
                uitButton.text = "Show Castle";
                break;
            case "Show Castle":
                FollowCam.POI = S.castle;
                uitButton.text = "Show Both";
                break;
            case "Show Both":
                FollowCam.POI = GameObject.Find("ViewBoth");
                uitButton.text = "Show Slingshot";
                break;
            default:
                break;
        }
    }
    // статистический метод позволяющий увеличить из любого кода
    public static void ShotFired()
    {
        S.shotsTaken++;
    }
}
