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

    public Vector3 castlePos; // ���������� ��� ��������� �����
    public GameObject[] castles; //��� �����

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
    {// ������ ���������� �����
        if (castle!=null)
        {
            Destroy(castle);
            Goal.goalMet = false;
        }
        //������ �������
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject pTemp in gos)
        {
            Destroy(pTemp);
        }
        //������� ����� �����
        castle = Instantiate(castles[level]);
        castle.transform.position = castlePos;
        shotsTaken = 0;
        mode = GameMode.playing;

        //���������� ������ � ��������� �������

    }

    // Update is called once per frame
    void Update()
    {
        UpdateGUI();
        if ((mode == GameMode.playing)&& Goal.goalMet)
        {
            mode = GameMode.levelEnd;
            SwitchView("Show Both");
            // ������ ����� ������� ����� 2 �������
            Invoke("NextLevel", 2f);
            
        }
    }
    void UpdateGUI()
    {
        // �������� ������ � ��������� ��
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
    // �������������� ����� ����������� ��������� �� ������ ����
    public static void ShotFired()
    {
        S.shotsTaken++;
    }
}
