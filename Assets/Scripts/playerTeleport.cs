using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTeleport : MonoBehaviour
{

    private GameObject currentTeleporter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1)) {

            if (currentTeleporter != null) {

                transform.position = currentTeleporter.GetComponent<teleporterScrpt>().GetDestination().position;

            }
        
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Teleporter")) {

            currentTeleporter = collision.gameObject;
        
        }

    }

    private void OnTriggerExit2D(Collider2D collision) {

        if (collision.gameObject == currentTeleporter) {

            currentTeleporter = null;

        }
    
    }

}
