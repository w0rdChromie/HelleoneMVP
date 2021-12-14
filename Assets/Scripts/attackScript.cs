using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackScript : MonoBehaviour
{
    private float attackPause;
    public float startAtkPause;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float atkRange;
    public int damage;
    public float atkRangeX;
    public float atkRangeY;

    // Update is called once per frame
    void Update()
    {
        if (attackPause <= 0) {

            if (Input.GetKeyUp(KeyCode.Mouse0)) {

                Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(atkRangeX, atkRangeY), 0, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++) {
                
                    enemiesToDamage[i].GetComponent<enemyIdle>().TakeDamage(damage);
                
                }
            
            }

            attackPause = startAtkPause;
        
        }
        else {

            attackPause -= Time.deltaTime;
        
        }
    }

    void OnDrawGizmosSelected() {

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector3(atkRangeX, atkRangeY, 1));
    
    }

}
