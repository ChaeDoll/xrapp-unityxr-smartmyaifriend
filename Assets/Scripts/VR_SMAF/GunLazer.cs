using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLazer : MonoBehaviour
{
    public Transform laserOrigin;  // 레이저가 시작되는 지점 (총의 끝 부분)
    public LineRenderer lineRenderer;
    public GameObject bulletHolePrefab;
    public MinigameGunScript minigameGunScript;
    List<GameObject> bulletHoles;
    private void Start()
    {
        bulletHoles = new List<GameObject>();
    }
    void Update()
    {
        // 레이저 시작 위치
        lineRenderer.SetPosition(0, laserOrigin.position);

        // 레이저가 나아가는 방향
        RaycastHit hit;
        if (Physics.Raycast(laserOrigin.position, laserOrigin.forward, out hit))
        {
            // 레이저가 무언가에 닿았을 때의 위치
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            // 레이저가 닿지 않을 경우, 최대 거리까지 그리기
            lineRenderer.SetPosition(1, laserOrigin.position + laserOrigin.forward * 100f);
        }
    }

    public void CreateBulletHole()
    {
        RaycastHit hit;
        if (Physics.Raycast(laserOrigin.position, laserOrigin.forward, out hit, 100f))
        {
            GameObject bulletHole;
            // 총알 자국 생성
            bulletHole = Instantiate(bulletHolePrefab, hit.point, Quaternion.LookRotation(hit.normal));
            bulletHoles.Add(bulletHole);

            MinigameGunTarget target = hit.collider.GetComponent<MinigameGunTarget>();
            if (target != null)
            {
                minigameGunScript.HitTarget(target.targetScore);
            }
        }
    }
    public void ClearBulletHole()
    {  
        foreach (GameObject bullet in bulletHoles)
        {
            Destroy(bullet);
        }
    }
}
