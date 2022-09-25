using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    static public GameObject POI; // обьект слежки
    public float easing = 0.05f;
    public Vector2 minXY = Vector2.zero;

    public float camZ; // координата камеры
    // Start is called before the first frame update
    private void Awake()
    {
        camZ = Camera.main.transform.position.z;
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
        Vector3 destination;
        if (POI == null)
        {
            destination = Vector3.zero;
        }
        else
        {
            destination = POI.transform.position;
            if (POI.CompareTag("Projectile"))
            {
                if (POI.GetComponent<Rigidbody>().IsSleeping())
                {
                    POI = null;
                    return;
                }
            }
        }
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);
        // Lerp это среднее между двумя точками, но зависит от того к какой точке ближе его нужно выбрать
        destination = Vector3.Lerp(transform.position, destination, easing); // определить точку между текущем положении камеры и destination
        destination.z = camZ;
        transform.position = destination;
        Camera.main.orthographicSize = destination.y + 10; 
    }
}
