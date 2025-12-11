using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.WSA;

public class PlayerControler : MonoBehaviour
{
    public float speed = 3.0f;

    public int maxhealth = 5;

    public GameObject projectilePrefab;

    public float timeInvincible = 2;

    public int health { get { return currentHealth; } }
    int currentHealth;

    bool isinvincible;
    float invincibleTimer;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    private object animator;

   
    Vector2 lookDirection = new Vector2(1, 0);

    // Start is called before the first frame update
    void Start()
    {

        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxhealth;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        

        if (isinvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if(invincibleTimer < 0 )
            {
                isinvincible = false;
            }
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            Launch();
            
        }

        if(Input.GetKeyDown(KeyCode.X))
        {
           
        }
    }

    private void Launch()
    {
        throw new NotImplementedException();
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
            
            if(isinvincible)
            {
                return;
            }
            return;
        }
        isinvincible = true;
        invincibleTimer = timeInvincible;

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxhealth);
        UIHealthBar.instance.SetValue(currentHealth/(float)maxhealth);
    }

    void Lunch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        
    }

}
