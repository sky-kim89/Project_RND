
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Damage {

        public Damage(Unit actor, int damagePoint)
        {
            m_Unit = actor;
            m_DamagePoint = damagePoint;
        }

        private Unit m_Unit = null;
        public Unit Unit
        {
            get
            {
                return m_Unit;
            }
        }

        private int m_DamagePoint = 0;
        public int DamagePoint
        {
            get
            {
                return (int)(((float)m_Unit.AP) * ((float)DamageDrainage * 0.001f));
            }
        }

        public int DamageDrainage = 1000;
        public float Knockback = 1f;
    }
