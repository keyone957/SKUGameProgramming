using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//NPC클릭했을때 UI띄우기.
//인게임 메뉴가 활성화 되어있을때는 npc클릭 못하게
// 최초 작성자 : 홍원기
// 수정자 : 홍원기
// 최종 수정일 : 2024-05-11
public class NPCController : MonoBehaviour
{
    [SerializeField] private GameObject activeUI;
    [SerializeField] private string npcName;
    [SerializeField] private LayerMask layerMask;
    void Update()
    {
        if (AllSceneCanvas.instance.isOpenMenu == false)
        {
            OnClickNPC();
        }

    }

    private void OnClickNPC()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(pos, Vector2.zero);

            foreach (var hit in hits)
            {
                if (hit.transform != null && hit.transform.gameObject.name==npcName)
                {
                    activeUI.SetActive(true);
                }
            }
        }
    }
}
