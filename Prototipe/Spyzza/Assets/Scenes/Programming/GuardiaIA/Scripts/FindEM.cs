using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindEM : MonoBehaviour
{
    public float hearingRadius;
    public float seeRadius;
    public float seeAngle;
    public float Speed;
    [HideInInspector]
    public Vector3 lastPos;
    public AnimationClip lookArround;
    float animationLenght;
    float startY;
    public int next = 0;
    public Vector3 lastKnown;
    public bool holdsObject = false;
    public bool catched = false;
    public LayerMask ObstacleMask;
    public GameObject[] patrolPoints; //Puntos a los que se movera/volvera el guardia mientras el jugador no haya sido escuchado/visto
    public Vector3 startPosition;

    public bool Aggro = false, canSee = false, hasSeen = false, hearStop = false;
    public GameObject player;

    private void Start()
    {
        startY = transform.position.y;
        animationLenght = lookArround.length;
        startPosition = transform.position;
    }
    private void Update()
    {
        if (!catched)
        {
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                hearStop = true;
            }
            else
            {
                hearStop = false;
            }
            if (Vector3.Distance(transform.position, player.transform.position) <= hearingRadius)
            {
                if (!hearStop) { Hear(); }
                See();
            }
            else if (!hasSeen)
            {
                Patrol();
            }
            if (!canSee && hasSeen && Vector3.Distance(transform.position, player.transform.position) >= seeRadius)
            {
                LastPosition(lastKnown);
            }
        }
    }
    #region Codigo
    IEnumerator resumePatrol()
    {
        yield return new WaitForSeconds(1);
        canSee = false;
        hasSeen = false;
        Patrol();
    }
    void Catch()
    {
        catched = true;
        player.GetComponent<Animator>().Play("Electrocuted");
        player.GetComponent<Player>().alive = false;
        //GameOver
        GameManager.Instance.GameOver();
    }

    private void OnDrawGizmos() //INFO: Permite la visualizaciond el campo de vision de los guardias en el editor de UNITY no necesario para el correcto funcionamiento del codigo
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, seeRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, hearingRadius);

        Vector3 fovLine1 = Quaternion.AngleAxis(seeAngle, transform.up) * transform.forward * seeRadius;
        Vector3 fovLine2 = Quaternion.AngleAxis(-seeAngle, transform.up) * transform.forward * seeRadius;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, fovLine1);
        Gizmos.DrawRay(transform.position, fovLine2);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, (player.transform.position - transform.position).normalized * seeRadius);

        Gizmos.color = Color.black;
        Gizmos.DrawRay(transform.position, transform.forward * seeRadius);
    }
    public void See()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= seeRadius)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            direction.y *= 0;
            float angle = Vector3.Angle(transform.forward, direction);
            if (angle <= seeAngle)
            {
                Ray ray = new Ray(transform.position, player.transform.position - transform.position);
                RaycastHit hit;
                if (!Physics.Raycast(ray, out hit, seeRadius, ObstacleMask))
                {
                    MoveToPlayer(player.transform.position);
                    canSee = true;
                    hasSeen = true;
                }
            }
            else
            {
                canSee = false;
            }
        }
        else
        {
            canSee = false;
        }
    }
    private void Hear()
    {
        Vector3 playerDirection = player.transform.position - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, playerDirection, Speed * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    public void MoveToPlayer(Vector3 playerPosition)
    {
        if (!catched)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPosition, Speed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, startY, transform.position.z);
            lastKnown = playerPosition;
            if (Vector3.Distance(transform.position, playerPosition) <= 1f)
            {
                Catch();
            }
        }
    }
    void LastPosition(Vector3 LastPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, LastPosition, Speed * Time.deltaTime);
        float Distance = Vector3.Distance(transform.position, LastPosition);
        if (Vector3.Distance(transform.position, LastPosition) <= 0.5f)
        {
            //Play animación de girar
            //animationLenght -= Time.deltaTime;
            //if(animationLenght <= 0)
            //{
            StartCoroutine(resumePatrol());
            //}
        }
    }

    public void Patrol() //Crear EmptyObjects en la escena con la posición Y = a la del guardia y arrastralos todos a PatrolPoints en el editor en el orden que quieras que siga el Guardia
    {
        hasSeen = false;
        canSee = false;
        if (next == patrolPoints.Length - 1)
        {
            next = 0;
        }
        Transform nextPos = patrolPoints[next].transform;
        if (Vector3.Distance(transform.position, nextPos.position) <= 0.1f)
        {
            next++;
        }
        transform.LookAt(patrolPoints[next].transform);
        transform.position = Vector3.MoveTowards(transform.position, nextPos.position, Speed * Time.deltaTime);
    }
    #endregion
}
