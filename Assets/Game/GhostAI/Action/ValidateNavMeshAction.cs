using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Validate NavMesh", story: "Validate NavMesh from [AI]", category: "Action", id: "e69904020680643a6f57de624049e938")]
public partial class ValidateNavMeshAction : Action
{
    [SerializeReference] public BlackboardVariable<GhostAIController> AI;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        // Mengecek apakah ada reference ke Ghost AI Controller
        // Jika tidak ada langsung mengembalikan status gagal
        if (AI.Value == null)
        {
            return Status.Failure;
        }

        // Mengecek apakah ada reference ke NavMeshAgent
        // Jika tidak ada langsung mengembalikan status gagal
        if (AI.Value.NavMeshAgent == null)
        {
            return Status.Failure;
        }

        // Mengecek NavMeshAgent tidak aktif
        // Jika iya langsung mengembalikan status gagal
        if (AI.Value.NavMeshAgent.isActiveAndEnabled == false)
        {
            return Status.Failure;
        }

        // Mengecek agen navmesh tidak di area navmesh
        // Jika iya langsung mengembalikan status gagal
        if (AI.Value.NavMeshAgent.isOnNavMesh == false)
        {
            return Status.Failure;
        }

        // Jika reference ke Ghost AI Controller,
        // reference ke NavMeshAgent, NavMeshAgent aktif, 
        // dan agen NavMesh ada di area NavMesh
        // maka kembalikan status success dan lanjut ke node berikutnya
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

