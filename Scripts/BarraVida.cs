using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    // Utilizamos la imagen de Barra de el AssetStore
    public Image barra;
    [Range(0.0f, 20f)]
    private float vida;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Restamos proporcionalmente la vida segun la division entre la vida total, gracias a fillAmount.
        vida = (float)VG.healthPlayerStatic;
        barra.fillAmount = vida / 20;
    }
}