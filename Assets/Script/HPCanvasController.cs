using UnityEngine;

public class HPCanvasController : MonoBehaviour
{
    private void Update()
    {
        this.transform.rotation = Camera.main.transform.rotation;
    }
}
