namespace MyProjecktExtnesions
{
	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
    using UnityEngine.UI;
    using DG.Tweening;

	public static class UnityExtensions
	{
		public static void EnableCollider(this Component component)
		{
			if (component != null)
			{
				Collider[] colliders = component.GetComponents<Collider>();
				foreach (var collider in colliders)
				{
					collider.enabled = true;
				}
			}
		}

		public static void DisableCollider(this Component component)
		{
			if (component != null)
			{
				Collider[] colliders = component.GetComponents<Collider>();
				foreach (var collider in colliders)
				{
					collider.enabled = false;
				}
			}
		}

		public static void ApplyCollider(this Component component, bool enable)
		{
			if (component != null)
			{
				Collider[] colliders = component.GetComponents<Collider>();
				foreach (var collider in colliders)
				{
					collider.enabled = enable;
				}
			}
		}

		public static void EnableCollider(this GameObject gameObject)
		{
			if (gameObject != null)
			{
				Collider[] colliders = gameObject.GetComponents<Collider>();
				foreach (var collider in colliders)
				{
					collider.enabled = true;
				}
			}
		}

		public static void DisableCollider(this GameObject gameObject)
		{
			if (gameObject != null)
			{
				Collider[] colliders = gameObject.GetComponents<Collider>();
				foreach (var collider in colliders)
				{
					collider.enabled = false;
				}
			}
		}

		public static void ApplyCollider(this GameObject gameObject, bool enable)
		{
			if (gameObject != null)
			{
				Collider[] colliders = gameObject.GetComponents<Collider>();
				foreach (var collider in colliders)
				{
					collider.enabled = enable;
				}
			}
		}

		public static bool IsActiveInScene(this Component component)
		{
			return (component != null) ? component.gameObject.activeInHierarchy : false;
		}

		public static void Activate(this Component component)
		{
			if (component != null)
			{
				component.gameObject.SetActive(true);
			}
		}

		public static void Deactivate(this Component component)
		{
			if (component != null)
			{
				component.gameObject.SetActive(false);
			}
		}

		public static void Enable(this MonoBehaviour monobehaviour)
		{
			if (monobehaviour.IsNotNull())
			{
				monobehaviour.enabled = true;
			}
		}

		public static void Disable(this MonoBehaviour monobehaviour)
		{
			if (monobehaviour.IsNotNull())
			{
				monobehaviour.enabled = false;
			}
		}

		public static void ActivateChildren(this Component component, bool recursively = true)
		{
			if (component != null)
			{
				for (int i = 0; i < component.transform.childCount; ++i)
				{
					Transform child = component.transform.GetChild(i);
					if (recursively == true)
					{
						child.ActivateChildren();
					}
					else
					{
						child.Activate();
					}
				}
			}
		}

		public static void DeactivateChildren(this Component component, bool recursively = true)
		{
			if (component != null)
			{
				for (int i = 0; i < component.transform.childCount; ++i)
				{
					Transform child = component.transform.GetChild(i);
					if (recursively == true)
					{
						child.DeactivateChildren();
					}
					else
					{
						child.Deactivate();
					}
				}
			}
		}

		public static void ApplyActive(this Component component, bool active)
		{
			if (component != null)
			{
				component.gameObject.SetActive(active);
			}
		}

		public static bool IsActiveInScene(this GameObject gameObject)
		{
			return (gameObject != null) ? gameObject.activeInHierarchy : false;
		}

		public static void Activate(this GameObject gameObject)
		{
			if (gameObject != null)
			{
				gameObject.SetActive(true);
			}
		}

		public static void Deactivate(this GameObject gameObject)
		{
			if (gameObject != null)
			{
				gameObject.SetActive(false);
			}
		}

		public static void ApplyActive(this GameObject gameObject, bool active)
		{
			if (gameObject != null)
			{
				gameObject.SetActive(active);
			}
		}

		public static void ChangeLayer(this Transform trans, string layerName)
		{
			if (trans.IsNotNull())
			{
				trans.gameObject.layer = LayerMask.NameToLayer(layerName);
			}
		}

		public static void ChangeLayer(this Component component, string layerName)
		{
			if (component.IsNotNull())
			{
				component.gameObject.layer = LayerMask.NameToLayer(layerName);
			}
		}

		public static void ChangeLayer(this GameObject gameObject, string layerName)
		{
			if (gameObject.IsNotNull())
			{
				gameObject.layer = LayerMask.NameToLayer(layerName);
			}
		}

		public static void ChangeLayersRecursively(this Transform trans, string layerName)
		{
			if (trans.IsNotNull())
			{
				trans.gameObject.layer = LayerMask.NameToLayer(layerName);
				foreach (Transform child in trans)
				{
					child.ChangeLayersRecursively(layerName);
				}
			}
		}

		public static void ChangeLayerRecursively(this Component component, string layerName)
		{
			if (component.IsNotNull())
			{
				component.gameObject.layer = LayerMask.NameToLayer(layerName);
				foreach (Transform child in component.gameObject.transform)
				{
					child.ChangeLayersRecursively(layerName);
				}
			}
		}

		public static void ChangeLayerRecursively(this GameObject gameObject, string layerName)
		{
			if (gameObject.IsNotNull())
			{
				gameObject.layer = LayerMask.NameToLayer(layerName);
				foreach (Transform child in gameObject.transform)
				{
					child.ChangeLayersRecursively(layerName);
				}
			}
		}

		public static void LayerCullingShow(this Camera cam, int layerMask)
		{
			cam.cullingMask |= layerMask;
		}

		public static void LayerCullingShow(this Camera cam, string layer)
		{
			LayerCullingShow(cam, 1 << LayerMask.NameToLayer(layer));
		}

		public static void LayerCullingHide(this Camera cam, int layerMask)
		{
			cam.cullingMask &= ~layerMask;
		}

		public static void LayerCullingHide(this Camera cam, string layer)
		{
			LayerCullingHide(cam, 1 << LayerMask.NameToLayer(layer));
		}

		public static void LayerCullingToggle(this Camera cam, int layerMask)
		{
			cam.cullingMask ^= layerMask;
		}

		public static void LayerCullingToggle(this Camera cam, string layer)
		{
			LayerCullingToggle(cam, 1 << LayerMask.NameToLayer(layer));
		}

		public static bool LayerCullingIncludes(this Camera cam, int layerMask)
		{
			return (cam.cullingMask & layerMask) > 0;
		}

		public static bool LayerCullingIncludes(this Camera cam, string layer)
		{
			return LayerCullingIncludes(cam, 1 << LayerMask.NameToLayer(layer));
		}

		public static void LayerCullingToggle(this Camera cam, int layerMask, bool isOn)
		{
			bool included = LayerCullingIncludes(cam, layerMask);
			if (isOn && !included)
			{
				LayerCullingShow(cam, layerMask);
			}
			else if (!isOn && included)
			{
				LayerCullingHide(cam, layerMask);
			}
		}

		public static void LayerCullingToggle(this Camera cam, string layer, bool isOn)
		{
			LayerCullingToggle(cam, 1 << LayerMask.NameToLayer(layer), isOn);
		}
        
		public static void SetParent(this GameObject gameObject, GameObject parent)
		{
			if (gameObject != null && parent != null)
			{
				gameObject.transform.SetParent(parent.transform);
			}
		}

		public static void SetParent(this Transform transform, GameObject parent)
		{
			if (transform != null && parent != null)
			{
				transform.SetParent(parent.transform);
			}
		}

		public static void ResetTransform(this GameObject gameObject)
		{
			if (gameObject != null)
			{
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localRotation = Quaternion.identity;
				gameObject.transform.localScale = Vector3.one;
			}
        }

        public static void RendemPosition(this Transform transform)
        {
            if (transform != null)
            {
                transform.localPosition = new Vector3(Random.Range(-6f, 6f), 0, 0);
                transform.localRotation = Quaternion.identity;
                //transform.localScale = Vector3.one;
            }
        }

        public static void CopyTransform(this GameObject gameObject, GameObject targetObject)
		{
			if (gameObject != null)
			{
				gameObject.transform.localPosition = targetObject.transform.localPosition;
				gameObject.transform.localRotation = targetObject.transform.localRotation;
				gameObject.transform.localScale = targetObject.transform.localScale;
			}
		}

		public static int MakeLayerMask(string[] layerMasks)
		{
			UnityEngine.Assertions.Assert.IsNotNull(layerMasks);

			int value = 0;
			foreach (var layerMask in layerMasks)
			{
				int maskValue = LayerMask.NameToLayer(layerMask);
				value |= (1 << maskValue);
			}

			return value;
		}

		public static IEnumerator WaitForRealSeconds(float time)
		{
			float start = Time.realtimeSinceStartup;
			while (Time.realtimeSinceStartup < start + time)
			{
				yield return null;
			}
		}

		public static bool RemoveComponent<T>(this Component component)
		{
			var components = component.GetComponents(typeof(Component));
			foreach (var comp in components)
			{
				if (comp is T)
				{
					GameObject.DestroyImmediate(comp);
					return true;
				}
			}

			return false;
		}

		public static void RemoveComponentInChildren<T>(this Component component)
		{
			var components = component.GetComponentsInChildren(typeof(Component));
			foreach (var comp in components)
			{
				if (comp is T)
				{
					GameObject.DestroyImmediate(comp);
				}
			}
		}

		public static IEnumerator SetActiveSceneDelayed(this UnityEngine.SceneManagement.Scene scene, System.Action onActivated)
		{
			while (!UnityEngine.SceneManagement.SceneManager.SetActiveScene(scene))
			{
				yield return null;
			}

			if (onActivated.IsNotNull())
			{
				onActivated();
			}
		}
	}
}