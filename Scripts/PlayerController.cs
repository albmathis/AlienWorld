using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int healthPlayer;
    // Start is called before the first frame update
    void Start()
    {
        VG.healthPlayerStatic = healthPlayer; // Vida del jugador en un principio
    }
    private void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

}
