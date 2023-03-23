using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevitateObject : MonoBehaviour
{

    // La variable AudioClip contiene el sonido que se reproducirá al destruir el objeto
    public AudioClip clip;

    // El volumen del sonido
    public float volume = 10;

    // GameObjects que contienen los controladores de la UI y del menú
    public GameObject uiControllerGO; 
    public GameObject menuControllerGO; 

    MenuController mucontroller; // Instancia del controlador del menú
    UIController uicontroller; // Instancia del controlador de la UI

    // La velocidad de levitación del objeto
    public float speed = 1.0f;

    // El rango de levitación del objeto
    public float range = 1.0f;

    // La posición inicial del objeto
    private Vector3 startPosition;

    // Este método se ejecuta al inicio del juego
    private void Start()
    {
        // Busca el objeto con el tag "UI" y "MU" y los almacena en las variables correspondientes
        uiControllerGO = GameObject.FindGameObjectWithTag("UI");
        menuControllerGO = GameObject.FindGameObjectWithTag("MU");
        
        // Almacena la posición inicial del objeto
        startPosition = transform.position;
    }

    // Este método se ejecuta en cada frame del juego
    void Update()
    {
        // Busca el objeto con el tag "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // Calcula la distancia entre el objeto y el jugador
        float distance = Vector3.Distance(transform.position, player.transform.position);

        // Si la distancia es menor que 2 unidades, destruye el objeto, reproduce el sonido y suma puntos
        if (distance < 2.0f)
        {
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(clip, transform.position, volume);
            SumarPunts();
        }

        // Calcula la nueva posición del objeto en el eje Y
        float newY = Mathf.Sin(Time.time * speed) * range + startPosition.y;

        // Establece la nueva posición del objeto
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    // Este método suma puntos y comprueba si el jugador ha ganado
    void SumarPunts()
    {
        // Suma 20 puntos
        VG.score = VG.score + 20;

        // Obtiene la instancia del controlador de la UI y llama al método SumarPuntos()
        uicontroller = uiControllerGO.GetComponent<UIController>();
        uicontroller.SumarPuntos();

        // Si el jugador tiene más de 70 puntos, llama al método YouWin() del controlador del menú y reinicia los puntos a 0
        if (VG.score > 70)
        {
            mucontroller = menuControllerGO.GetComponent<MenuController>();
            mucontroller.YouWin();
            VG.score = 0;
        }
    }
}