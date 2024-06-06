using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace JigsawGame.Tile
{
    public class TileSorting
    {
        private List<SpriteRenderer> allSprites;

        public TileSorting()
        {
            allSprites = new List<SpriteRenderer>();
        }

        public void Clear()
        {
            allSprites.Clear();
        }

        public void Add(SpriteRenderer renderer)
        {
            allSprites.Add(renderer);
            SetRenderOrder(renderer, allSprites.Count);
        }

        public void Remove(SpriteRenderer renderer)
        {
            allSprites.Remove(renderer);
            for (int i = 0; i < allSprites.Count; i++)
            {
                SetRenderOrder(allSprites[i], i + 1);
            }
        }

        public void BringToTop(SpriteRenderer renderer)
        {
            Remove(renderer);
            Add(renderer);
        }

        private void SetRenderOrder(SpriteRenderer renderer, int index)
        {
            renderer.sortingOrder = index;
            Vector3 p = renderer.transform.position;
            p.z = -index / 5.0f;
            renderer.transform.position = p;
        }
    }
}
