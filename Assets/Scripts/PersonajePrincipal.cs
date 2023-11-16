using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb2d;
    
    public float velocidadNormal = 5.0f;
    public float velocidadCorriendo = 8.5f;
    private const float FactorVelocidadCorriendo = 1.2f;

    private bool _enSuelo;
    public LayerMask capaSuelo;
    private const float FuerzaSalto = 10.0f;
    
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    private static readonly int EstaAndando = Animator.StringToHash("estaAndando");

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = true;
        
        animator = GetComponent<Animator>();

        _enSuelo = EstaEnSuelo();
        capaSuelo = LayerMask.GetMask("Suelo");
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
        var factorVelocidadAnimacion = correr ? FactorVelocidadCorriendo : 1f;
        animator.speed = factorVelocidadAnimacion;

        // Ajustar la escala del sprite para reflejar la dirección del movimiento
        spriteRenderer.flipX = movimientoHorizontal switch
        {
            < 0 => false,
            > 0 => true,
            _ => spriteRenderer.flipX
        };
        
        // Añadir la condición para el salto
        if (Input.GetKeyDown(KeyCode.Space) && _enSuelo)
        {
            Saltar();
        }
    }

    private static float GetMovimientoHorizontal()
    {
        return Input.GetAxis("Horizontal");
    }

    private static bool GetCorriendo()
    {
        return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
    }
    
    private bool EstaEnSuelo()
    {
        // Lanzar un rayo hacia abajo desde la posición del personaje
        var hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, capaSuelo);

        // Comprobar si el rayo golpeó algo (está en el suelo)
        return hit.collider != null;
    }
    
    private void Saltar()
    {
        rb2d.AddForce(Vector2.up * FuerzaSalto, ForceMode2D.Impulse);
        _enSuelo = false; // Actualizar la condición de estar en el suelo
        // También puedes añadir una transición de animación de salto aquí si es necesario.
    }


}