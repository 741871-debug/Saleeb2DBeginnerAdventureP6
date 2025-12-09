using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{
    public float speed = 3.0f;

    public int maxhealth = 5;
    public float timeInvincible = 2;

    public int health { get { return currentHealth; } }
    int currentHealth;

    bool isinvincible;
    float invincibleTimer;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    // Start is called before the first frame update
    void Start()
    {

        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxhealth;
        currentHealth = 1;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (isinvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if(invincibleTimer < 0 )
            {
                isinvincible = false;
            }
        }

    }
    private void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime; ;

        rigidbody2d.MovePosition(position);
    }
    public void ChangeHealth(int amount)
    {
        if(amount < 0)
        {
            return;
        }
        isinvincible = true;
        invincibleTimer = timeInvincible;

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxhealth);
        Debug.Log(currentHealth + "/" + maxhealth + (5));
    }

}
