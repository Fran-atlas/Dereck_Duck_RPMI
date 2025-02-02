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
            Debug.LogWarning("No se ha asignado un botón en el Inspector");
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
        Application.Quit(); //Salir de la aplicación, cierra el juego completamente
    }


}
