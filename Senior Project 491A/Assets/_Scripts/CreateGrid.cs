using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGrid : MonoBehaviour
{
    [SerializeField]
    private float size = 1f;

    [SerializeField] private int xValUnits = 1;
    
    [SerializeField] private int yValUnits = 1;
    
    
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
        Gizmos.color = Color.yellow;
        if (size > .99)
        {
            for (float x = 0; x < xValUnits; x += size)
            {
                for( float y = 0; y < yValUnits; y += size)
                {
                    var point = GetNearestPointOnGrid(new Vector3(x, y));
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
