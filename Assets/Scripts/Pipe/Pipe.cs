using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [field:SerializeField] public bool IsOpen { get; private set; }
    [SerializeField] private PipeConnectZone pipeConnectZoneLeft;
    [SerializeField] private PipeConnectZone pipeConnectZoneRight;

    private float maxHeight = 20f;
    private float minHeight = 1.5f;
    private float moveSpeed = 5f;

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

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            LiftingPipe();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            LoweringPipe();
        }
    }

    public void LiftingPipe()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        CheckBorder();
    }

    public void LoweringPipe()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        CheckBorder();
    }

    private void CheckBorder()
    {
        Vector3 clampedPosition = transform.position;
        clampedPosition.y = Mathf.Clamp(transform.position.y, minHeight, maxHeight);
        transform.position = clampedPosition;
    }

    public void ActivateValve(bool isActivate)
    {
        IsOpen = isActivate;
    }
}
