﻿using System;
using System.Threading.Tasks;

namespace CommonUtility.Task
{
    public class TaskRunner
    {
        /// <summary>
        /// Executes a method function asynchronously, executing a callback when execution completes callback
        /// </summary>
        /// <param name="function">An asynchronous method that has no arguments and the return type must be void</param>
        /// <param name="callback">The callback method that executes when the asynchronous method finishes executing, the method has no arguments, and the return type must be void</param>
        public static async void RunAsync(Action function, Action callback)
        {
            Func<System.Threading.Tasks.Task> taskFunc = () =>
            {
                return TaskEx.Run(() =>
                {
                    function();
                });
            };
            await taskFunc();
            if (callback != null)
            {
                callback.Invoke();
            }
        }

        /// <summary>
        /// Executes a method function asynchronously, executing a callback when execution completes callback
        /// </summary>
        /// <typeparam name="TResult">Return type of asynchronous method</typeparam>
        /// <param name="function">An asynchronous method that has no arguments and the return type must be TResult</param>
        /// <param name="callback">The callback method that executes when the asynchronous method finishes executing, the method parameter is TResult, and the return type must be void</param>
        public static async void RunAsync<TResult>(Func<TResult> function, Action<TResult> callback)
        {
            Func<Task<TResult>> taskFunc = () =>
            {
                return TaskEx.Run(() =>
                {
                    return function();
                });
            };
            if (callback != null)
            {
                callback.Invoke(await taskFunc());
            }
        }
    }
}
