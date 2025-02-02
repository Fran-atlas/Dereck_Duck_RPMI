using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickUp : MonoBehaviour
{
    public string sceneToLoad; // Nombre de la escena a la que cambiar

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Verifica si el jugador toca el cristal
        {
            SceneManager.LoadScene(sceneToLoad); // Cambia de escena
        }
    }
}
