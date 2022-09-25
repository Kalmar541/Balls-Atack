using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    static public Slingshot S;
    public GameObject lanchPoint;
    public GameObject prebafProjectil;
    public GameObject projectile;

    public Vector3 launchPos;

    public bool aimingMode;

    public float velocityMult = 8f;

    private Rigidbody projectileRigitBody;
    static public Vector3 LAUNCH_POS
    {
        get
        {
            if (S==null)
            {
                return Vector3.zero;
            }
            return S.launchPos;
        }
    }
    // Start is called before the first frame update
    private void Awake()
    {
        Transform launchPointTrans = transform.Find("LaunchPoint"); //transform.Find поиск по дочерним обьектам
        lanchPoint = launchPointTrans.gameObject;
        lanchPoint.SetActive(false);
        launchPos = launchPointTrans.position; // центр рогатки
        S = this;

    }
    void Start()

    {
        
    }

    // Update is called once per frame
    //public Vector3 mousePos2DP;
    //public Vector3 mausePos3DP;
    void Update()
    {
        if (!aimingMode)
        {
            return;
        }
         Vector3 mousePos2D = Input.mousePosition;// mousePos2DP = mousePos2D;


        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mausePos3D = Camera.main.ScreenToWorldPoint(mousePos2D); //mausePos3DP = mausePos3D;


        Vector3 mousDelta = mausePos3D - launchPos;
        float maxMagnituda = this.GetComponent<SphereCollider>().radius;

        if (mousDelta.magnitude> maxMagnituda)
        {
            mousDelta.Normalize();
            mousDelta*= maxMagnituda;
        }
        //передвигание снаряда
        Vector3 projPos = launchPos + mousDelta;
        projectile.transform.position = projPos;
        if (Input.GetMouseButtonUp(0))
        {
            aimingMode = false;
            projectileRigitBody.isKinematic = false;
            projectileRigitBody.velocity = -mousDelta * velocityMult;
            FollowCam.POI = projectile;
            projectile = null;
            MissionDemolition.ShotFired();
            ProjectileLine.S.poi = projectile;
        }
    }
    private void OnMouseEnter()
    {
        lanchPoint.SetActive(true);
    }
    private void OnMouseExit()
    {
        lanchPoint.SetActive(false);
    }
    private void OnMouseDown()
    {
        aimingMode = true;
        projectile = Instantiate(prebafProjectil) ;
        projectile.transform.position = launchPos;
        projectileRigitBody = projectile.GetComponent<Rigidbody>();
        projectile.GetComponent<Rigidbody>().isKinematic = true;
    }
}
