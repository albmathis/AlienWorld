using UnityEngine;
using System.Collections;

public class ShootableBox : MonoBehaviour {

    // La cantidad de salud actual de la caja
    public int currentHealth = 3;
    public int damage = 1;
    
    // Clips de sonido para la destrucción de la caja y para cuando el jugador se acerca demasiado
    public AudioClip clip;
    public AudioClip clip2;
    
    // Volumen para los clips de sonido
    public float volume = 10;
    
    // El GameObject que contiene el controlador de la interfaz de usuario
    public GameObject uiControllerGO;
    
    // El GameObject que contiene el controlador de menú
    public GameObject menuControllerGO;

    // El controlador de menú
    MenuController mucontroller;

    private Transform target; // Objetivo del enemigo
    public float moveSpeed = 12.0f; // Movimiento del enemigo

    // El controlador de la interfaz de usuario
    UIController uicontroller;

    // Inicializa el controlador de la interfaz de usuario y el controlador de menú
    private void Start()
    {
        uiControllerGO = GameObject.FindGameObjectWithTag("UI");
        menuControllerGO = GameObject.FindGameObjectWithTag("MU");
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Se llama cuando se actualiza el cuadro
    void Update()
    {
        // Encuentra el GameObject del jugador
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        // movimiento hacia el objetivo
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        // Calculate the direction to the player
        Vector3 directionToPlayer = (target.position - transform.position).normalized;

        // Rotate towards the player
        transform.rotation = Quaternion.LookRotation(directionToPlayer);
        // Calcula la distancia entre la caja y el jugador
        float distance = Vector3.Distance(transform.position, player.transform.position);

        // Si el jugador se acerca demasiado, destruye la caja y hace daño al jugador
        if (distance < 1.0f)
        {
            Destroy(gameObject);
            VG.healthPlayerStatic -= damage;
            AudioSource.PlayClipAtPoint(clip2, transform.position, volume); // Produce el sonido de explosión
            uicontroller = uiControllerGO.GetComponent<UIController>();
            uicontroller.RestarVida();
            if (VG.healthPlayerStatic <= 0) // Si el jugador pierde toda su salud, es game over
            {
                Destroy(player);
                VG.healthPlayerStatic = 0; // Para evitar errores, establece la salud en 0
                VG.score = 0;
                mucontroller = menuControllerGO.GetComponent<MenuController>();
                mucontroller.YouLose();
            }   
        }
    }

    // Se llama cuando la caja recibe daño
    public void Damage(int damageAmount)
    {
        // Resta la cantidad de daño
        currentHealth -= damageAmount;

        // Si la salud cae por debajo de cero, la caja es destruida y aumenta el puntaje
        if (currentHealth <= 0) 
        {
            VG.score++;
            RestarVida();
        }
    }

    // Destruye la caja y aumenta el puntaje
    void RestarVida()
    {
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(clip, transform.position, volume);
        uicontroller = uiControllerGO.GetComponent<UIController>();
        uicontroller.SumarPuntos();
        
        // Si el puntaje supera los 70, el jugador gana
        if (VG.score > 70) 
        {
            mucontroller = menuControllerGO.GetComponent<MenuController>();
            mucontroller.YouWin();
            VG.score = 0;
        }
    }
}