using UnityEngine;

public class HPCanvasController : MonoBehaviour
{
    [SerializeField] Transform _targetTransform;
    private void Update()
    {
        if (_targetTransform == null) return;
        this.transform.rotation = Camera.main.transform.rotation;
        this.transform.position = new Vector3(_targetTransform.position.x, _targetTransform.position.y + 1f, _targetTransform.position.z +1f);
    }
}
