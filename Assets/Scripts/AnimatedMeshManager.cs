using UnityEngine;

namespace DefaultNamespace
{
    public class AnimatedMeshManager : MonoBehaviour
    {
        public SkinnedMeshRenderer Renderer;
        public MeshCollider Collider;

        void Update()
        {
            Mesh temp = new Mesh();
            Renderer.BakeMesh(temp);
            Collider.sharedMesh = null;
            Collider.sharedMesh = temp;
        }
    }
}