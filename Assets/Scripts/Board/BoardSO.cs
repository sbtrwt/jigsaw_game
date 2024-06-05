using JigsawGame.Tile;
using System.Collections.Generic;
using UnityEngine;

namespace JigsawGame.Board
{
    [CreateAssetMenu(fileName = "BoardScriptableObject", menuName = "ScriptableObjects/BoardScriptableObject")]
    public class BoardSO : ScriptableObject
    {
        public TileView TilePrefab;
        public int RowCount;
        public int ColumnCount;
        public int HeightInPixel;
        public int WidthInPixel;
    
        public List<string> ImagesPath = new List<string>();

    }
}
