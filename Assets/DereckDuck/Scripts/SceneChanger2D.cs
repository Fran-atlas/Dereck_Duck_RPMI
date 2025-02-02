using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public string sceneName; 
    public Button startButton; 
    public Button exitButton; 

    void Start()
    {
        if (startButton != null)
        {
            startButton.onClick.AddListener(ChangeScene);
        }
        else
        {
            Debug.LogWarning("No se ha asignado un bot�n en el Inspector");
        }
    }

    void ChangeScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("No se ha asignado un nombre de escena en el Inspector");
        }
    }
    public void ExitGame()
    {
        Application.Quit(); //Salir de la aplicaci�n, cierra el juego completamente
    }


}
