using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Services
{
    public class SystemsManager
    {
        private static readonly Dictionary<Type, ISystem> _globalSystems = new();
        private static readonly Dictionary<Type, ISystem> _levelSystems = new();

        public static ISystem AddInstance(ISystem system, bool global = false)
        {
            var type = system.GetType();
            if (global)
            {
                if (!_globalSystems.TryAdd(type, system))
                {
                    Debug.LogError($"System of type {type} is already added to Global Systems");
                }
            }
            else
            {
                if (!_levelSystems.TryAdd(type, system))
                {
                    Debug.LogError($"System of type {type} is already added to Level Systems");
                }
            }

            return system;
        }
        
        public static ISystem Add<T>(T system, bool global = false) where T : ISystem
        {
            var type = system.GetType();

            if (global)
            {
                if (!_globalSystems.TryAdd(type, system))
                {
                    Debug.LogError($"System of type {type} is already added to Global Systems");
                    return null;
                }
            }
            else
            {
                if (!_levelSystems.TryAdd(type, system))
                {
                    Debug.LogError($"System of type {type} is already added to Level Systems");
                    return null;
                }
            }

            return system;
        }

        public static void ClearLevelSystems()
        {
            _levelSystems.Clear();
        }
        
        public static ISystem Add<T>(bool global = false)
        {
            var system = Activator.CreateInstance(typeof(T)) as ISystem;
            return Add(system, global);
        }

        public static void RemoveSystem(Type system, bool global)
        {
            switch (global)
            {
                case true when _globalSystems.ContainsKey(system):
                    _globalSystems.Remove(system);
                    break;
                case false when _levelSystems.ContainsKey(system):
                    _levelSystems.Remove(system);
                    break;
            }
        }
        
        public static T Get<T>()
        {
            var type = typeof(T);
            foreach (var value in _globalSystems.Values.Where(IsValidByType<T>))
            {
                return (T)value;
            }
            
            foreach (var value in _levelSystems.Values.Where(IsValidByType<T>))
            {
                return (T)value;
            }
            
            return default;
        }
        
        public static IEnumerable<T> GetAll<T>()
        {
            List<T> systems = _globalSystems.Values.Where(IsValidByType<T>).Cast<T>().ToList();
            systems.AddRange(_levelSystems.Values.Where(IsValidByType<T>).Cast<T>());

            return systems;
        }
        
        public static IEnumerable<ISystem> GetAllByGlobalFlag(bool isGlobal)
        {
            List<ISystem> data = new List<ISystem>();
            if (isGlobal)
            {
                foreach (var system in _globalSystems.Values)
                {
                    data.Add(system);
                }

                return data;
            }
            
            foreach (var system in _levelSystems.Values)
            {
                data.Add(system);
            }
            return data;
        }

        public static void Init(bool global = false)
        {
            if (global)
            {
                foreach (var system in _globalSystems)
                {
                    system.Value.InitSystem();
                }
            }
            else
            {
                foreach (var system in _levelSystems)
                {
                    system.Value.InitSystem();
                }
            }
        }

        public static void Start(bool global = false)
        {
            if (global)
            {
                foreach (var system in _globalSystems.Values)
                {
                    system.StartSystem();
                }
            }
            else
            {
                foreach (var system in _levelSystems.Values)
                {
                    system.StartSystem();
                }
            }
        }
        
        public static void GlobalTick(float deltaTime)
        {
            foreach (var system in _globalSystems.Values)
            {
                system.Tick(deltaTime);
            }
            
            foreach (var system in _levelSystems.Values)
            {
                system.Tick(deltaTime);
            }
        }

        private static bool IsValidByType<T>(ISystem system)
        {
            var type = typeof(T);
            return system.GetType() == type || 
                   system.GetType().GetInterfaces().ToList().Exists(t => t == type);
        }
    }
}