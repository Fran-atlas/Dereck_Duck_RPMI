using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float enemySpeed = -1f;
    Rigidbody2D newrigidbody;


 
    void Start()
    {
        newrigidbody = GetComponent<Rigidbody2D>();
    }

  
    void Update()
    {
        if (isFacingLeft())
        {
            newrigidbody.velocity = new Vector2(enemySpeed, newrigidbody.velocity.y);
        }
        else
        {
            newrigidbody.velocity = new Vector2(-enemySpeed, newrigidbody.velocity.y);
        }
    }

    bool isFacingLeft()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(newrigidbody.velocity.x, transform.localScale.y);
    }
}
