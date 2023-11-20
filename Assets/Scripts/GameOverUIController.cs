using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUIController : MonoBehaviour
{
    public KeyCode[] teclasReinicio = { KeyCode.R, KeyCode.Space, KeyCode.Return };

    private void Update()
    {
        if (teclasReinicio.Any(tecla => Input.GetKeyDown(tecla)))
        {
            ReiniciarJuego();
            return;
        }
    }
    
    private void ReiniciarJuego()
    {
        SceneManager.LoadScene(0);
    }
}
