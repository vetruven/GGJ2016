using System.Collections.Generic;
using Random = UnityEngine.Random;

    public static class ListExtensions
    {
        /// <summary>
        /// Gets random item from list (based on UnityEngine.Random.value)
        /// - Returns Null If the list is empty
        /// </summary>
        public static T GetRandom<T>(this List<T> _this)
        {
            if (_this.Count == 0)
                return default(T);
            else
                return _this[Random.Range(0, _this.Count)];
        }

        /// <summary>
        /// Gets random item from list and removes it from the list (based on UnityEngine.Random.value)
        /// - Returns Null If the list is empty
        /// </summary>
        public static T PopRandom<T>(this List<T> _this)
        {
            if (_this.Count == 0)
                return default(T);
            else
            {
                int i = Random.Range(0, _this.Count);
                T obj = _this[i];
                _this.RemoveAt(i);
                return obj;
            }
        }


        /// <summary>
        /// Returns first member in list e.g. list[list.Count - 1]
        /// </summary>
        public static T GetFirst<T>(this List<T> _this)
        {
            if (_this.Count == 0)
                return default(T);
            else
            {
                return _this[0];
            }
        }

        /// <summary>
        /// Returns first member in list and removes it from the list
        /// </summary>
        public static T PopFirst<T>(this List<T> _this)
        {
            if (_this.Count == 0)
                return default(T);
            else
            {
                T obj = _this[0];
                _this.RemoveAt(0);
                return obj;
            }
        }


        /// <summary>
        /// Returns last member in list e.g. list[list.Count - 1]
        /// </summary>
        public static T GetLast<T>(this List<T> _this)
        {
            if (_this.Count == 0)
                return default(T);
            else
            {
                return _this[_this.Count - 1];
            }
        }

        /// <summary>
        /// Returns last member in list e.g. list[list.Count - 1] and removes it from the list
        /// </summary>
        public static T PopLast<T>(this List<T> _this)
        {
            if (_this.Count == 0)
                return default(T);
            else
            {
                T obj = _this[_this.Count - 1];;
                _this.RemoveAt(_this.Count - 1);
                return obj;
            }
        }


    }
