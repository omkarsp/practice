using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovementAI : MonoBehaviour
{
    [SerializeField] Transform startPos;
    [SerializeField] Transform endPos;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] NavMeshObstacle[] obstacles;
    [SerializeField] NavMeshSurface surface;

    [SerializeField] Vector3 clickPos;
    bool isMove = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Move()
    {
        agent.SetDestination(Input.mousePosition);
        isMove = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) { 
            isMove = true;
            Move();
        }
    }
}
