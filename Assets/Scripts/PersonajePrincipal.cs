using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float velocidadNormal = 5.0f;
    public float velocidadCorriendo = 8.5f;
    public float factorVelocidadCorriendo = 1.4f;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    private static readonly int EstaAndando = Animator.StringToHash("estaAndando");

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = true;
        
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        var movimientoHorizontal = GetMovimientoHorizontal();
        var correr = GetCorriendo();

        // Ajustar la velocidad según si está corriendo o no
        var velocidadActual = correr ? velocidadCorriendo : velocidadNormal;

        // Ajustar la velocidad del movimiento
        var movimiento = new Vector2(movimientoHorizontal, -rb2d.gravityScale);
        rb2d.velocity = movimiento * velocidadActual;

        // Ajustar la animación de caminar
        animator.SetBool(EstaAndando, movimientoHorizontal != 0);

        // Ajustar la velocidad de ejecución de la animación de caminar
        var factorVelocidadAnimacion = correr ? factorVelocidadCorriendo : 1f;
        animator.speed = factorVelocidadAnimacion;

        // Ajustar la escala del sprite para reflejar la dirección del movimiento
        spriteRenderer.flipX = movimientoHorizontal switch
        {
            < 0 => false,
            > 0 => true,
            _ => spriteRenderer.flipX
        };
    }

    private static float GetMovimientoHorizontal()
    {
        return Input.GetAxis("Horizontal");
    }

    private static bool GetCorriendo()
    {
        return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
    }
}