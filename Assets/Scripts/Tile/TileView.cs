using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JigsawGame.Tile
{
    public class TileView : MonoBehaviour
    {
        private TileController controller;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private BoxCollider2D boxCollider2D;
        [SerializeField] private LayerMask layerMask;
        private Camera mainCamera;
        private Vector3 positionOffset = new Vector3(0.0f, 0.0f, 0.0f);
        private Vector3 previousPosition;
        private void Start()
        {
            mainCamera = Camera.main;
        }
        public void SetController(TileController controller)
        {
            this.controller = controller;
        }

        public void SetSpriteRenderer(Sprite spriteToSet) => spriteRenderer.sprite = spriteToSet;

        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                controller?.OnTileClickDown();
              
                if (controller.IsSelected)
                {
                    previousPosition = transform.position;
                    positionOffset = transform.position - mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f));
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                //Vector3 mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
                controller?.OnTileClickUp();
            }
#endif
#if UNITY_ANDROID
            if (Input.touchCount >= 1)
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    controller?.OnTileClickDown();
                    if (controller.IsSelected)
                    {
                        positionOffset = transform.position - mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f));
                    }
                }

                if (Input.touches[0].phase == TouchPhase.Ended)
                {
                    controller?.OnTileClickUp();
                }
            }
#endif
            if (controller.IsSelected)
            {
                Vector3 curPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f)) + positionOffset;
                //curPosition.z = 1;
                transform.position = curPosition;
            }

        }

        public bool ValidateClickAction()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
           
            if (hit.collider != null && boxCollider2D.Equals(hit.collider) )
            {
                return true;
            }
            return false;
        }
        public Collider2D GetOverlappedCollider()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
            return hit.collider;
        }

        public void SetColliderSize(Vector2 sizeToSet)
        {
            boxCollider2D.size = sizeToSet;
        }
        public void SetColliderOffset(Vector2 positionToSet)
        {
            boxCollider2D.offset = positionToSet;
        }
        public void SetPreviousPosition()
        {
            transform.position = previousPosition;
        }
        public SpriteRenderer GetSpriteRenderer() => spriteRenderer;
    }
}