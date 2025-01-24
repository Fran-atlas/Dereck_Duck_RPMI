using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance != null) Debug.Log("There is a GameManager already!");
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject); // Si metemos otros GAMEMANAGER, cuando ambos estén metidos, con este podemos elminiar el segundo GameManager.
        }
    }
}
