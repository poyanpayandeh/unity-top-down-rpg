using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float health = 10;

    public void setHealth(float value) {
        health -= value;
            if (health <= 0) {
                defeated();
            }
            print(getHealth());
    }

    public float getHealth() {
        return this.health;
    }

    public void defeated() {
        Destroy(gameObject);
    }
}
