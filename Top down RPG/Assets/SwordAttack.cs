using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public Collider2D swordCollider;
    public float damage = 3;
    Vector2 rightAttackOffset;
    

    private void Start() {
        rightAttackOffset = transform.position;
    }

    public void attackRight() {
        swordCollider.enabled = true;
        transform.localPosition = rightAttackOffset;
    }


     public void attackLeft() {
        swordCollider.enabled = true;
        transform.localPosition = new Vector3(rightAttackOffset.x -1, rightAttackOffset.y);
     }

    public void stopAttack() {
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Enemy") {
            EnemyScript enemy = other.GetComponent<EnemyScript>();

            if (enemy != null) {
                enemy.setHealth(damage);
            }
        }
    }
}
