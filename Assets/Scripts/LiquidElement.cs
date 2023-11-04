using System.Collections;
using UnityEngine;

public class LiquidElement : MonoBehaviour
{
    public bool IsTransfer { get; private set; }
    private Rigidbody2D liquidBody;
    private StorageTank storageTank;
    private float impulseFoce = 15f;
    private float lowerBarrier = -10f;

    private void Start()
    {
        IsTransfer = false;
        liquidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (transform.position.y <= lowerBarrier)
        {
            storageTank.RemoveLiquidFromList(this);
            Destroy(gameObject); // ObjectPool!
        }
    }

    public void TransferLiquid(TypeOfConnectZone targetTypeOfConnectZone)
    {
        SetImpulse(targetTypeOfConnectZone);
        StartCoroutine(Transferring());
    }

    private IEnumerator Transferring()
    {
        IsTransfer = true;
        yield return new WaitForSeconds(0.25f);
        IsTransfer = false;
    }

    private void SetImpulse(TypeOfConnectZone targetTypeOfConnectZone)
    {
        switch (targetTypeOfConnectZone)
        {
            case TypeOfConnectZone.Left:
                liquidBody.AddForce(Vector2.right * impulseFoce, ForceMode2D.Impulse);
                break;
            case TypeOfConnectZone.Right:
                liquidBody.AddForce(Vector2.left * impulseFoce, ForceMode2D.Impulse);
                break;
            default:
                break;
        }
    }

    public void SetTank(StorageTank storageTank)
    {
        this.storageTank = storageTank;
    }
}
