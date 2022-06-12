using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float health = 10;
    Animator animate;

    private void Start()
    {
        animate = GetComponent<Animator>();
    }

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

    public void removeEnemy() {
        Destroy(gameObject);
    }

    public void defeated()
    {
        animate.SetTrigger("Defeated");
    }
}
