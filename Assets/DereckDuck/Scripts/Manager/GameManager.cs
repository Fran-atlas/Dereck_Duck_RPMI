using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public bool pause;
    public static GameManager Instance
    {
        get
        {
            if (instance != null) Debug.Log("There is a GameManager already!");
            return instance;
        }
    }

    [Header("UI Panels")]
    [SerializeField] private GameObject gameOverPanel = null;
    [SerializeField] private GameObject winPanel = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject); // Si metemos otros GAMEMANAGER, cuando ambos estén metidos, con este podemos eliminar el segundo GameManager.
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.LoadScene("Level3");
        }
    }

    public void GameOver()
    {
        // Inicia la corrutina que retrasará la acción
        StartCoroutine(ShowGameOverPanelWithDelay(5f));
    }

    private IEnumerator ShowGameOverPanelWithDelay(float delay)
    {
        // Espera el tiempo indicado (en este caso, 5 segundos)
        yield return new WaitForSeconds(delay);

        // Muestra el panel de Game Over después del retraso
        if (winPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // Detiene el tiempo del juego
        Time.timeScale = 0;
    }

    public void Win()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }
        Time.timeScale = 0;
    }

    public void Close()
    {
        // Cierra el juego
        Debug.Log("Game Closed");
        Application.Quit();

        // Si estás probando en el editor de Unity, puedes usar esto para cerrar la aplicación del editor también:
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
