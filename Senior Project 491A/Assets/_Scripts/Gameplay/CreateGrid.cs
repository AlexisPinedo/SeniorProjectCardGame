using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TODO
/// </summary>
public class CreateGrid : MonoBehaviour
{
    //When incrementing by a value in other scripts reference the size value here for the increment
    [SerializeField, Range(1f, 100f)]
    private float _size = 1f;

    public float size
    {
        get => _size;
        set
        {
            _size = Mathf.Clamp(value, 0, 100); 
            InitializePlacements();
        }
    }

    [SerializeField] private int _xValUnits = 1;

    public int xValUnits
    {
        get => _xValUnits;
        set
        {
            _xValUnits = value;
            InitializePlacements();
        }
    }

    [SerializeField] private int _yValUnits = 1;

    public int yValUnits
    {
        get => _yValUnits;
        set
        {
            _yValUnits = value;
            InitializePlacements();
        }
    }

    [SerializeField] private Color GizmoColor;

    public Stack<Vector2> freeLocations = new Stack<Vector2>();
    public Dictionary<Vector2, PlayerCardContainer> cardLocationReference = new Dictionary<Vector2, PlayerCardContainer>();

    private void Awake()
    {
        cardLocationReference.Clear();

        Gizmos.color = GizmoColor;
        if (size > .99)
        {
            InitializePlacements();
        }
        else
        {
            //Debug.Log("Size Cannot be less then 1");
        }
    }

    private void OnEnable()
    {
        HandContainer.CardDrawn += SetCardPlacement;
    }

    private void OnDisable()
    {
        HandContainer.CardDrawn -= SetCardPlacement;

    }

    /// <summary>
    /// Gets the nearest Vector2 position on the grid.
    /// </summary>
    /// <param name="position">Reference position</param>
    /// <returns></returns>
    public Vector2 GetNearestPointOnGrid(Vector2 position)
    {
        int xCount = Mathf.RoundToInt(position.x / size);
        int yCount = Mathf.RoundToInt(position.y / size);

        Vector2 result = new Vector2((float)xCount * size, (float)yCount * size);

        result += new Vector2(transform.position.x, transform.position.y);

        return result;
    }

    /// <summary>
    /// TODO
    /// </summary>
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
            //Debug.Log("Size Cannot be less then 1");
        }
    }

    /// <summary>
    /// Initializes the Vector2 spaces as false, i.e., the spaces are empty.
    /// </summary>
    private void InitializePlacements()
    {
        float xTotal = size * _xValUnits;   // Keep as float to allow grid resizing
        int yTotal = (int)size * _yValUnits;

        for (float x = 0; x < xTotal; x += size)
        {
            for (float y = 0; y < yTotal; y += size)
            {
                Vector2 point = GetNearestPointOnGrid(new Vector2(x, y));
                freeLocations.Push(point);
            }
        }
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="position"></param>
    /// <param name="value"></param>
    public void SetCardPlacement(PlayerCardContainer cardContainer)
    {
        Vector2 freeLocation = freeLocations.Pop();
        cardLocationReference.Add(freeLocation, cardContainer);
        cardContainer.gameObject.transform.position = freeLocation;
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="newSize"></param>
    /// <param name="newX"></param>
    public void ResizeGrid(float newSize, int newX)
    {
        //Debug.Log("Resizing grid");

        string parsed = newSize.ToString("0.000");    // Truncate at thousandths
        _size = float.Parse(parsed);
        _xValUnits = newX;

        InitializePlacements();
    }

}
