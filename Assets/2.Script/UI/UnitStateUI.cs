using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UnitStateUI : MonoBehaviour
{
    public GameObject m_HPObj = null;
    public GameObject m_CoolObj = null;

    public TextMeshPro m_TextMesh = null;

    public Renderer Unit = null;
    public Material EnemyMaterial = null;
    public Material AllyMaterial = null;

    public void Respawn()
    {
        m_HPObj.transform.localScale = new Vector3(0.1f, 0.1f, 1f);
        m_CoolObj.transform.localScale = new Vector3(0.1f, 0.1f, 0);
        m_TextMesh.gameObject.SetActive(false);
    }

    public void SetHP(float hp)
    {
        if (hp <= 0)
            hp = 0;
        m_HPObj.transform.DOScaleZ(hp, 0.2f);
    }

    public void SetEnemy(bool isEnemy)
    {
        Unit.material = isEnemy ? EnemyMaterial : AllyMaterial;
    }

    public void Hit(int damage)
    {
        m_TextMesh.gameObject.SetActive(true);
        m_TextMesh.text = damage.ToString();
    }

    public void HitEnd()
    {
        m_TextMesh.gameObject.SetActive(false);
    }

    public void Restore()
    {
        ObjectPool.Instance.Restore(this.gameObject);
    }
}
