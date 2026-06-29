using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Set LastSeenTarget", story: "Set [LastSeenTarget] from [A]", category: "Action/Blackboard", id: "54daeb4136c5039ecebe60a04f2f446f")]
public partial class SetLastSeenTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<Vector3> LastSeenTarget;
    [SerializeReference] public BlackboardVariable<GhostAIController> A;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

