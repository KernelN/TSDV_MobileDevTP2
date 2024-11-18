using UnityEngine;

namespace TheWasteland.Gameplay
{
    public class IKFootSolver : MonoBehaviour
    {
        [SerializeField] LayerMask terrainLayer = default;
        [SerializeField] Transform target = default;
        [SerializeField] IKFootSolver oppositeFoot;
        [Header("Move Length")]
        [SerializeField, Min(0)] float minDistToMove = 1;
        [Tooltip("distBySpeedMod * (extraSpeed / distBySpeedMod)")]
        [SerializeField, Min(0)] float distBySpeedMod = 1;
        [Header("Height")]
        [SerializeField, Min(0)] float stepHeight = 1;
        [Tooltip("stepHeight * (extraSpeed / sHeightBySpeedMod)")]
        [SerializeField, Min(0)] float sHeightBySpeedMod = 2;
        [SerializeField, Min(0)] float heightOffset;
        [Header("Move Duration")]
        [SerializeField, Min(0)] float moveTime;
        [Tooltip("deltaTime * (extraSpeed * timeBySpeedMod)")]
        [SerializeField, Min(0)] float timeBySpeedMod = 1;
        Vector3 oldPosition, currentPosition, newPosition;
        Vector3 oldNormal, currentNormal, newNormal;
        Quaternion originalRot;
        float timer;
        float extraSpeed = 1;

        public bool isMoving { get; private set; }
        
        float SqrMinDistToMove => (FinalDistToMove) * (FinalDistToMove);
        float FinalStepHeight => stepHeight * (extraSpeed * sHeightBySpeedMod);
        float FinalMoveTime => moveTime * (extraSpeed * timeBySpeedMod);
        float FinalDistToMove => minDistToMove/extraSpeed;
        
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
                timer += Time.deltaTime * extraSpeed;
                
                if (timer >= moveTime)
                {
                    timer = moveTime;
                    isMoving = false;
                }
                else UpdateTargetPos(false); //if it's still moving, update target pos
                
                UpdateRig(timer / moveTime);
            }
            else UpdateTargetPos();
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
                tempPosition.y += Mathf.Sin(t * Mathf.PI) * FinalStepHeight;

                currentPosition = tempPosition;
                currentNormal = Vector3.Lerp(oldNormal, newNormal, t);
            }
        }
        public void SetExtraSpeed(float speed) => extraSpeed = speed + 1;
        void UpdateTargetPos(bool triggerMove = true)
        {
            if (Physics.Raycast(target.position, Vector3.down, out RaycastHit hit, 10, terrainLayer))
            {
                target.position = hit.point + Vector3.up * heightOffset;
                target.up = originalRot * hit.normal;
            }
            
            if ((target.position - transform.position).sqrMagnitude > SqrMinDistToMove)
            {
                newPosition = target.position;
                newNormal = target.up;
                
                if (!triggerMove) return;
                
                timer = 0;
                isMoving = true;
                
                oldPosition = transform.position;
                oldNormal = transform.up;
            }
        }
    }
}