using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Laser : MonoBehaviour
{
    private GameObject laserObject;
    public GameObject LaserObject {  get => laserObject; }

    private LaserBeam laserBeam;

    private float maxDistance = 400f;

    private Vector2 origin;
    private Vector2 direction;
    public string type;
    public int id;
    public GameObject map;

    private float bias = -0.5f;

    private bool _state;
    // �Ӽ��� ���� ������ ����
    public bool State
    {
        get { return _state; }
        set
        {
            _state = value;
        }
    }

    public GameObject laserPrefab;

    private float distance = -1f;
    public float Distance { get { return distance; } }

    public void Initialize()
    {
        origin = type switch
        {
            "up" => new Vector2(transform.position.x, transform.position.y + bias),
            "down" => new Vector2(transform.position.x, transform.position.y - bias),
            "left" => new Vector2(transform.position.x - bias, transform.position.y),
            "right" => new Vector2(transform.position.x + bias, transform.position.y),
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

        GenerateLaser();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateLaser()
    {
        bool isFarAway = false;

        origin = type switch
        {
            "up" => new Vector2(transform.position.x, transform.position.y + bias),
            "down" => new Vector2(transform.position.x, transform.position.y - bias),
            "left" => new Vector2(transform.position.x - bias, transform.position.y),
            "right" => new Vector2(transform.position.x + bias, transform.position.y),
            _ => Vector2.zero
        };

        // �ʱ� ���� (������ �����ϸ� ���ְ� �ٽ� Ž��)
        if (laserObject != null) Destroy(laserObject);

        LayerMask layerMask = 1 << LayerMask.NameToLayer("Default");

        // 1. ������ �Ÿ� ���
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, maxDistance);

        if (hit.collider != null)
        {
            Debug.Log($"{hit.collider.name} ������Ʈ���� Ž�� �Ϸ�. ������ �߻� �غ�!");
            distance = hit.distance;
        }
        else
        {
            isFarAway = true;
            distance = 100f;
        }


        // 2. ������ ������Ʈ ���� (���� ����, ȸ��)
        GameObject instance = Instantiate(laserPrefab, Vector2.zero, Quaternion.identity);

        Vector2 scale = instance.transform.localScale;
        scale.x = distance;
        instance.transform.localScale = scale;

        if (type is "up" or "down")
        {
            instance.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
        }


        // 3. ������ ������Ʈ ��ġ
        Vector2 midPoint = !isFarAway ? (origin + hit.point) / 2 :
            (2 * origin + (direction * distance)) / 2;

        Debug.Log($"{origin}�� {hit.point}");
        instance.transform.position = midPoint;


        // 4. ��迡 ������ Bind
        laserObject = instance;
        if (laserObject == null) Debug.Log("�ν��Ͻ� ���� ����");

        laserObject.transform.SetParent(transform);

        laserBeam = laserObject.GetOrAddComponent<LaserBeam>();
        laserBeam.Initialize(gameObject);

        if (!_state) laserBeam.gameObject.SetActive(false);
        else laserBeam.gameObject.SetActive(true);
    }

    public void ModifyLaser(bool state)
    {

        if (!_state) laserBeam.gameObject.SetActive(false);
        else laserBeam.gameObject.SetActive(true);
    }

    public void dump()
    {
        origin = type switch
        {
            "up" => new Vector2(transform.position.x, transform.position.y + bias),
            "down" => new Vector2(transform.position.x, transform.position.y - bias),
            "left" => new Vector2(transform.position.x - bias, transform.position.y),
            "right" => new Vector2(transform.position.x + bias, transform.position.y),
            _ => Vector2.zero
        };
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        // Ray�� ���� ������ ���� ����
        origin = type switch
        {
            "up" => new Vector2(transform.position.x, transform.position.y + bias),
            "down" => new Vector2(transform.position.x, transform.position.y - bias),
            "left" => new Vector2(transform.position.x + bias, transform.position.y),
            "right" => new Vector2(transform.position.x - bias, transform.position.y),
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

        // ������ ��θ� �ʷϻ����� ǥ��
        Gizmos.color = Color.green;

        // Raycast ����
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, maxDistance);

        if (hit.collider != null)
        {
            // Raycast�� �浹�� ���
            Gizmos.DrawLine(origin, hit.point);
            Gizmos.DrawSphere(hit.point, 0.1f);
        }
        else
        {
            // Raycast�� �浹���� ���� ���
            Gizmos.DrawLine(origin, origin + direction * maxDistance);
        }
    }
}
