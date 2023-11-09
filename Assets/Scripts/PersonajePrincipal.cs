using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float velocidad = 5.0f;
    public float fuerzaSalto = 10.0f;
    public bool enSuelo;

    // Nueva variable para el SpriteRenderer
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        enSuelo = true;

        // Obtener una referencia al SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        var movimientoHorizontal = Input.GetAxis("Horizontal");

        var movimiento = new Vector2(movimientoHorizontal, -rb2d.gravityScale);

        rb2d.velocity = movimiento * velocidad;

        // Cambiar la escala del sprite según la dirección
        if (movimientoHorizontal < 0)
        {
            // Si se mueve hacia la izquierda, invierte la escala en X
            spriteRenderer.flipX = false;
        }
        else if (movimientoHorizontal > 0)
        {
            // Si se mueve hacia la derecha, restaura la escala en X
            spriteRenderer.flipX = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            enSuelo = true; // El personaje está en el suelo
        }
    }
}