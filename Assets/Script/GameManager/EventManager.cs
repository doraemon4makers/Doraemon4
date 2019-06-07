using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

	public class EventManager : MonoBehaviour {

		//声明存放全局事件的字典
		private static Dictionary<string,Delegate> s_GlobalEventTable = new Dictionary<string, Delegate>();

		//声明存放在单个游戏对象上被调用的局部事件的字典
		private static Dictionary<GameObject,Dictionary<string,Delegate>> s_EventTable = new Dictionary<GameObject,Dictionary<string,Delegate>>();

		//全局事件的登录函数
		public static void RegisterEvent(string eventName, Action handler)
		{
			RegisterEvent(eventName, (Delegate)handler);
		}

		public static void RegisterEvent<T>(string eventName, Action<T> handler)
		{
			RegisterEvent(eventName, (Delegate)handler);
		}

        public static void RegisterEvent<T1,T2>(string eventName, Action<T1,T2> handler)
        {
            RegisterEvent(eventName, (Delegate)handler);
        }

        public static void RegisterEvent<T1, T2, T3>(string eventName, Action<T1, T2, T3> handler)
        {
            RegisterEvent(eventName, (Delegate)handler);
        }

        public static void RegisterEvent<T1, T2, T3, T4>(string eventName, Action<T1, T2, T3, T4> handler)
        {
            RegisterEvent(eventName, (Delegate)handler);
        }

        private static void RegisterEvent(string eventName, Delegate handler)
		{
			Delegate prevHandler;
			if(s_GlobalEventTable.TryGetValue(eventName,out prevHandler))	//如果要存入字典的键名称跟已经在字典里面的键名重复了
			{
				s_GlobalEventTable[eventName] = Delegate.Combine(prevHandler,handler);
				//将已有的键名对应的委托和要新存入的委托合并，存回已有建名对应的位置
			}
			else 	//如果字典中不存在以eventName为键名称的键值对，就以eventName为键名称创建新的键值对
			{
				s_GlobalEventTable.Add(eventName,handler);
			}
		}


		//局部事件的登录函数
		public static void RegisterEvent(GameObject obj, string eventName, Action handler)
		{
			RegisterEvent(obj, eventName, (Delegate)handler);
		}

		public static void RegisterEvent<T>(GameObject obj, string eventName, Action<T> handler)
		{
			RegisterEvent(obj, eventName, (Delegate)handler);
		}

        public static void RegisterEvent<T1, T2>(GameObject obj, string eventName, Action<T1, T2> handler)
        {
            RegisterEvent(obj, eventName, (Delegate)handler);
        }

        public static void RegisterEvent<T1, T2, T3>(GameObject obj, string eventName, Action<T1, T2, T3> handler)
        {
            RegisterEvent(obj, eventName, (Delegate)handler);
        }

        public static void RegisterEvent<T1, T2, T3, T4>(GameObject obj, string eventName, Action<T1, T2, T3, T4> handler)
        {
            RegisterEvent(obj, eventName, (Delegate)handler);
        }

        private static void RegisterEvent(GameObject obj, string eventName, Delegate handler)
		{
			//在局部事件字典中查找以obj游戏对象为键的字典
			Dictionary<string, Delegate> handlers;

			//如果在局部事件字典中找不到以obj游戏对象为键的字典，说明这是obj游戏对象的第一次事件登录
			if (!s_EventTable.TryGetValue(obj, out handlers)) 
			{
				//在局部字典中添加以obj为键，一个空白字典为值的键值对
				handlers = new Dictionary<string, Delegate>();
				s_EventTable.Add(obj, handlers);
			}

			//在记录obj游戏对象的局部事件的字典中查找以eventName为键的委托
			Delegate prevHandlers;
			//如果在局部事件字典中已经存在以eventName为键的委托
			if (handlers.TryGetValue(eventName, out prevHandlers))
			{
				//就将已有的键名对应的委托和要新存入的委托合并，存回已有建名对应的位置
				handlers[eventName] = Delegate.Combine(prevHandlers, handler);
			} else
			{
				//否则，说明这是eventName的第一次事件登录，那么就以eventName为键名称创建新的键值对
				handlers.Add(eventName, handler);
			}
		}

		//全局事件的调用函数
		public static void ExecuteEvent(string eventName)
		{
			Action handler;
			handler = s_GlobalEventTable[eventName] as Action;
			handler();
		}

		public static void ExecuteEvent<T>(string eventName, T arg1)
		{
			Action<T> handler;
			handler = s_GlobalEventTable[eventName] as Action<T>;
			if (handler != null) {
				handler(arg1);
			}
		}

        public static void ExecuteEvent<T1, T2>(string eventName, T1 arg1, T2 arg2)
        {
            Action<T1,T2> handler;
            handler = s_GlobalEventTable[eventName] as Action<T1, T2>;
            if (handler != null)
            {
                handler(arg1, arg2);
            }
        }

        public static void ExecuteEvent<T1, T2, T3>(string eventName, T1 arg1, T2 arg2, T3 arg3)
        {
            Action<T1, T2, T3> handler;
            handler = s_GlobalEventTable[eventName] as Action<T1, T2, T3>;
            if (handler != null)
            {
                handler(arg1, arg2, arg3);
            }
        }

        public static void ExecuteEvent<T1, T2, T3, T4>(string eventName, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            Action<T1, T2, T3, T4> handler;
            handler = s_GlobalEventTable[eventName] as Action<T1, T2, T3, T4>;
            if (handler != null)
            {
                handler(arg1, arg2, arg3, arg4);
            }
        }


        //局部事件的调用函数
        public static void ExecuteEvent(GameObject obj, string eventName)
		{
			Dictionary<string,Delegate> handlers;
			handlers = s_EventTable[obj];

			if(handlers != null)
			{
				Action handler;
				handler = handlers[eventName] as Action;
				if(handler != null)
				{
					handler();
				}
			}
		}

		public static void ExecuteEvent<T>(GameObject obj, string eventName, T arg1)
		{
			Dictionary<string,Delegate> handlers;
			handlers = s_EventTable[obj];

			if(handlers != null)
			{
				Action<T> handler;
				handler = handlers[eventName] as Action<T>;
				if(handler != null)
				{
					handler(arg1);
				}
			}
		}

        public static void ExecuteEvent<T1, T2>(GameObject obj, string eventName, T1 arg1, T2 arg2)
        {
            Dictionary<string, Delegate> handlers;
            handlers = s_EventTable[obj];

            if (handlers != null)
            {
                Action<T1, T2> handler;
                handler = handlers[eventName] as Action<T1, T2>;
                if (handler != null)
                {
                    handler(arg1, arg2);
                }
            }
        }

        public static void ExecuteEvent<T1, T2, T3>(GameObject obj, string eventName, T1 arg1, T2 arg2, T3 arg3)
        {
            Dictionary<string, Delegate> handlers;
            handlers = s_EventTable[obj];

            if (handlers != null)
            {
                Action<T1, T2, T3> handler;
                handler = handlers[eventName] as Action<T1, T2, T3>;
                if (handler != null)
                {
                    handler(arg1, arg2, arg3);
                }
            }
        }

        public static void ExecuteEvent<T1, T2, T3, T4>(GameObject obj, string eventName, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            Dictionary<string, Delegate> handlers;
            handlers = s_EventTable[obj];

            if (handlers != null)
            {
                Action<T1, T2, T3, T4> handler;
                handler = handlers[eventName] as Action<T1, T2, T3, T4>;
                if (handler != null)
                {
                    handler(arg1, arg2, arg3, arg4);
                }
            }
        }

        //取消全局事件订阅的函数
        private static void UnregisterEvent(string eventName, Delegate handler)
		{
			Delegate prevHandlers;
			if (s_GlobalEventTable.TryGetValue(eventName, out prevHandlers)) {
				s_GlobalEventTable[eventName] = Delegate.Remove(prevHandlers, handler);
			}
		}

		public static void UnregisterEvent(string eventName, Action handler)
		{
			UnregisterEvent(eventName, (Delegate)handler);
		}

		public static void UnregisterEvent<T>(string eventName, Action<T> handler)
		{
			UnregisterEvent(eventName, (Delegate)handler);
		}

        public static void UnregisterEvent<T1, T2>(string eventName, Action<T1, T2> handler)
        {
            UnregisterEvent(eventName, (Delegate)handler);
        }

        public static void UnregisterEvent<T1, T2, T3>(string eventName, Action<T1, T2, T3> handler)
        {
            UnregisterEvent(eventName, (Delegate)handler);
        }

        public static void UnregisterEvent<T1, T2, T3, T4>(string eventName, Action<T1, T2, T3, T4> handler)
        {
            UnregisterEvent(eventName, (Delegate)handler);
        }

        //取消局部事件订阅的函数
        private static void UnregisterEvent(GameObject obj, string eventName, Delegate handler)
		{
			Dictionary<string, Delegate> handlers;
			if (s_EventTable.TryGetValue(obj, out handlers)) {
				Delegate prevHandlers;
				if (handlers.TryGetValue(eventName, out prevHandlers)) {
					handlers[eventName] = Delegate.Remove(prevHandlers, handler);
				}
			}
		}

		public static void UnregisterEvent(GameObject obj, string eventName, Action handler)
		{
			UnregisterEvent(obj, eventName, (Delegate)handler);
		}

		public static void UnregisterEvent<T>(GameObject obj, string eventName, Action<T> handler)
		{
			UnregisterEvent(obj, eventName, (Delegate)handler);
		}

        public static void UnregisterEvent<T1, T2>(GameObject obj, string eventName, Action<T1, T2> handler)
        {
            UnregisterEvent(obj, eventName, (Delegate)handler);
        }

        public static void UnregisterEvent<T1, T2, T3>(GameObject obj, string eventName, Action<T1, T2, T3> handler)
        {
            UnregisterEvent(obj, eventName, (Delegate)handler);
        }

        public static void UnregisterEvent<T1, T2, T3, T4>(GameObject obj, string eventName, Action<T1, T2, T3, T4> handler)
        {
            UnregisterEvent(obj, eventName, (Delegate)handler);
        }

    }