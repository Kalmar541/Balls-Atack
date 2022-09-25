using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rigitbodysleeps : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb!=null)
        {
            rb.Sleep();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))

        {
            collision.gameObject.tag="Untagged";
            Destroy(collision.gameObject, 4f);
            //Destroy(this.gameObject);
            
        }
    }
}
