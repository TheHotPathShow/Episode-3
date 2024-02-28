using System;
using com.daxode.imgui;
using Unity.Entities;


[UpdateInGroup(typeof(InitializationSystemGroup))]
public partial struct ImGuiSystem : ISystem
{
    public struct Singleton : IComponentData {}

    public void OnUpdate(ref SystemState state)
    {
        // Start the Dear ImGui frame
        if (ImGuiHelper.NewFrameSafe())
        {
            if (!SystemAPI.HasSingleton<Singleton>())
                state.EntityManager.AddComponent<Singleton>(state.SystemHandle);
        } else if (SystemAPI.HasSingleton<Singleton>())
        {
            state.EntityManager.RemoveComponent<Singleton>(state.SystemHandle);
        }
    }
}