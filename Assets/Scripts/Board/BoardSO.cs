using JigsawGame.Tile;
using UnityEngine;

namespace JigsawGame.Board
{
    [CreateAssetMenu(fileName = "BoardScriptableObject", menuName = "ScriptableObjects/BoardScriptableObject")]
    public class BoardSO : ScriptableObject
    {
        public TileView TilePrefab;

    }
}
