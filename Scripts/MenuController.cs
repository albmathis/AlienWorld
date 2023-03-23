using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Credits()
    {
        SceneManager.LoadScene("AlienWorld"); // Escena de Los Creditos
    }

    public void Options()
    {
        SceneManager.LoadScene("Options"); // Escena de las Opciones
    }
    public void YouWin()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("YouWin"); // Escena de Ganar

    }
    public void YouLose()
    {
        SceneManager.LoadScene("YouLose"); // Escena de Ganar
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Menu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("MainMenu"); // Escena del Menu Principal
    }
    public void Exit() // Para salir del juego sin salir de Unity.
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    
    }
}
