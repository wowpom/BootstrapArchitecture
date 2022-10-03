using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace CodeBase.Logic
{
    [RequireComponent(typeof(BoxCollider))]
    public class SaveTrigger : MonoBehaviour
    {
        private ISaveLoadService _saveLoadServices;
        private BoxCollider _boxCollider;

        private void Awake()
        {
            _saveLoadServices = ServicesLocator.Container.Single<ISaveLoadService>();
            _boxCollider = GetComponent<BoxCollider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            _saveLoadServices.SaveProgress();
        }

        private void OnDrawGizmos()
        {
            if (!_boxCollider)
                _boxCollider = GetComponent<BoxCollider>();
            Gizmos.color = new Color32(20, 200, 30, 130);
            Gizmos.DrawCube(transform.position + _boxCollider.center, _boxCollider.size);
        }
    }
}
