using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb2d;

    public float velocidadNormal = 5.0f;
    public float velocidadCorriendo = 8.5f;
    private const float FactorVelocidadCorriendo = 1.2f;

    public LayerMask capaSuelo;
    private const float FuerzaSalto = 11.0f;
    public const int SaltosMaximos = 1;
    public int saltosRestantes;

    public BoxCollider2D boxCollider;

    public SpriteRenderer spriteRenderer;
    public Animator animator;
    private static readonly int EstaAndando = Animator.StringToHash("estaAndando");
    
    public GameManager gameManager;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = true;

        animator = GetComponent<Animator>();

        boxCollider.GetComponent<BoxCollider2D>();

        saltosRestantes = SaltosMaximos;
    }

    private void Update()
    {
        var movimientoHorizontal = GetMovimientoHorizontal();
        var correr = GetCorriendo();

        GestionarVelocidadYMovimiento(correr, movimientoHorizontal);

        DireccionJugador(movimientoHorizontal);

        ProcesarSalto();
    }

    private void GestionarVelocidadYMovimiento(bool correr, float movimientoHorizontal)
    {
        var velocidadActual = correr ? velocidadCorriendo : velocidadNormal;

        rb2d.velocity = new Vector2(movimientoHorizontal * velocidadActual, rb2d.velocity.y);

        animator.SetBool(EstaAndando, movimientoHorizontal != 0);

        var factorVelocidadAnimacion = correr ? FactorVelocidadCorriendo : 1f;
        animator.speed = factorVelocidadAnimacion;
    }

    private void DireccionJugador(float movimientoHorizontal)
    {
        spriteRenderer.flipX = movimientoHorizontal switch
        {
            < 0 => false,
            > 0 => true,
            _ => spriteRenderer.flipX
        };
    }

    private void ProcesarSalto()
    {
        if (EstaEnSuelo())
        {
            saltosRestantes = SaltosMaximos;
        }

        if (Input.GetKeyDown(KeyCode.Space) && saltosRestantes > 0)
        {
            saltosRestantes--;
            rb2d.AddForce(Vector2.up * FuerzaSalto, ForceMode2D.Impulse);
        }
    }

    private bool EstaEnSuelo()
    {
        var bounds = boxCollider.bounds;
        var rayCastHit = Physics2D.BoxCast(bounds.center, new Vector2(bounds.size.x, bounds.size.y),
            0f, Vector2.down, 0.2f, capaSuelo);

        return rayCastHit.collider != null;
    }

    private static float GetMovimientoHorizontal()
    {
        return Input.GetAxis("Horizontal");
    }

    private static bool GetCorriendo()
    {
        return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
    }
    
    public void MostrarPantallaGameOver()
    {
        SceneManager.LoadScene("Scenes/GameOver");
    }
    
    public void MostrarPantallaHasGanado()
    {
        SceneManager.LoadScene("Scenes/HasGanado");
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ZonaDeMuerte"))
        {
            MostrarPantallaGameOver();
        }
        
        if (collision.gameObject.CompareTag("GanarJuego"))
        {
            MostrarPantallaHasGanado();
        }
    }
}