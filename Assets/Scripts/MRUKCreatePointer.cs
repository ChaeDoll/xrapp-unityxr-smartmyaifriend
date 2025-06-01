using Meta.XR.MRUtilityKit;
using Oculus.Interaction;
using Oculus.Interaction.Surfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MRUKCreatePointer : MonoBehaviour
{
    private GameObject[] floors;
    public BoxPositioning boxPositioning;
    
    public void CreateFloorPointer()
    {
        floors = FindObjectsOfType<GameObject>().Where(obj => obj.name == "FLOOR_EffectMesh").ToArray();
        if (floors.Length != 0)
        {
            foreach (GameObject floor in floors)
            {
                var pointable = floor.AddComponent<PointableElement>();
                var colliderSurface = floor.AddComponent<ColliderSurface>();
                colliderSurface.InjectCollider(floor.GetComponent<Collider>());
                var rayInteractable = floor.AddComponent<RayInteractable>();
                rayInteractable.InjectSurface(colliderSurface);
                rayInteractable.InjectOptionalPointableElement(pointable);
                rayInteractable.WhenPointerEventRaised += HandlePointerEvent;
            }
        }
    }
    private void HandlePointerEvent(PointerEvent evt)
    {
        if (evt.Type == PointerEventType.Select)
        {
            boxPositioning.SelectPosition();
        }
    }
}
