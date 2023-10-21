using System.Collections.Generic;
using UnityEngine;

public class GridModel
{
    /// <summary>
    /// グリッドの中点リスト
    /// </summary>
    private List<Vector3> _cellPostions;

    /// <summary>
    /// グリッド内にアイテムがあるかどうか
    /// </summary>
    private string[] _existItem;
    public GridModel(int gridCount, List<Vector3> cellPostions)
    {
        _existItem = new string[gridCount];
        _cellPostions = cellPostions;
    }

    /// <summary>
    /// グリッド内にアイテムを設置フラグ
    /// </summary>
    /// <param name="contactX">グリッドとアイテムが接触したx座標</param>
    /// <param name="contactZ">グリッドとアイテムが接触したz座標</param>
    /// <param name="itemName">アイテム名</param>
    public void SetItem(float contactX, float contactZ, string itemName)
    {
        for(int i = 0; i < _cellPostions.Count; i++)
        {
            if(contactX == _cellPostions[i].x && contactZ == _cellPostions[i].z)
            {
                _existItem[i] = itemName;
            }
        }
    }

    public void GetItem(string itemName)
    {
        for(int i = 0; i < _existItem.Length; i++)
        {
            if (_existItem[i] == itemName)
            {
                _existItem[i] = null;
            }
        }

        //foreach(var i in _existItem)
        //{
        //    Debug.Log(i);
        //}
    }
}
