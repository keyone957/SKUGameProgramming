using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

//������� ��Ÿ�� ���� ������Ʈ
//2�� ������ ��Ÿ�� Ȱ�� ��Ȱ�� �ݺ�
// �ۼ��� : ������
// ������ : ������
// ���� ������ : 2024-05-21

public class FlickeringTilemap : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private float activeDuration = 2.0f; // Ÿ�ϸ��� Ȱ��ȭ�Ǵ� �ð�
    [SerializeField] private float inactiveDuration = 2.0f; // Ÿ�ϸ��� ��Ȱ��ȭ�Ǵ� �ð�

    private TileBase[] originalTiles; // ���� Ÿ���� �����ϴ� �迭
    private BoundsInt bounds; // Ÿ�ϸ��� ����

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
            // Ÿ�ϸ� Ȱ��ȭ
            SetTilemapActive(true);
            yield return new WaitForSeconds(activeDuration);

            // Ÿ�ϸ� ��Ȱ��ȭ
            SetTilemapActive(false);
            yield return new WaitForSeconds(inactiveDuration);
        }
    }

    private void SetTilemapActive(bool isActive)
    {
        if (isActive)
        {
            // Ÿ�ϸ��� Ȱ��ȭ�մϴ�.
            tilemap.SetTilesBlock(bounds, originalTiles);
        }
        else
        {
            // Ÿ�ϸ��� ��Ȱ��ȭ�մϴ�.
            tilemap.ClearAllTiles();
        }
    }
}