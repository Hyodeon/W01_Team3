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
    // 속성을 통해 변수를 감시
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

        // 초기 점검 (레이저 존재하면 없애고 다시 탐색)
        if (laserObject != null) Destroy(laserObject);

        LayerMask layerMask = 1 << LayerMask.NameToLayer("Default");

        // 1. 레이저 거리 재기
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, maxDistance);

        if (hit.collider != null)
        {
            Debug.Log($"{hit.collider.name} 오브젝트까지 탐색 완료. 레이저 발사 준비!");
            distance = hit.distance;
        }
        else
        {
            isFarAway = true;
            distance = 100f;
        }


        // 2. 레이저 오브젝트 생성 (길이 조절, 회전)
        GameObject instance = Instantiate(laserPrefab, Vector2.zero, Quaternion.identity);

        Vector2 scale = instance.transform.localScale;
        scale.x = distance;
        instance.transform.localScale = scale;

        if (type is "up" or "down")
        {
            instance.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
        }


        // 3. 레이저 오브젝트 배치
        Vector2 midPoint = !isFarAway ? (origin + hit.point) / 2 :
            (2 * origin + (direction * distance)) / 2;

        Debug.Log($"{origin}과 {hit.point}");
        instance.transform.position = midPoint;


        // 4. 기계에 레이저 Bind
        laserObject = instance;
        if (laserObject == null) Debug.Log("인스턴스 생성 오류");

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

        // Ray의 시작 지점과 방향 설정
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

        // 레이저 경로를 초록색으로 표시
        Gizmos.color = Color.green;

        // Raycast 수행
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, maxDistance);

        if (hit.collider != null)
        {
            // Raycast가 충돌한 경우
            Gizmos.DrawLine(origin, hit.point);
            Gizmos.DrawSphere(hit.point, 0.1f);
        }
        else
        {
            // Raycast가 충돌하지 않은 경우
            Gizmos.DrawLine(origin, origin + direction * maxDistance);
        }
    }
}
