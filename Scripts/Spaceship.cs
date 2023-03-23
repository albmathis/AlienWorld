using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{

    // The box's current health point total
    public AudioClip clip;
    public float volume = 10;
    public GameObject uiControllerGO; // The UI controller GameObject
    public GameObject menuControllerGO; // The MemuController controller GameObject

    MenuController mucontroller; 

    UIController uicontroller; 

    // The speed of levitation
    public float speed = 1.0f;

    // The range of levitation
    public float range = 1.0f;

    // The starting position of the object
    private Vector3 startPosition;


    private void Start()
    {
        uiControllerGO = GameObject.FindGameObjectWithTag("UI");
        menuControllerGO = GameObject.FindGameObjectWithTag("MU");
        startPosition = transform.position;
    }

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < 10.0f && VG.score > 60 && Input.GetKeyDown(KeyCode.E))
        {
            AudioSource.PlayClipAtPoint(clip, transform.position, volume); // Producimos el ruido de la explosion
            mucontroller = menuControllerGO.GetComponent<MenuController>();
            mucontroller.YouWin();
            VG.score = 0;
        }
        // Calculate the new y position of the object
        float newY = Mathf.Sin(Time.time * speed) * range + startPosition.y;

        // Set the new position of the object
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}