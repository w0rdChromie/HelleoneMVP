using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class newEnemyScript : MonoBehaviour
{

    [SerializeField] Transform player;
    [SerializeField] float wakeRange;
    [SerializeField] float enSpd;
    private float startSpd;
    Rigidbody2D enrb;

    public int enHealth;
    private float stun;
    public float stunStart;
    public bool hurt;
    private Renderer rendCol;

    [SerializeField] private Color normColor = Color.white;
    [SerializeField] private Color dmgColor = Color.white;


    // Start is called before the first frame update
    void Start()
    {
        enrb = GetComponent<Rigidbody2D>();
        rendCol = GetComponent<Renderer>();
        startSpd = enSpd;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToTarget = Vector2.Distance(transform.position, player.position);
        if (distanceToTarget < wakeRange && stun <= 0) {

            chaseTarget();
            hurt = false;

        }

        if (hurt)
        {

            enSpd = 0;
            stun -= Time.deltaTime;
            rendCol.material.color = dmgColor;

        }
        else
        {
            enSpd = startSpd;
            rendCol.material.color = normColor;

        }

        if (enHealth <= 0)
        {

            Destroy(gameObject);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);

        }

        if (transform.position.y < -12)
        {

            Destroy(gameObject);

        }
    }

    private void chaseTarget() {

        if (transform.position.x < player.position.x)
        {

            enrb.velocity = new Vector2(enSpd, 0);
            transform.localScale = new Vector2(1,1);

        }

        else{

            enrb.velocity = new Vector2(-enSpd, 0);
            transform.localScale = new Vector2(-1,1);

        }
    
    }

    private void stopPursuit() {

        enrb.velocity = new Vector2(0,0);
    
    }

    public void damageTaken(int dmg)
    {

        stun = stunStart;
        enHealth -= dmg;
        hurt = true;
        enrb.AddForce(new Vector2(-5f, 2f), ForceMode2D.Impulse);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerCont playerC = collision.GetComponent<playerCont>();
            playerC.TakeDamage(10);
        }

        if (collision.gameObject.CompareTag("Sword"))
        {
            damageTaken(1);
            Debug.Log(collision.transform.name);
        }

    }
}
