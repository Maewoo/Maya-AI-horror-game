using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Wait Until Reach Destination", story: "[AI] wait until reach destination", category: "Action", id: "f844cdeacc5598e1be47700bb19a2a43")]
public partial class WaitUntilReachDestinationAction : Action
{
    [SerializeReference] public BlackboardVariable<GhostAIController> AI;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        // Mengecek apakah ada reference AI Controller
        // kalau tidak ada langsung dianggap gagap
        if (AI.Value == null)
        {
            return Status.Failure;
        }

        // Mendapatkan reference ke NavMeshAgent 
        // dari AI Controller
        UnityEngine.AI.NavMeshAgent agent = AI.Value.NavMeshAgent;

        // Jika reference ke Nav Mesh Agent tidak ada
        // Maka akan langsung dianggap gagal
        if (agent == null)
        {
            return Status.Failure;
        }

        // Jika Nav Mesh Agent masih 
        // menghitung jalur ke last seen position
        // maka status nya akan dianggap masih berjalan
        // sehingga tidak akan lanjut ke node berikutnya
        if (agent.pathPending == true)
        {
            return Status.Running;
        }

        // Jika Nav jarak agent lebih besar dari jarak
        // stopping distance ditambahkan dengan distance threshold
        // maka status nya akan dianggap masih berjalan
        // sehingga tidak akan lanjut ke node berikutnya
        if (agent.remainingDistance > agent.stoppingDistance + 0.5)
        {
            return Status.Running;
        }

        // Jika tidak (sudah sampai tujuan) maka node status nya
        // akan dianggap berhasil dan selesai
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

