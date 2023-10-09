using UnityEngine;

public class GridSystem : MonoBehaviour
{
    /// <summary>
    /// グリッドの行数
    /// </summary>
    [SerializeField] private int _rows;

    /// <summary>
    /// グリッドの列数
    /// </summary>
    [SerializeField] private int _columns;

    /// <summary>
    /// セルのサイズ
    /// </summary>
    [SerializeField] private float _cellSize;

    /// <summary>
    /// グリッドの原点位置
    /// </summary>
    private Vector3 originPosition; // グリッドの原点の位置

    private void Start()
    {
        Vector3 offset = new Vector3(-_columns * _cellSize * 0.5f, _cellSize * 0.5f, -_rows * _cellSize * 0.5f);
        originPosition = transform.position + offset;
        CreateGrid();
    }

    private void CreateGrid()
    {
        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _columns; j++)
            {
                Vector3 cellPosition = GetCellPosition(i, j);
                PlaceItem(cellPosition);
            }
        }
    }

    private Vector3 GetCellPosition(int row, int column)
    {
        float xPosition = originPosition.x + (_cellSize * column) + _cellSize / 2;
        float zPosition = originPosition.z + (_cellSize * row) + _cellSize / 2;
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

        for (int i = 0; i <= _rows; i++)
        {
            float zPosition = originPosition.z + i * _cellSize;
            Vector3 startPos = new Vector3(originPosition.x, originPosition.y, zPosition);
            Vector3 endPos = new Vector3(originPosition.x + _columns * _cellSize, originPosition.y, zPosition);
            Gizmos.DrawLine(startPos, endPos);
        }

        for (int j = 0; j <= _columns; j++)
        {
            float xPosition = originPosition.x + j * _cellSize;
            Vector3 startPos = new Vector3(xPosition, originPosition.y, originPosition.z);
            Vector3 endPos = new Vector3(xPosition, originPosition.y, originPosition.z + _rows * _cellSize);
            Gizmos.DrawLine(startPos, endPos);
        }
    }
}

