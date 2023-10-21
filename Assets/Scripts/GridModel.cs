using System.Collections.Generic;
using UnityEngine;

public class GridModel
{
    private List<Vector3> _cellPostions;

    /// <summary>
    /// グリッド内にアイテムがあるかどうか
    /// </summary>
    private bool[] _existItem;
    public GridModel(int gridCount, List<Vector3> cellPostions)
    {
        _existItem = new bool[gridCount];
        _cellPostions = cellPostions;
    }

    /// <summary>
    /// グリッド内にアイテムを設置フラグ
    /// </summary>
    /// <param name="contactX">グリッドとアイテムが接触したx座標</param>
    /// <param name="contactZ">グリッドとアイテムが接触したz座標</param>
    public void SetItem(float contactX, float contactZ)
    {
        for(int i = 0; i < _cellPostions.Count; i++)
        {
            if(contactX == _cellPostions[i].x && contactZ == _cellPostions[i].z)
            {
                _existItem[i] = true;
            }
        }
    }
}
