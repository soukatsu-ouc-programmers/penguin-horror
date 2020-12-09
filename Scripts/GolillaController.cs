using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolillaController : MonoBehaviour
{
    /// <summary>
    /// 移動スピード
    /// </summary>
    [SerializeField]
    private float moveSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move(moveSpeed);

    }

    private void Move(float speed)
    {
        this.transform.Translate(Vector3.forward * speed);
    }
    private void OnCollisionEnter(Collision collision)
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Obstacle")
        {
            Destroy(other.gameObject);
        }
        
    }
}
