using UnityEngine;

public class PersonajePrincipal : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float velocidad = 5.0f;
    public float fuerzaSalto = 10.0f;
    public bool enSuelo;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        enSuelo = true;
    }

    private void Update()
    {
        var movimientoHorizontal = Input.GetAxis("Horizontal");

        var movimiento = new Vector2(movimientoHorizontal, -rb2d.gravityScale);

        rb2d.velocity = movimiento * velocidad;
        
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            enSuelo = true; // El personaje está en el suelo
        }
    }
}