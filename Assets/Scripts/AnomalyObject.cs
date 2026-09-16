using UnityEngine;

// Clase abstracta para los objetos con anomalias IAnomalous aplica ApplyAnomaly y ResetAnomaly.
public abstract class AnomalyObject : MonoBehaviour, IAnomalous
{
    [SerializeField] private string anomalyDescription = "Something feels off.";

    // Read-only para acceder a la descripcion de la anomalia desde otras clases
    public string AnomalyDescription => anomalyDescription;

    public abstract void ApplyAnomaly();

    public virtual void ResetAnomaly() { }
}