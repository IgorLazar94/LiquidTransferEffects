using UnityEngine;

public class PoolController : MonoBehaviour
{
    [SerializeField] private int poolCount;
    [SerializeField] private bool autoExpand;
    [SerializeField] private LiquidElement liquidElementPrefab;

    private LiquidPool<LiquidElement> pool;

    private void Start()
    {
        this.pool = new LiquidPool<LiquidElement>(liquidElementPrefab, poolCount, this.transform);
        this.pool.autoExpand = this.autoExpand;
    }

    public LiquidElement CreateLiquidElement()
    {
        var bullet = this.pool.GetFreeElement();
        return bullet;
    }
}
