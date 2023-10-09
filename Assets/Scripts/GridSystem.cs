using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    #region SerializeField
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
    #endregion SerializeField

    #region private変数
    /// <summary>
    /// グリッドの原点位置
    /// </summary>
    private Vector3 originPosition;

    /// <summary>
    /// グリッドの中点リスト
    /// </summary>
    private List<Vector3> _cellPositions = new List<Vector3>();
    #endregion private変数

    #region public変数
    /// <summary>
    /// グリッドの中点リスト
    /// </summary>
    public List<Vector3> CellPostions => _cellPositions;
    #endregion public変数

    private void Start()
    {
        Vector3 offset = new Vector3(-_columns * _cellSize * 0.5f, _cellSize * 0.5f, -_rows * _cellSize * 0.5f);
        originPosition = transform.position + offset;
        CreateGrid();
    }

    /// <summary>
    /// グリッドの作成
    /// </summary>
    private void CreateGrid()
    {
        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _columns; j++)
            {
                _cellPositions.Add(GetCellPosition(i, j));
            }
        }
    }

    /// <summary>
    /// 指定位置から最も近いグリッドの中点を返す
    /// </summary>
    /// <param name="position">指定位置</param>
    /// <returns></returns>
    public Vector3 GetClosestPos(Vector3 position)
    {
        Vector3 closestPoint = _cellPositions[0];
        float closestDistance = Vector3.Distance(position, closestPoint);
        foreach(var cellPosition in _cellPositions)
        {
            float distance = Vector3.Distance(position, cellPosition);
            if(distance < closestDistance)
            {
                closestDistance = distance;
                closestPoint = cellPosition;
            }
        }
        return closestPoint;
    }

    /// <summary>
    /// セルの中心を取得
    /// </summary>
    /// <param name="row">行</param>
    /// <param name="column">列</param>
    /// <returns></returns>
    private Vector3 GetCellPosition(int row, int column)
    {
        float xPosition = originPosition.x + (_cellSize * column) + _cellSize / 2;
        float zPosition = originPosition.z + (_cellSize * row) + _cellSize / 2;
        return new Vector3(xPosition, originPosition.y, zPosition);
    }

    private void PlaceItem(Vector3 position)
    {
        Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere), position, Quaternion.identity);
    }

    /// <summary>
    /// スクリプトがGameObjectに設定されているとき
    /// </summary>
    private void OnDrawGizmos()
    {
        // グリッドの色を設定
        Gizmos.color = Color.green;

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

