using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float velocidad = 5.0f;
    public float fuerzaSalto = 10.0f;
    public bool enSuelo; 
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        enSuelo = true;

        // Obtener una referencia al SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = true;
    }

    private void Update()
    {
        var movimientoHorizontal = Input.GetAxis("Horizontal");

        var movimiento = new Vector2(movimientoHorizontal, -rb2d.gravityScale);

        rb2d.velocity = movimiento * velocidad;

        spriteRenderer.flipX = movimientoHorizontal switch
        {
            // Cambiar la escala del sprite según la dirección
            < 0 => false,
            > 0 => true,
            _ => spriteRenderer.flipX
        };
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            enSuelo = true; // El personaje está en el suelo
        }
    }
}