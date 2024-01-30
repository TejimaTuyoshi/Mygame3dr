using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KeyAndDoor : MonoBehaviour
{
    [SerializeField] GameObject door;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            door.SetActive(false);
            this.gameObject.SetActive(false);
            Debug.Log("åÆÇå©Ç¬ÇØÇΩÅI");
        }
    }
}
