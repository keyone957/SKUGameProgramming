using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HideTileMap : MonoBehaviour
{  
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private float fadeDuration = 3.0f;  // 페이드 아웃 지속 시간
    private bool isFadingOut = false;  
    private bool soundPlayed = false;  
    private void Update()
    {
        if (DungeonSystem.instance.monsterCnt == 1 && !isFadingOut)
        {
            if (!soundPlayed)
            {
                SoundManager._instance.PlaySound(Define._hide);
                soundPlayed = true; 
            }
            StartCoroutine(FadeOutTilemap());
        }
    }

    private IEnumerator FadeOutTilemap()
    {
        isFadingOut = true;  // 페이드 아웃 시작 플래그 설정
        float elapsed = 0f;
        Color originalColor = tilemap.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            tilemap.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        tilemap.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        tilemap.gameObject.SetActive(false);
        isFadingOut = false; 
    }
}
