using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{

    [SerializeField] private LayerMask playerProyectile;
    [SerializeField] public int life = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int playerProyectileMask = playerProyectile.value;
        Debug.Log(playerProyectileMask + "  " + collision.gameObject.layer);

        if (collision.gameObject.layer == playerProyectileMask)
        {
            Debug.Log("Trigger2");
        }
    }

}
