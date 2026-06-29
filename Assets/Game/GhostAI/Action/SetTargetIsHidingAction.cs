using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Set TargetIsHiding", story: "Set [TargetIsHiding] from [AI]", category: "Action/Blackboard", id: "d18e73147be0762dc89d312269c20e4f")]
public partial class SetTargetIsHidingAction : Action
{
    [SerializeReference] public BlackboardVariable<bool> TargetIsHiding;
    [SerializeReference] public BlackboardVariable<GhostAIController> AI;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        // Jika tidak ada reference ke AI Controller,
        // dan PlayerCharacter maka langsung kembalikan status gagal
        if (AI.Value == null && AI.Value.Target == null)
        {
            return Status.Failure;
        }
        // Mengisikan nilai variable TargetIsHiding dengan IsHiding
        // dari module PlayerCharacter yang diakses dari AI Controller
        TargetIsHiding.Value = AI.Value.Target.IsHiding;
        // Setelah selesai mengisikan nilai variable TargetIsHiding
        // maka kembalikan status success dan lanjut ke node berikutnya
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

