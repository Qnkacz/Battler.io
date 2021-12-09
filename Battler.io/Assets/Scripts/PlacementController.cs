using System;
using Battle.Unit;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlacementController : MonoBehaviour
    {
        public GameObject currDraggableObject;
        private void Update()
        {
            Ray raycast = Camera.main!.ScreenPointToRay(Input.mousePosition);
            OnClickDown(raycast);
            OnClickUp();
            OnMouseMove(raycast);
        }

        private void OnMouseMove(Ray rayCast)
        {
            if (currDraggableObject != null)
            {
                if (Physics.Raycast(rayCast, out var hit))
                {
                    if (hit.collider.CompareTag("Ground"))
                    {
                        currDraggableObject.transform.position = hit.point;
                    }
                }
            }
        }

        private void OnClickDown(Ray rayCast)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(rayCast, out var hit))
                {
                    if (hit.collider.CompareTag("Spawner") && currDraggableObject == null)
                    {
                        currDraggableObject = hit.collider.gameObject;
                        var script = currDraggableObject.GetComponent<Spawner>();
                        script.IsDragged = true;
                        hit.collider.enabled = false;
                    }
                }
            }
        }

        private void OnClickUp()
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (currDraggableObject == null) return;
                currDraggableObject.GetComponent<BoxCollider>().enabled = true;
                currDraggableObject.GetComponent<Spawner>().IsDragged = false;
                currDraggableObject = null;
            }
            
        }
    }
}