using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLazer : MonoBehaviour
{
    public Transform laserOrigin;  // �������� ���۵Ǵ� ���� (���� �� �κ�)
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
        // ������ ���� ��ġ
        lineRenderer.SetPosition(0, laserOrigin.position);

        // �������� ���ư��� ����
        RaycastHit hit;
        if (Physics.Raycast(laserOrigin.position, laserOrigin.forward, out hit))
        {
            // �������� ���𰡿� ����� ���� ��ġ
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            // �������� ���� ���� ���, �ִ� �Ÿ����� �׸���
            lineRenderer.SetPosition(1, laserOrigin.position + laserOrigin.forward * 100f);
        }
    }

    public void CreateBulletHole()
    {
        RaycastHit hit;
        if (Physics.Raycast(laserOrigin.position, laserOrigin.forward, out hit, 100f))
        {
            GameObject bulletHole;
            // �Ѿ� �ڱ� ����
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
