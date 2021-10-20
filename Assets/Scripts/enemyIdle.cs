using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemyIdle : MonoBehaviour
{

    public int enemyHealth;
    public float enemySpd;
    public float idleDist;
    private float stunTime;
    public float startStun;

    private bool movingRight = true;
    public Transform groundDetect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (stunTime <= 0) {

            enemySpd = 2;
        
        }
        else {

            enemySpd = 0;
            stunTime -= Time.deltaTime;
        
        }


        if (enemyHealth <= 0) {

            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);

        }

        transform.Translate(Vector2.right * enemySpd * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetect.position, Vector2.down, idleDist);
        if (groundInfo.collider == false) {

            if (movingRight == true) {

                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;

            }
            else {

                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            
            }
        
        }
    }

    public void TakeDamage(int damage) {

        stunTime = startStun;
        enemyHealth -= damage;
    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}
