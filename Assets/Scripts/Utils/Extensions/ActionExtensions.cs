using System;

    /// <summary>
    /// Extensions for action events
    /// </summary>
    public static class ActionExtentions
    {
        /// <summary>
        /// Safely invokes an action
        /// </summary>
        /// <param name="action">The action to invoke</param>
        public static void SafeInvoke(this Action action)
        {
            if (action == null) return;

            action();
        }

        /// <summary>
        /// Safely invokes an action with an argument
        /// </summary>
        /// <typeparam name="T">The first argument type in the action</typeparam>
        /// <param name="action">The action to invoke</param>
        /// <param name="args">The argument to send the action</param>
        public static void SafeInvoke<T>(this Action<T> action, T args)
        {
            if (action == null) return;

            action(args);
        }

        /// <summary>
        /// Safely invokes an action with two argument
        /// </summary>
        /// <typeparam name="T">The first argument type in the action</typeparam>
        /// <typeparam name="TK">The second argument type in the action</typeparam>
        /// <param name="action">The action to invoke</param>
        /// <param name="args">The argument to send the action</param>
        /// <param name="args2">The second argument to send the action</param>
        public static void SafeInvoke<T, TK>(this Action<T, TK> action, T args, TK args2)
        {
            if (action == null) return;

            action(args, args2);
        }

        /// <summary>
        /// Safely invokes an action with three argument
        /// </summary>
        /// <typeparam name="T">The first argument type in the action</typeparam>
        /// <typeparam name="TK">The second argument type in the action</typeparam>
        /// <typeparam name="TKK">The third argument type in the action</typeparam>
        /// <param name="action">The action to invoke</param>
        /// <param name="args">The argument to send the action</param>
        /// <param name="args2">The second argument to send the action</param>
        /// <param name="args3">The third argument to send the action</param>
        public static void SafeInvoke<T, TK, TKK>(this Action<T, TK, TKK> action, T args, TK args2, TKK args3)
        {
            if (action == null) return;

            action(args, args2, args3);
        }
    }