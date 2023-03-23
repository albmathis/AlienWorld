using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Scene = UnityEngine.SceneManagement.Scene;

public class UIController : MonoBehaviour
{
    public TMP_Text scoreTxt; // Valor de la puntacion
    public TMP_Text healthTxt; // Valor de la Vida

    bool pausar = false;

    // Diferentes Paneles para la jugabilidad
    public GameObject pausePanel; // Al Pausar 

    public AudioClip clip2; // Clip para la musica

    public float volume = 5; // Volumen de la propia musica

    public int healthplayer;

    public GameObject menuControllerGO; // The MemuController controller GameObject

    MenuController mucontroller; 


    // Start is called before the first frame update
    void Start()
    {
        //Valores iniciales del juego, como que el juego se reanude o los propios valores de Vida o Puntuacion
        Time.timeScale = 1;
        scoreTxt.text = VG.score.ToString();
        healthTxt.text = VG.healthPlayerStatic.ToString();
        menuControllerGO = GameObject.FindGameObjectWithTag("MU");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) // Nos fijamos si el jugador presiona Escape dentro del espacio de tiempo de un frame
        {
            Pausar();
        }
        if(Input.GetKeyDown(KeyCode.M)) // Nos fijamos si el jugador presiona M dentro del espacio de tiempo de un frame
        {
            MainMenu();
        }
    }

    public void Pausar()
    {
        if (!pausar)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
            pausar = true;
        }
        else
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
            pausar = false;
        }
    }


    public void SumarPuntos()
    {
        scoreTxt.text = VG.score.ToString(); // Sumamos los puntos al matar un enemigo o da�ar al Boss

    }

    public void RestarVida() // Restamos la vida y lo mostramos por pantalla
    {
        healthTxt.text = VG.healthPlayerStatic.ToString();
    }
    public void MainMenu()
    {
            mucontroller = menuControllerGO.GetComponent<MenuController>();
            mucontroller.Menu();
    }
}
