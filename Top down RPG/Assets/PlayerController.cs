using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.01f;
    bool canMove = true;
    public SwordAttack swordAttack;

    Vector2 movementInput;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    Animator animator;
    SpriteRenderer spriterenderer;
    

    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriterenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate() {
        if (canMove)
        {
            if (movementInput != Vector2.zero)
            {
                bool success = tryMove(movementInput);

                if (!success)
                {
                    success = tryMove(new Vector2(movementInput.x, 0));
                }

                if (!success)
                {
                    success = tryMove(new Vector2(0, movementInput.y));
                }

                animator.SetBool("isMoving", success);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }

            // set driection of sprite to movement direction
            if (movementInput.x < 0)
            {
                spriterenderer.flipX = true;
            }
            else if (movementInput.x > 0)
            {
                spriterenderer.flipX = false;
            }
        }
    }

    private bool tryMove(Vector2 direction) {
        if (direction != Vector2.zero)
        {
            // detect collisions
            int count = rb.Cast(direction, // x and y values betwwen -1 and 1 that represent the direction from the body to look for collisions
                movementFilter, // the settings that determine where a collision can occur on such as layers to collide with
                castCollisions, // list of collisions to store the found collisions into after the cast is finished
                moveSpeed * Time.fixedDeltaTime + collisionOffset); // the amount to cast equal to the movement plus an offset

            if (count == 0)
            {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                return false;
            }
        } else
        {
            return false;
        }
    }
    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    void OnFire()
    {
        animator.SetTrigger("swordAttack");
    }

    public void lockMovement()
    {
        canMove = false;
    }

    public void UnlockMovement()
    {
        canMove = true;
    }

    public void Attack() {
        lockMovement();
        if(spriterenderer.flipX == true) {
            swordAttack.attackLeft();
        } else {
            swordAttack.attackRight();
        }
    }

    public void EndSwordAttack()
    {
        UnlockMovement();
        swordAttack.stopAttack();
    }
}
