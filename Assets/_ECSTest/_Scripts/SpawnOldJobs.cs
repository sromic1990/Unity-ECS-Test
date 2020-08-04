using System;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;
using Random = UnityEngine.Random;

public class SpawnOldJobs : MonoBehaviour
{
    [Range(0, 15000)] [SerializeField] private int _maxSheep;
    [SerializeField] private GameObject _sheepPrefab;
    [SerializeField] private Transform _sheepHolder;
    private Transform[] _sheepTransforms;

    struct MoveJob : IJobParallelForTransform
    {
        public void Execute(int index, TransformAccess transform)
        {
            transform.position += 0.1f * (transform.rotation * new Vector3(0, 0 , 1));
            if (transform.position.z > 50)
            {
                transform.position = new Vector3(transform.position.x, 0, -50);
            }
        }
    }

    private MoveJob _moveJob;
    private JobHandle _moveHandle;
    private TransformAccessArray _transforms;
    
    private void Start()
    {
        _sheepTransforms = new Transform[_maxSheep];
        for (int i = 0; i < _maxSheep; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50));
            GameObject sheep = Instantiate(_sheepPrefab, pos, Quaternion.identity);
            sheep.transform.SetParent(_sheepHolder);
            _sheepTransforms[i] = sheep.transform;
        }
        _transforms = new TransformAccessArray(_sheepTransforms);
    }

    private void Update()
    {
        _moveJob = new MoveJob { };
        _moveHandle = _moveJob.Schedule(_transforms);
    }

    private void LateUpdate()
    {
        _moveHandle.Complete();
    }

    private void OnDestroy()
    {
        _transforms.Dispose();
    }
}
