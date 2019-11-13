using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// The area of the screen where the parent object's Cards will be shown.
/// </summary>
public class CardGrid : MonoBehaviour
{
    // When incrementing by a value in other scripts reference the Size value here for the increment

    [SerializeField, Range(1f, 5f)] private float size = 1f;

    [SerializeField] private Color GizmoColor;

    public Stack<Vector2> freeLocations = new Stack<Vector2>();
    public Dictionary<Vector2, CardDisplay> cardLocationReference = new Dictionary<Vector2, CardDisplay>();

    [SerializeField] private int _xValUnits = 1;
    [SerializeField] private int _yValUnits = 1;

    public delegate void OnGridResize();

    public OnGridResize onGridResize;

    public float Size
    {
        get => size;
        set
        {
            size = Mathf.Clamp(value, 0, 5);
            ResizeGrid();
            onGridResize();
        }
    }

    public int xValUnits
    {
        get => _xValUnits;
        set
        {
            _xValUnits = value;
            InitializePlacements();
        }
    }

    public int yValUnits
    {
        get => _yValUnits;
        set
        {
            _yValUnits = value;
            InitializePlacements();
        }
    }

    private void Awake()
    {
        cardLocationReference.Clear();
        freeLocations.Clear();

        Gizmos.color = GizmoColor;
        if (Size > .99)
        {
            InitializePlacements();
        }
        else
        {
            // Size cannot be less than 1
        }
    }

    /// <summary>
    /// Initializes the Vector2 spaces as false, i.e., initialize the spaces as empty.
    /// </summary>
    private void InitializePlacements()
    {
        //Debug.Log("Initializing Placements");
        float xTotal = Size * _xValUnits; // Keep as float to allow grid resizing
        int yTotal = (int) Size * _yValUnits;

        for (float x = 0; x < xTotal; x += Size)
        {
            for (float y = 0; y < yTotal; y += Size)
            {
                Vector2 point = GetNearestPointOnGrid(new Vector2(x, y));
                freeLocations.Push(point);
            }
        }

        //Debug.Log("Elementds added to freelocations");
    }

    /// <summary>
    /// Gets the nearest Vector2 position on the grid.
    /// </summary>
    /// <param name="position">Reference position</param>
    /// <returns></returns>
    public Vector2 GetNearestPointOnGrid(Vector2 position)
    {
        int xCount = Mathf.RoundToInt(position.x / Size);
        int yCount = Mathf.RoundToInt(position.y / Size);

        Vector2 result = new Vector2((float) xCount * Size, (float) yCount * Size);

        result += new Vector2(transform.position.x, transform.position.y);

        return result;
    }

    /// <summary>
    /// TODO
    /// </summary>
    private void OnDrawGizmos()
    {
        float xTotal = Size * _xValUnits;
        int yTotal = (int) Size * _yValUnits;

        Gizmos.color = GizmoColor;
        if (Size > .99)
        {
            for (float x = 0; x < xTotal; x += Size)
            {
                for (float y = 0; y < yTotal; y += Size)
                {
                    var point = GetNearestPointOnGrid(new Vector2(x, y));
                    Gizmos.DrawSphere(point, 0.1f);
                }
            }
        }
        else
        {
            // Size cannot be less than 1
        }
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="newSize"></param>
    /// <param name="newX"></param>
    public void ResizeGrid()
    {
        Debug.Log("Resizing CardGrid");
        InitializePlacements();
    }

}
    