using UnityEngine;

public class GridLayout : MonoBehaviour
{
    [Header("Grid Settings")]
    public int gridSizeX = 3;
    public int gridSizeZ = 3;
    public float cellSize = 1.5f;
    public float itemHeight = 0.5f;

    [Header("Visual Settings")]
    public bool showDebugGrid = true;
    public Color debugLineColor = Color.white;

    private Vector3 gridOriginPos;
    [SerializeField] private Transform gridOriginTransform;

    private void Awake()
    {
        gridOriginPos = gridOriginTransform.position;
    }

    private void Start()
    {
        if (showDebugGrid) { DrawDebugGrid(); }
    }

    public Vector3 GetGridPosition(int index)
    {
        int row = index / gridSizeX;
        int col = index % gridSizeX;

        Vector3 position = gridOriginPos;
        position.x += col * cellSize;
        position.z += row * cellSize;
        position.y += itemHeight;

        return position;
    }

    private void DrawDebugGrid()
    {
        float gridWidth = gridSizeX * cellSize;
        float gridLength = gridSizeZ * cellSize;

        for (int x = 0; x <= gridSizeX; x++)
        {
            Debug.DrawLine(
                gridOriginPos + new Vector3(x * cellSize, 0, 0),
                gridOriginPos + new Vector3(x * cellSize, 0, gridLength),
                debugLineColor,
                Mathf.Infinity
            );
        }

        for (int z = 0; z <= gridSizeZ; z++)
        {
            Debug.DrawLine(
                gridOriginPos + new Vector3(0, 0, z * cellSize),
                gridOriginPos + new Vector3(gridWidth, 0, z * cellSize),
                debugLineColor,
                Mathf.Infinity
            );
        }
    }

    public bool IsValidGridPosition(Vector3 position)
    {
        Vector3 localPosition = position - gridOriginPos;
        float x = localPosition.x / cellSize;
        float z = localPosition.z / cellSize;

        return x >= 0 && x < gridSizeX && z >= 0 && z < gridSizeZ;
    }
}
