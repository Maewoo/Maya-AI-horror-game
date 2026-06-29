using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Set CanSeeTarget", story: "Set [CanSeeTarget] from [AI]", category: "Action/Blackboard", id: "8db9629a6e6749476ecfd8c463db0df1")]
public partial class SetCanSeeTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<bool> CanSeeTarget;
    [SerializeReference] public BlackboardVariable<GhostAIController> AI;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        // Jika tidak ada reference ke AI Controller,
        // dan SightPerception maka langsung kembalikan status gagal
        if (AI.Value == null && AI.Value.SightPerception == null)
        {
            return Status.Failure;
        }
        // Mengisikan nilai variable CanSeeTarget dengan CanSeePlayer
        // dari module SightPerception yang diakses dari AI Controller
        CanSeeTarget.Value = AI.Value.SightPerception.CanSeePlayer;
        // Setelah selesai mengisikan nilai variable CanSeeTarget
        // maka kembalikan status success dan lanjut ke node berikutnya
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

