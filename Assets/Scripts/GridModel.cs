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
    /// グリッド内にアイテムを設置
    /// </summary>
    /// <param name="contactPosition">アイテムがグリッドに接触した位置</param>
    /// <param name="itemName">アイテム名</param>
    public void SetItem(Vector3 contactPosition, string itemName)
    {
        for(int i = 0; i < _cellPostions.Count; i++)
        {
            if (contactPosition == _cellPostions[i])
            {
                Debug.Log(itemName);
                _existItem[i] = itemName;
            }
        }
    }

    /// <summary>
    /// グリッド内からアイテムがなくなった時、グリッドを空にする
    /// </summary>
    /// <param name="itemName"></param>
    public void GetItem(string itemName)
    {
        for(int i = 0; i < _existItem.Length; i++)
        {
            if (_existItem[i] == itemName)
            {
                _existItem[i] = null;
            }
        }
    }
}
