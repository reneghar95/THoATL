using UnityEngine;

// Interactable como clase base abstracta. No mas CompareTag duplicados. Detectar player > llamar metodo.
public abstract class Interactable : MonoBehaviour
{
    // Variable de proximidad del player para las subclases
    protected bool isPlayerNear = false;

    // Trigger unico
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        isPlayerNear = true;
        OnPlayerEnter();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        isPlayerNear = false;
        OnPlayerExit();
    }

    // metodo abstracto para que las subclases si o si tengan que definir el comportamiento
    protected abstract void OnPlayerEnter();

    // virtual para que puedan sobrescribir si necesitan
    protected virtual void OnPlayerExit() { }
}