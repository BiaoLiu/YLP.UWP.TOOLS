using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using YLP.UWP.Core.Attributes;

namespace YLP.UWP.Core
{
    /// <summary>  
    /// 枚举帮助类  
    /// </summary>  
    public class EnumHelper
    {
        /// <summary>  
        /// 获取枚举项的Attribute  
        /// </summary>  
        /// <typeparam name="T">自定义的Attribute</typeparam>  
        /// <param name="source">枚举</param>  
        /// <returns>返回枚举,否则返回null</returns>  
        public static T GetCustomAttribute<T>(Enum source) where T : Attribute
        {
            Type sourceType = source.GetType();
            string sourceName = Enum.GetName(sourceType, source);
            FieldInfo field = sourceType.GetField(sourceName);
            var attributes = field.GetCustomAttributes(typeof(T), false);

            return attributes.FirstOrDefault(a => a is T) as T;
        }

        /// <summary>  
        ///获取DescriptionAttribute描述  
        /// </summary>  
        /// <param name="source">枚举</param>  
        /// <returns>有description标记，返回标记描述，否则返回null</returns>  
        public static string GetDescription(Enum source)
        {
            var attr = GetCustomAttribute<DisplayNameAttribute>(source);

            return attr?.DisplayName;
        }
    }
}
