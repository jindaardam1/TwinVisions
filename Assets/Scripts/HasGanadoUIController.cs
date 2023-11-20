using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HasGanadoUIController : MonoBehaviour
{
    public KeyCode[] teclasReinicio = { KeyCode.R, KeyCode.Space, KeyCode.Return };

    private void Update()
    {
        if (teclasReinicio.Any(tecla => Input.GetKeyDown(tecla)))
        {
            ReiniciarJuego();
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TerminarJuego();
        }
    }
    
    private void ReiniciarJuego()
    {
        SceneManager.LoadScene(0);
    }
    
    private void TerminarJuego()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
