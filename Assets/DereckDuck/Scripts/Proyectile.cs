using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectile : MonoBehaviour
{

    [SerializeField] private new ParticleSystem particleSystem;
    [SerializeField] private string objectiveTag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") || collision.CompareTag(objectiveTag)) 
        {
            ParticleSystem particles = Instantiate(particleSystem, transform.position, Quaternion.identity);
            Destroy(gameObject);
        } 
    }
}
