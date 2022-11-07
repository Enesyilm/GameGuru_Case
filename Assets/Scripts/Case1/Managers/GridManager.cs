using System.Collections.Generic;
using Case1.Controller;

using UnityEngine;
using Case1.Signals;

public class GridManager : MonoBehaviour
{
    
    #region Self Variables

    #region Public Variables
        public GridController[,] GridList;
        public int GridSize;
    #endregion

    #region Serialized Variables
        [SerializeField] private GridSpawnController _gridSpawnController;
        [SerializeField] private Transform gridHolder;
    #endregion

    #region Private Variables
        private int _matchCount=0;
        private List<GridController> _tempadjacentList=new List<GridController>();
    #endregion

    #endregion
    
    #region Event Subscription

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }
        
    private void UnSubscribeEvents()
    {
        GridSignals.Instance.onCreateNewGrid -= OnCreateGrids;
    }

    private void SubscribeEvents()
    {
        GridSignals.Instance.onCreateNewGrid += OnCreateGrids;
    }
    
    #endregion
    void Start()
    {
        CreateNewGrids(GridSize);
        CalculateBounds(GridSize);
    }

    public void CalculateBounds(int gridSize)
    {
        GridSize = gridSize;
        float boundValue=_gridSpawnController.gridPrefab.GetComponent<SpriteRenderer>().bounds.size.y*gridSize*Screen.height/Screen.width*.5f;
        Camera.main.orthographicSize=gridSize;
        
    }
    
    public void OnCreateGrids(int gridSize)
    {
        ResetPreviousGrids();
        CreateNewGrids(gridSize);
        CalculateBounds(gridSize);
    }

    private void CreateNewGrids(int gridSize)
    {
       
        GridController _currentGridController;
        Vector3 _posIterator = Vector3.zero;
        GridList = new GridController[gridSize,gridSize];
        Vector3 InitPos =(gridSize*.5f*new Vector3(1,1,0))-new Vector3(.5f,.5f,0);
        for (int rowIndex = 0; rowIndex < gridSize; rowIndex++)
        {
            for (int columnIndex = 0; columnIndex < gridSize; columnIndex++)
            {
                _posIterator =  new Vector3(rowIndex, columnIndex, 1)-InitPos;
                _currentGridController =_gridSpawnController.GetFromPool(_posIterator);
                _currentGridController.transform.parent = gridHolder;
                GridList[rowIndex, columnIndex] = _currentGridController;
                _currentGridController.GridManager = this;
                _currentGridController.CurrentIndex=new  Vector2(rowIndex,columnIndex);
            }
        }
    }

    public void ResetPreviousGrids()
    {
        foreach (var gridController in GridList)
        {
            gridController.ResetGrid();
            gridController.gameObject.SetActive(false);
        }
      
    }

    public void CheckAdjacentAmount(int xValue,int yValue)
    {
        int totalAdjacent=CountAdjacentAmount(xValue,yValue,_tempadjacentList);
        if (totalAdjacent >3)
        {
            ClearTempGrids();
        }
        _tempadjacentList.Clear();
    }

    private void ClearTempGrids()
    {
        _matchCount++;
        GridSignals.Instance.onIncreaseMatchCount?.Invoke(_matchCount);
        for (int index = 0; index < _tempadjacentList.Count; index++)
        {
            _tempadjacentList[index].ResetGrid();
        }
    }

    public int CountAdjacentAmount(int x,int y,List<GridController> tempAdjacentList)// Connected Cells Algorithm updated for this case requirements 
    {
        int _adjacentCount = 1;
        int _cornerGridValue;
        for (int row = x-1; row <  x+2; row++)
        {
            for (int col = y-1; col <  y+2; col++)
            {
                _cornerGridValue = Mathf.Abs((x + y) - (row + col));
                if (_cornerGridValue == 2 || _cornerGridValue == 0||(row+1)*(col+1) <= 0) continue;
                
                if (row>=GridSize||col>=GridSize||(x==row&&y==col))continue;

                if (tempAdjacentList.Contains(GridList[row, col]) || !GridList[row, col].IsGridClicked) continue;
               
                tempAdjacentList.Add(GridList[row,col]);
                _adjacentCount+=CountAdjacentAmount(row,col,tempAdjacentList);
            }
        }
        return _adjacentCount;
    }
}
