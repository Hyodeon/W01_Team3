using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private GameObject laserObject;

    private float maxDistance = 100f;
    private LayerMask layerMask;

    private Vector2 origin;
    private Vector2 direction;
    public string type;

    public GameObject laserPrefab;

    private float distance = -1f;
    public float Distance { get { return distance; } }

    void Start()
    {

        origin = type switch
        {
            "up" => new Vector2(transform.position.x, transform.position.y - 0.5f),
            "down" => new Vector2(transform.position.x, transform.position.y + 0.5f),
            "left" => new Vector2(transform.position.x - 0.5f, transform.position.y),
            "right" => new Vector2(transform.position.x + 0.5f, transform.position.y),
            _ => Vector2.zero
        };

        direction = type switch
        {
            "up" => Vector2.down,
            "down" => Vector2.up,
            "left" => Vector2.right,
            "right" => Vector2.left,
            _ => Vector2.zero
        };

        Debug.Log(direction);

        GenerateLaser();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateLaser()
    {
        if (laserObject != null) Destroy(laserObject);

        RaycastHit2D hit = Physics2D.Raycast(origin, direction,
             maxDistance, ~0);

        if (hit.collider != null)
        {
            distance = hit.distance;
        }

        GameObject instance = Instantiate(laserPrefab, Vector2.zero, Quaternion.identity);

        Vector2 scale = instance.transform.localScale;

        scale.x = distance;

        instance.transform.localScale = scale;

        Vector2 midPoint = (origin + hit.point) / 2;

        instance.transform.position = midPoint;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(origin, origin + (Vector2)direction * maxDistance);
    }
}
