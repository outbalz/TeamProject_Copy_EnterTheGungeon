using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class CMapSpawnTest : MonoBehaviour
{
    
     
    Quaternion _quaternion;
    RaycastHit2D _hit;

    private void Update()
    {
        //sadf();
       if (Input.GetMouseButtonDown(0))
       {
           Vector3 vec3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            

            RaycastHit2D hit = Physics2D.Raycast(vec3, Vector3.zero);
             
       
           if(hit.collider != null)
           {
               
               Tilemap tile = hit.collider.GetComponent<Tilemap>();

                _quaternion = tile.transform.rotation;
                tile.transform.rotation = Quaternion.identity;

                Vector3Int cell = tile.WorldToCell(hit.point);
               
               tile.SetTile(cell, null);

                tile.RefreshAllTiles();

                tile.transform.rotation = _quaternion;

               
            }
       }
        
    }

    void sadf()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(point, Vector2.zero);

            if (hit.collider != null)
            {
                Tilemap tile = hit.collider.GetComponent<Tilemap>();

                if (tile != null)
                {

                    Matrix4x4 inverseMatrix = tile.transform.localToWorldMatrix.inverse;

                    // 박살난 hit.point를 회전하기 전의 원래 "순수한 평평한 2D 좌표"로 강제 복원합니다.
                    Vector3 cleanLocalPos = inverseMatrix.MultiplyPoint3x4(hit.point);

                    // 평평하게 복원된 좌표를 넣어야만 WorldToCell이 제정신을 차리고 작동합니다.
                    Vector3Int cell = tile.WorldToCell(cleanLocalPos);

                    // 타일 삭제
                    tile.SetTile(cell, null);

                    Debug.Log($"[정상 작동 확인] 복원된 셀 좌표: {cell}");
                }
            }
        }
    }
}


