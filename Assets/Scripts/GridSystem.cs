using UnityEngine;

public class GridSystem : MonoBehaviour
{
    /// <summary>
    /// グリッドの行数
    /// </summary>
    [SerializeField] private int rows;

    /// <summary>
    /// グリッドの列数
    /// </summary>
    [SerializeField] private int columns;

    /// <summary>
    /// セルのサイズ
    /// </summary>
    [SerializeField] private float cellSize;

    /// <summary>
    /// グリッドの原点位置
    /// </summary>
    private Vector3 originPosition; // グリッドの原点の位置

    private void Start()
    {
        Vector3 offset = new Vector3(-columns * cellSize * 0.5f, cellSize * 0.5f, -rows * cellSize * 0.5f);
        originPosition = transform.position + offset;
        CreateGrid();
    }

    private void CreateGrid()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Vector3 cellPosition = GetCellPosition(i, j);
                PlaceItem(cellPosition);
            }
        }
    }

    private Vector3 GetCellPosition(int row, int column)
    {
        float xPosition = originPosition.x + (cellSize * column) + cellSize / 2;
        float zPosition = originPosition.z + (cellSize * row) + cellSize / 2;
        return new Vector3(xPosition, originPosition.y, zPosition);
    }

    private void PlaceItem(Vector3 position)
    {
        //Instantiate(itemPrefab, position, Quaternion.identity);
    }

    /// <summary>
    /// スクリプトがGameObjectに設定されているとき
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green; // グリッドの色を設定

        for (int i = 0; i <= rows; i++)
        {
            float zPosition = originPosition.z + i * cellSize;
            Vector3 startPos = new Vector3(originPosition.x, originPosition.y, zPosition);
            Vector3 endPos = new Vector3(originPosition.x + columns * cellSize, originPosition.y, zPosition);
            Gizmos.DrawLine(startPos, endPos);
        }

        for (int j = 0; j <= columns; j++)
        {
            float xPosition = originPosition.x + j * cellSize;
            Vector3 startPos = new Vector3(xPosition, originPosition.y, originPosition.z);
            Vector3 endPos = new Vector3(xPosition, originPosition.y, originPosition.z + rows * cellSize);
            Gizmos.DrawLine(startPos, endPos);
        }
    }
}

