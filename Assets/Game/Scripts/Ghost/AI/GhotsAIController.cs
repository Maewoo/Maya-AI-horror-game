using System.Collections;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class GhostAIController : MonoBehaviour
{
    // Variable untuk reference ke component BehaviorAgent
    [SerializeField]
    private BehaviorGraphAgent _behaviorGraphAgent;
    // Variable untuk reference ke component NavMeshAgent
    [SerializeField]
    private NavMeshAgent _navMeshAgent;
    // Variable untuk reference ke Player (Target)
    [SerializeField]
    private PlayerCharacter _target;
    // Variable untuk reference ke sistem Sight Perception
    [SerializeField]
    private SightPerception _sightPerception;

    // Property untuk mendapatkan nilai variable _behaviorGraphAgent
    public BehaviorGraphAgent BehaviorGraphAgent => _behaviorGraphAgent;
    // Property untuk mendapatkan nilai variable _navMeshAgent
    public NavMeshAgent NavMeshAgent => _navMeshAgent;
    // Property untuk mendapatkan nilai variable _target
    public PlayerCharacter Target => _target;
    // Property untuk mendapatkan nilai variable _sightPerception
    public SightPerception SightPerception => _sightPerception;

    // Membuat event yang akan dipanggil ketika AI di-despawn
    public UnityEvent OnDespawn;

    // Function untuk melakukan despawn AI (menyembunyikan/me-nonaktifkan)
    public void Despawn()
    {
        // Menjalankan coroutine, untuk menunggu sampai akhir frame
        // baru me-nonaktifkan AI
        StartCoroutine(DespawnAfterEndOfFrame());
    }

    private IEnumerator DespawnAfterEndOfFrame()
    {
        // Memastikan ada reference ke behavior graoh agent
        if (_behaviorGraphAgent != null)
        {
            // melakukan reset variable blackboard CanSeeTarget
            // dari blackboard behavior graph BT_ChasingGhost
            // supaya ketika respawn, status target kembali
            // menjadi tidak terlihat
            _behaviorGraphAgent.SetVariableValue("CanSeeTarget", false);
            // menonaktifkan behavior graph, 
            // supaya alur perilaku AI berhenti ketika
            // AI nonaktif
            _behaviorGraphAgent.enabled = false;
        }

        // Memastikan ada reference ke NavMeshAgent dan AI ada
        // di dalam area navmesh
        if (_navMeshAgent != null && _navMeshAgent.isOnNavMesh == true)
        {
            // Mengosongkan jalur AI, untuk AI tidak bergerak
            // ketika akan di-nonaktifkan
            _navMeshAgent.ResetPath();
            // Menonaktifkan navmesh agent, supaya AI tidak
            // bergerak ketika AI nonaktif
            _navMeshAgent.enabled = false;
        }

        // Memanggil event OnDespawn untuk module lain
        // jika ingin melakukan sesuatu ketika AI di-despawn
        OnDespawn?.Invoke();
        // Menunggu sampai akhir frame, 
        // memastikan node action di dalam behavior graph
        // telah mengembalikan status success/failed
        yield return new WaitForEndOfFrame();
        // Menonaktifkan object di akhir frame
        gameObject.SetActive(false);
    }
}