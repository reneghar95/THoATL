// Interface para anomalias
public interface IAnomalous
{
    // Anomalia activa
    void ApplyAnomaly();

    // Anomalia inactiva
    void ResetAnomaly();

    string AnomalyDescription { get; }
}