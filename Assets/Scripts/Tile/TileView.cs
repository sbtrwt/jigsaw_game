using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JigsawGame.Tile
{
    public class TileView : MonoBehaviour
    {
        private TileController controller;
        [SerializeField] private SpriteRenderer spriteRenderer;

        public void SetController(TileController controller)
        {
            this.controller = controller;
        }

        public void SetRenderer(Sprite spriteToSet) => spriteRenderer.sprite = spriteToSet;
    }
}