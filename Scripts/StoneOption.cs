using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneOption : MonoBehaviour
{
    public float xp = 100;
    
    public Material[] material;

    private void Awake()
    {
         
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (xp<0)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
           print("impulse"+collision.impulse);
            print("relativeVelocity" + collision.relativeVelocity);
            float sumInpuls = Mathf.Abs( collision.impulse.x) + Mathf.Abs(collision.impulse.y) + Mathf.Abs(collision.impulse.z);
            
            xp -= sumInpuls;
            
        }
    }
}
