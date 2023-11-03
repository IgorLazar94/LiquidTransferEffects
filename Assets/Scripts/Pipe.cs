using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [field:SerializeField] public bool IsOpen { get; private set; }
    [SerializeField] private PipeConnectZone pipeConnectZoneLeft;
    [SerializeField] private PipeConnectZone pipeConnectZoneRight;

    private void Start()
    {
        IsOpen = false;
        pipeConnectZoneLeft.SetPipe(this);
        pipeConnectZoneRight.SetPipe(this);
    }

    public void TransferFluid(PipeConnectZone enterPipeConnectZone, LiquidElement liquidElement)
    {
        switch (enterPipeConnectZone.GetTypeOfConnectZone())
        {
            case TypeOfConnectZone.Left:
                enterPipeConnectZone.TransferFluidToPos(pipeConnectZoneRight.transform.position, liquidElement);
                liquidElement.TransferLiquid(TypeOfConnectZone.Left);
                break;
            case TypeOfConnectZone.Right:
                enterPipeConnectZone.TransferFluidToPos(pipeConnectZoneLeft.transform.position, liquidElement);
                liquidElement.TransferLiquid(TypeOfConnectZone.Right);
                break;
            default:
                break;
        }
    }
}
