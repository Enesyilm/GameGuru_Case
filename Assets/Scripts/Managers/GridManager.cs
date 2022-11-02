using System;
using System.Collections;
using System.Collections.Generic;
using Controller;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    #region Self Variables

    #region Public Variables

    

    #endregion

    #region Serialized Variables

    [SerializeField] private int GridSize;
    [SerializeField] private float offsetAmount;
    [SerializeField] private GridSpawnController _gridSpawnController;
    

    #endregion

    #region Private Variables

    private GameObject[,] GridArray;

    #endregion

    #endregion

    void Start()
    {
        OnCreateGrids(GridSize);
        CalculateBounds();
    }

    public void CalculateBounds()
    {
            float boundValue=_gridSpawnController.gridPrefab.GetComponent<SpriteRenderer>().bounds.size.x/2*Screen.height/Screen.width*.5f;
            var _cameraBound=new Bounds();
            Camera.main.orthographicSize=GridSize;
         //foreach(var gridTile in _gridSpawnController._gridList) _cameraBound.Encapsulate(gridTile.GetComponent<Sprite>().bounds);
        //_cameraBound.Expand(b);
    }
    public void OnCreateGrids(int gridSize)
    {
       Vector3 _posIterator = Vector3.zero;
        GridArray = new GameObject[gridSize,gridSize];
        for (int index = 0; index < gridSize; index++)
        {
            for (int rowIndex = 0; rowIndex < gridSize; rowIndex++)
            {
                _posIterator = Vector3.zero + new Vector3(offsetAmount * index, offsetAmount * rowIndex, 0);
                _gridSpawnController.GetFromPool(_posIterator);
            }
        }
            

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
