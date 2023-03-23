using UnityEngine;
using System.Collections;

public class RaycastShootComplete : MonoBehaviour {

    // Variables que afectan al arma y al disparo
    public int gunDamage = 1; // El daño del arma
    public float fireRate = 0.25f; // La cadencia de fuego del arma
    public float weaponRange = 50f; // El alcance del disparo
    public float hitForce = 100f; // La fuerza con la que golpea el disparo
    public Transform gunEnd; // El final del arma de donde sale el disparo

    // Variables para las lineas de vision
    private Camera fpsCam; // La camara en primera persona
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f); // La duracion del efecto de disparo
    private AudioSource gunAudio; // El audio del disparo
    private LineRenderer laserLine; // La linea de vision del disparo
    private float nextFire; // El tiempo que falta para poder disparar otra vez

    // Start is called before the first frame update
    void Start () 
    {
        // Obtenemos los componentes necesarios del objeto
        laserLine = GetComponent<LineRenderer>(); // La linea de vision
        gunAudio = GetComponent<AudioSource>(); // El audio del arma
        fpsCam = GetComponentInParent<Camera>(); // La camara en primera persona
    }

    // Update is called once per frame
    void Update () 
    {
        // Comprobamos si el jugador pulsa el boton de disparo y si ha pasado el tiempo suficiente desde el ultimo disparo
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire) 
        {
            // Actualizamos el tiempo de espera para poder volver a disparar
            nextFire = Time.time + fireRate;

            // Iniciamos el efecto de disparo
            StartCoroutine (ShotEffect());

            // Obtenemos el punto de origen del raycast, que se encuentra en el centro de la camara
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint (new Vector3(0.5f, 0.5f, 0.0f));

            // Creamos un raycast desde el punto de origen hacia la direccion hacia donde mira la camara, con el alcance del arma
            RaycastHit hit;
            laserLine.SetPosition (0, gunEnd.position); // Iniciamos la linea de vision en el final del arma
            if (Physics.Raycast (rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
            {
                // Si el raycast choca con algo, actualizamos la posicion de la linea de vision al punto de colision
                laserLine.SetPosition (1, hit.point);

                // Si el objeto golpeado tiene un componente ShootableBox, le causamos daño
                ShootableBox health = hit.collider.GetComponent<ShootableBox>();
                if (health != null)
                {
                    health.Damage (gunDamage);
                }

                // Si el objeto golpeado tiene un rigidbody, lo empujamos hacia atras
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce (-hit.normal * hitForce);
                }
            }
            else
            {
                // Si el raycast no choca con nada, actualizamos la posicion de la linea de vision hasta el alcance del arma
                laserLine.SetPosition (1, rayOrigin + (fpsCam.transform.forward * weaponRange));
            }
        }
    }


    private IEnumerator ShotEffect()
    {
        gunAudio.Play ();

        laserLine.enabled = true;

        yield return shotDuration;

        laserLine.enabled = false;
    }
}