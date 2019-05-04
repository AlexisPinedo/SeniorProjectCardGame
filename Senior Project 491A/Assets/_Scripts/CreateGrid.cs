using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGrid : MonoBehaviour
{
    //When incrementing by a value in other scripts reference the size value here for the increment
    [SerializeField] private float _size = 1f;
    public float size { get { return _size; } }

    [SerializeField] private int _xValUnits = 1;
    public int xValUnits { get { return _xValUnits; } }

    [SerializeField] private int _yValUnits = 1;
    public int yValUnits { get { return _yValUnits; } }

    [SerializeField] private Color GizmoColor;

    public Dictionary<Vector2, bool> objectPlacements = new Dictionary<Vector2, bool>();

    private void Awake()
    {
        Gizmos.color = GizmoColor;
        if (size > .99)
        {
            InitializePlacements();
        }
        else
        {
            Debug.Log("Size Cannot be less then 1");
        }
    }

    public Vector2 GetNearestPointOnGrid(Vector2 position)
    {
        int xCount = Mathf.RoundToInt(position.x / size);
        int yCount = Mathf.RoundToInt(position.y / size);

        Vector2 result = new Vector2((float)xCount * size, (float)yCount * size);

        result += new Vector2(transform.position.x, transform.position.y);

        return result;
    }

    private void OnDrawGizmos()
    {
        float xTotal = size * _xValUnits;
        int yTotal = (int)size * _yValUnits;

        Gizmos.color = GizmoColor;
        if (size > .99)
        {
            for (float x = 0; x < xTotal; x += size)
            {
                for (float y = 0; y < yTotal; y += size)
                {
                    var point = GetNearestPointOnGrid(new Vector2(x, y));
                    Gizmos.DrawSphere(point, 0.1f);
                }
            }
        }
        else
        {
            Debug.Log("Size Cannot be less then 1");
        }
    }

    /* Initialize the Vector2 spaces as false, i.e., the spaces are empty */
    private void InitializePlacements()
    {
        int xTotal = (int)size * _xValUnits;
        int yTotal = (int)size * _yValUnits;

        for (float x = 0; x < xTotal; x += size)
        {
            for (float y = 0; y < yTotal; y += size)
            {
                Vector2 point = GetNearestPointOnGrid(new Vector2(x, y));
                objectPlacements.Add(point, false);
            }
        }
    }

    public void SetObjectPlacement(Vector2 position, bool value = true)
    {
        objectPlacements[position] = value;
    }

    public bool IsPlaceable(Vector2 location)
    {
        if (objectPlacements[location] == false)
        {
            return true;
        }

        return false;
    }

    public bool ResizeGrid(float newSize, int newX)
    {
        Debug.Log("Resizing grid");

        return true;
    }
}
