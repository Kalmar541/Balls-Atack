using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLine : MonoBehaviour
{
    // Start is called before the first frame update
    static public ProjectileLine S; // одиночка
    public float minDist = 0.1f;

    private LineRenderer line;
    private GameObject _poi;
    private List<Vector3> points;
    public GameObject poi
    {
        get
        {
            return (_poi);
        }
        set
        {
            _poi = value;
            if (_poi!=null)
            {
                line.enabled = false;
                points = new List<Vector3>();
                AddPoint();
            }
        }
    }
    private void Awake()
    {
        S = this;
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        points = new List<Vector3>();
    }
    public void Clear()
    {
        _poi = null;
        line.enabled = false;
        points = new List<Vector3>();
    }
    public void AddPoint()
    {
        Vector3 pt = _poi.transform.position;
        if (points.Count>0&&(pt-lastPoint).magnitude<minDist)
        {
            return; // если точка не достаточно далеко от предыдущей то выйти
        }
        if (points.Count == 0) // если это точка запуска
        {
            Vector3 launchPosDiff = pt - Slingshot.LAUNCH_POS;
            points.Add(pt+ launchPosDiff);
            points.Add(pt);
            line.positionCount = 2;
            //установить первые две точки
           
            line.SetPosition(0, points[0]);
            line.SetPosition(1, points[1]);
            line.enabled=true;
        }
        else // обычное добавление точек
        {
            points.Add(pt);
            line.positionCount = points.Count;
           
            line.SetPosition(points.Count - 1, lastPoint);
            line.enabled = true;

        }
    }
    public Vector3 lastPoint // возращает местополжение последней точки
    {
        get
        {
            if (points==null)
            {
                return (Vector3.zero);
            }
            return (points[points.Count - 1]);
        }
       
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (poi==null)
            //если poi пустой то нужно анйти его
        {
            if (FollowCam.POI!=null)
            {
                if (FollowCam.POI.CompareTag("Projectile"))
                {
                    poi = FollowCam.POI;
                }
                else return; // выйти если ничего не найдено
            }
        }// если найден
        AddPoint();
        if (FollowCam.POI == null)
        {
            //Если FollowCam.POI содержит null, записать nulll в poi
            poi = null;
        }
    }
}
