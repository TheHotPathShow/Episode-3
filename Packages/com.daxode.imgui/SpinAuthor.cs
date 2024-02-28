using com.daxode.imgui;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class SpinAuthor : MonoBehaviour
{
    [SerializeField]
    float degsPerSecond  = 45f;

    class SpinAuthorBaker : Baker<SpinAuthor>
    {
        public override void Bake(SpinAuthor authoring)
        {
            var e = GetEntity(TransformUsageFlags.Dynamic);
            var speed = new MySpeed { RadiansPerSecond = math.radians(authoring.degsPerSecond) };
            AddComponent(e, speed);
        }
    }
}

internal struct MySpeed : IComponentData
{
    public float RadiansPerSecond;
}

partial struct RotationSystem : ISystem
{
    public void OnCreate(ref SystemState state) 
        => state.RequireForUpdate<ImGuiSystem.Singleton>();

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        ImGui.Begin("RotationSystem");
        foreach (var (lt, speed) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<MySpeed>>())
        {
            ImGui.SliderFloat($"Pos: {lt.ValueRO.Position}", ref speed.ValueRW.RadiansPerSecond, 0, 2 * math.PI);
            lt.ValueRW = lt.ValueRO.RotateY(speed.ValueRO.RadiansPerSecond * SystemAPI.Time.DeltaTime);
        }
        ImGui.End();
    }
}