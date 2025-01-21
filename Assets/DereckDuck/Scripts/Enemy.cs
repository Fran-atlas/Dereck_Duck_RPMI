using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float enemySpeed = -1f;
    Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.velocity = new Vector2(-1, rigidbody.velocity.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Ha salido de la colisión");
    }
}
