using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceSound : MonoBehaviour
{
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            audioSource.Play();
            this.enabled = false;
            this.GetComponent<Collider>().enabled = false;
        }
    }
}
