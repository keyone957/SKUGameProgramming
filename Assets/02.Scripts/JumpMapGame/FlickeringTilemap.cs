using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

//사라지는 맵타일 관련 컴포넌트
//2초 단위로 맵타일 활성 비활성 반복
// 작성자 : 장현우
// 수정자 : 장현우
// 최종 수정일 : 2024-05-21

public class FlickeringTilemap : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private float activeDuration = 2.0f; // 타일맵이 활성화되는 시간
    [SerializeField] private float inactiveDuration = 2.0f; // 타일맵이 비활성화되는 시간

    private TileBase[] originalTiles; // 원래 타일을 저장하는 배열
    private BoundsInt bounds; // 타일맵의 범위

    private void Start()
    {
        if (tilemap == null)
        {
            tilemap = GetComponent<Tilemap>();
        }

        bounds = tilemap.cellBounds;
        originalTiles = tilemap.GetTilesBlock(bounds);

        StartCoroutine(FlickerTilemap());
    }

    private IEnumerator FlickerTilemap()
    {
        while (true)
        {
            // 타일맵 활성화
            SetTilemapActive(true);
            yield return new WaitForSeconds(activeDuration);

            // 타일맵 비활성화
            SetTilemapActive(false);
            yield return new WaitForSeconds(inactiveDuration);
        }
    }

    private void SetTilemapActive(bool isActive)
    {
        if (isActive)
        {
            // 타일맵을 활성화합니다.
            tilemap.SetTilesBlock(bounds, originalTiles);
        }
        else
        {
            // 타일맵을 비활성화합니다.
            tilemap.ClearAllTiles();
        }
    }
}