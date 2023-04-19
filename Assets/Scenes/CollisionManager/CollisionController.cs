using UnityEngine;

public class CollisionController : MonoBehaviour
{
    public Shape shape;

    public void Start()
    {
        CreateShape();
    }

    public void Update()
    {
        UpdateShape();
    }

    protected virtual void CreateShape()
    {

    }

    protected virtual void UpdateShape()
    {

    }

}