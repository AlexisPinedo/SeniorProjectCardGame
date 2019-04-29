using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGrid : MonoBehaviour
{
    [SerializeField]
    private float size = 1f;

    [SerializeField]
    private int xValUnits = 1;
    
    [SerializeField]
    private int yValUnits = 1;

    [SerializeField]
    private Color GizmoColor; 

    
    
    public Vector2 GetNearestPointOnGrid(Vector2 position)
    {
        int xCount = Mathf.RoundToInt(position.x / size);
        int yCount = Mathf.RoundToInt(position.y / size);
        
        Vector2 result = new Vector2((float) xCount * size, (float) yCount * size);

        result += new Vector2(transform.position.x, transform.position.y);
        
        return result;
    }

    private void OnDrawGizmos()
    {
        int xTotal = (int)size * xValUnits;
        int yTotal = (int)size * yValUnits;

        Gizmos.color = GizmoColor;
        if (size > .99)
        {
            for (float x = 0; x < xTotal; x += size)
            {
                for( float  y = 0; y < yTotal; y += size)
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
}
