using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class CMapSpawnTest : MonoBehaviour
{
    [SerializeField]
    GameObject _room1;
    [SerializeField]
    GameObject _room2;
    [SerializeField]
    Tilemap _tilemap;
    // string _currentScene;
    //
    // void Start()
    // { 
    //     _currentScene = SceneManager.GetActiveScene().name;
    // }
    Collider2D _col2D;
    Vector3 size;
    Vector3 center;

    [SerializeField]
    int _seed = 100;

    [SerializeField]
    Vector3Int _vector3Int = new Vector3Int();
    [SerializeField]
    TileBase _tilebase;
    
    private void Start()
    {
        _col2D = _room1.GetComponentInChildren<TilemapCollider2D>();

        if( _col2D == null )
        {
            Debug.Log("콜라이더가 널");
        }
        Debug.Log(_col2D.GetInstanceID());

        

    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            //size = _col2D.bounds.size;
            //center = _col2D.bounds.center;
            //Debug.Log("사이즈와 위치 입력");
            //Debug.Log($"사이즈 : {size}");
            //Debug.Log($"센터 : {center}");

            //Random.InitState(_seed);
            //Debug.Log($"랜덤 : {Random.Range(1,10)}");
            TileChangeData a;

            _tilemap.SetTile(_vector3Int, _tilebase);
            //_tilemap.SetTiles();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            //Instantiate(_room2, center + size, rotation : Quaternion.identity);
        }
    }

}
