namespace MyProjecktExtnesions
{
	using UnityEngine;
	using System.Collections;
	using System.Diagnostics;
	using System.Net;

    public enum eStringType
    {
        Default = 0,
        Attract,
        Server
    }

	public static class CommonExtensions
	{
		public static void Reset(this System.Action action)
		{
			action = () => { };
		}

		public static T ToEnum<T>(this string value, bool ignoreCase = true)
		{
			return (T)System.Enum.Parse(typeof(T), value, ignoreCase);
		}

		public static T ToEnum<T>(this int value)
		{
			return (T)System.Enum.ToObject(typeof(T), value);
		}

		public static bool IsMatch(this HttpStatusCode code, int value)
		{
			return (value.ToEnum<HttpStatusCode>() == code);
		}

        public static bool ToBool(this int value)
        {
            return value != 0; 
        }

        public static int ToInt(this bool value)
        {
            return value ? 1 : 0;
        }

		public static void Restart(this Stopwatch stopWarch)
		{
			if (stopWarch != null)
			{
				stopWarch.Stop();
				stopWarch.Reset();
				stopWarch.Start();
			}
		}

		public static bool IsNullOrEmpty(this string value)
		{
			return (string.IsNullOrEmpty(value));
		}

		public static bool IsValidText(this string value)
		{
			return (string.IsNullOrEmpty(value) == false);
		}

		public static bool IsNull(this object value)
		{
			return (value == null);
		}

		public static bool IsNotNull(this object value)
		{
			return (value != null);
		}

		public static bool IsNull(this UnityEngine.Object value)
		{
			return (value == false);
		}

		public static bool IsNotNull(this UnityEngine.Object value)
		{
			return (value == true);
		}

		public static bool IsTrue(this bool value)
		{
			return (value == true);
		}

		public static bool IsFalse(this bool value)
		{
			return (value == false);
		}

		public static bool IsTrue(this int value)
		{
			return (value.IsAssigned());
		}

		public static bool IsFalse(this int value)
		{
			return (value.IsUnassigned());
		}

		public static bool IsAssigned(this int value)
		{
			return (value > 0);
		}

		public static bool IsUnassigned(this int value)
		{
			return (value == 0);
		}

		public static T ToConvert<T>(this System.Enum value)
		{
			object newValue = value as object;

			if (newValue.IsNotNull())
			{
				return (T)newValue;
			}
			else
			{
				throw new System.Exception(string.Format("Can't convert this [{0}] type", typeof(T).Name));			
			}
		}

        public static float Half(this float value)
        {
            if (value != 0f)
            {
                return value * 0.5f;
            }
            else
            {
                return 0f;
            }
        }

        public static float Half(this int value)
        {
            if (value != 0)
            {
                return value * 0.5f;
            }
            else
            {
                return 0f;
            }
        }
	}
}