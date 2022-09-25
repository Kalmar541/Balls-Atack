using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    static public bool goalMet =false;
    public bool goalMetTest = false;
    public float speed=2;
    public float alphaColor = 0.1f;
    private MeshRenderer meshrend;

    private void Awake()
    {
        meshrend = GetComponent<MeshRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //goalMetTest = goalMet;
        if (goalMet)
        {
            Dead();
        }
    }
    void Dead()
    {
        Color color = meshrend.material.color;
        if (color.a > 0)
        {
            color.a -= alphaColor * Time.deltaTime;

        }
        else { color.a = 0;
            Destroy(gameObject);
        }
        meshrend.material.color = color;
        Vector3 pos = transform.position;
        pos.y += Time.deltaTime * speed;
        transform.position = pos;
        
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            goalMet = true;
            Rigidbody rigid = GetComponent<Rigidbody>();

            rigid.useGravity = false;
            Collider collider = GetComponent<BoxCollider>();
            collider.isTrigger = true;
            // Также изменить альфа-канал цвета, чтобы увеличить непрозрачность
            /*Material mat = GetComponent<Renderer>().material;
            Color c = mat.color;
            c.a = 1;
            mat.color = c;*/
        }
    }
    /*{
        

        }
    }*/
}
