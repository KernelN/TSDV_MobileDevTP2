using UnityEngine;

namespace TheWasteland.Gameplay
{
    public class IKFootSolver : MonoBehaviour
    {
        [SerializeField] LayerMask terrainLayer = default;
        [SerializeField] Transform target = default;
        [SerializeField] IKFootSolver oppositeFoot;
        [SerializeField, Min(0)] float minDistToMove = 1;
        [SerializeField, Min(0)] float stepHeight = 1;
        [SerializeField, Min(0)] float moveTime;
        [SerializeField, Min(0)] float heightOffset;
        Vector3 oldPosition, currentPosition, newPosition;
        Vector3 oldNormal, currentNormal, newNormal;
        Quaternion originalRot;
        float timer;

        public bool isMoving { get; private set; }
        
        float SqrMinDistToMove => minDistToMove * minDistToMove;
        
        private void Start()
        {
            target.position = currentPosition = transform.position;
            target.up = currentNormal = transform.up;
            
            originalRot = transform.rotation;
        }
        void Update()
        {
            if(oppositeFoot.isMoving) return;
            
            transform.position = currentPosition;
            transform.up = currentNormal;

            if (isMoving)
            {
                timer += Time.deltaTime;
                
                if (timer >= moveTime)
                {
                    timer = moveTime;
                    isMoving = false;
                }
                
                UpdateRig(timer / moveTime);
                return;
            }
            
            if (Physics.Raycast(target.position, Vector3.down, out RaycastHit hit, 10, terrainLayer))
            {
                target.position = hit.point + Vector3.up * heightOffset;
                target.up = originalRot * hit.normal;
            }
            
            if ((target.position - transform.position).sqrMagnitude > SqrMinDistToMove)
            {
                timer = 0;
                isMoving = true;
                
                oldPosition = transform.position;
                oldNormal = transform.up;
                
                newPosition = target.position;
                newNormal = target.up;
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, 0.5f);

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(target.position, 0.5f);
        }

        public void UpdateRig(float t)
        {
            if (t < 1) //Update lerp & target pos
            {
                Vector3 tempPosition = Vector3.Lerp(oldPosition, newPosition, t);
                tempPosition.y += Mathf.Sin(t * Mathf.PI) * stepHeight;

                currentPosition = tempPosition;
                currentNormal = Vector3.Lerp(oldNormal, newNormal, t);
            }
        }
    }
}