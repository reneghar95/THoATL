using UnityEngine;

// Anomalia de muebles fuera de lugar.
public class MovedFurniture : AnomalyObject
{
    [SerializeField] private Vector3 normalPosition;
    [SerializeField] private Vector3 anomalyPosition;

    void Start()
    {
        if (normalPosition == Vector3.zero)
            normalPosition = transform.localPosition;
    }

    public override void ApplyAnomaly()
    {
        transform.localPosition = anomalyPosition;
    }

    public override void ResetAnomaly()
    {
        transform.localPosition = normalPosition;
    }
}