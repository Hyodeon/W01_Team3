using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private GameObject laserObject;
    public GameObject LaserObject {  get => laserObject; }

    private LaserBeam laserBeam;

    private float maxDistance = 100f;

    private Vector2 origin;
    private Vector2 direction;
    public string type;
    public int id;


    private bool _state;
    // �Ӽ��� ���� ������ ����
    public bool State
    {
        get { return _state; }
        set
        {
            ModifyLaser(value);
            _state = value;
        }
    }

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

        ModifyLaser(State);
    }

    public void Initialize()
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

        ModifyLaser(State);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateLaser()
    {
        // �ʱ� ���� (������ �����ϸ� ���ְ� �ٽ� Ž��)
        if (laserObject != null) Destroy(laserObject);

        LayerMask layerMask = 1 << LayerMask.NameToLayer("Default");

        // 1. ������ �Ÿ� ���
        RaycastHit2D hit = Physics2D.Raycast(origin, direction,
             maxDistance, layerMask);

        if (hit.collider != null)
        {
            Debug.Log($"{hit.collider.name} ������Ʈ���� Ž�� �Ϸ�. ������ �߻� �غ�!");
            distance = hit.distance;
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
        Vector2 midPoint = (origin + hit.point) / 2;
        instance.transform.position = midPoint;


        // 4. ��迡 ������ Bind
        laserObject = instance;
        if (laserObject == null) Debug.Log("�ν��Ͻ� ���� ����");

        laserObject.transform.SetParent(transform);

        laserBeam = laserObject.GetOrAddComponent<LaserBeam>();
        laserBeam.Initialize(gameObject);
    }

    public void ModifyLaser(bool state)
    {
        if (state) GenerateLaser();
        else Destroy(laserObject);
    }

}
