using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YLP.UWP.Core.Data
{
    /// <summary>
    /// 单例提供者 继承此类可实现单例模式
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingletonProvider<T> where T : new()
    {
        public static T Instance => SingletonCreator.instance;

        class SingletonCreator
        {
            static SingletonCreator()
            { }

            internal static readonly T instance = new T();
        }
    }
}
