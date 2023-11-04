using UnityEngine;

public enum TypeOfConnectZone
{
    Left,
    Right
}
public class PipeConnectZone : MonoBehaviour
{
    [SerializeField] private TypeOfConnectZone typeOfConnectZone;
    private Pipe pipe;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (pipe.IsOpen && collision.TryGetComponent(out LiquidElement liquidElement) && !liquidElement.IsTransfer)
        {
            pipe.TransferFluid(this, liquidElement);
        }
    }

    public void SetPipe(Pipe _pipe)
    {
        pipe = _pipe;
    }

    public TypeOfConnectZone GetTypeOfConnectZone()
    {
        return typeOfConnectZone;
    }

    public void TransferFluidToPos(Vector3 newPos, LiquidElement liquidElement)
    {
        liquidElement.transform.position = newPos;
    }
}
