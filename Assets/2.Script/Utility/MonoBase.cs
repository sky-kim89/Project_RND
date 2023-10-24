namespace MyProjecktExtnesions
{
	using UnityEngine;
	using System.Collections;
    using System.Collections.Generic;
    using System;


    public abstract class MonoBase : MonoBehaviour
	{
        protected virtual void DelayAction(float delay, Action action)
		{
            if (gameObject.activeSelf)
            {
                StartCoroutine(DelayActionHandler(delay, action));
            }
        }

        private IEnumerator DelayActionHandler(float delay, Action action)
		{
            if(delay != 0)
			    yield return new WaitForSeconds(delay);

			if (action != null)
			{
				action();
            }
		}
	}
}