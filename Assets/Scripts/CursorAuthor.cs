using System;
using Unity.Entities;
using UnityEngine;

public class CursorAuthor : MonoBehaviour
{
    [SerializeField] CursorModeStyle[] CursorModeStyles;
    
    class CursorAuthorBaker : Baker<CursorAuthor>
    {
        public override void Bake(CursorAuthor authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Renderable);
            AddComponent(entity, new CursorSelection{Mode = CursorSelection.CursorMode.Select});
            AddComponentObject(entity, new CursorManagedData { CursorModeStyles = authoring.CursorModeStyles });
        }
    }
}

class CursorManagedData : IComponentData
{
    public CursorModeStyle[] CursorModeStyles;
}

[Serializable]
struct CursorModeStyle
{
    public Sprite Default;
    public Sprite DefaultOnHover;
    public Sprite TileSelect;
    public Sprite TileSelectOnHover;
    public Sprite GridSelect;
    public Sprite GridSelectOnHover;
    public Sprite SlimeSelect;
    public Sprite SlimeSelectOnHover;
}