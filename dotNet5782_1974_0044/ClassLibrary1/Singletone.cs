using System;
using System.Reflection;

namespace Utilities
{
    public abstract class Singletone<T> where T : Singletone<T>
    {
        //singelton pattern step #2 - make the static and protected constructor
        static Singletone() { }
        protected Singletone() { }
        class Nested
        {
            //singelton step #3 - _instance is initialized no null
            internal static volatile T _instance = null;
            internal static readonly object _lock = new object();
            static Nested() { }
        }
        /// <summary>
        /// singelton step #4 - separate property ensures Lazy class instatiation, no setter, getter only
        /// </summary>
        public static T Instance
        {
            get
            {
                if (Nested._instance == null)
                {
                    lock(Nested._lock)
                    {
                        if(Nested._instance == null)
                        {
                            Type t = typeof(T);
                            if (t == null || !t.IsSealed)
                                throw new SingletoneExpection(string.Format("'{0}' must be a sealed class", t.Name));
                            ConstructorInfo constr = null;
                            try
                            {
                                constr = t.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
                            }
                            catch (Exception ex)
                            {
                                throw new SingletoneExpection(string.Format("A private/protected constructor is missing for '{0}'.", t.Name,ex));
                            }
                            if (constr == null || constr.IsAssembly)
                                throw new SingletoneExpection(string.Format("A private/protected constructor is missing for '{0}'.", t.Name));
                            Nested._instance = (T)constr.Invoke(null);
                        }
                    }
                }
                return Nested._instance;
            }
        }
    }
}
