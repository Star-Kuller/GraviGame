using System;
using System.Collections.Generic;
using Systems;
using UnityEngine;

namespace Services.ServiceLocator
{
    public class ServiceLocator
    {
        private ServiceLocator()
        { 
            
        }

        private readonly Dictionary<string, IService> _services = new Dictionary<string, IService>();
        
        private static ServiceLocator _current = new ServiceLocator();

        /// <summary>
        /// Получить текущий сервис
        /// </summary>
        public static ServiceLocator Current
        {
            get
            {
                if (_current == null)
                    _current = new ServiceLocator();
                return _current;
            }
            set => _current = value;
        }
        
        /// <summary>
        /// Получить сервис из текущего сервис локатора
        /// </summary>
        /// <typeparam name="T">Тип сервиса</typeparam>
        /// <returns>Сервис</returns>
        /// <exception cref="InvalidOperationException">Не удалось получить сервис</exception>
        public T Get<T>() where T : IService
        {
            var key = typeof(T).Name;
            if (_services.TryGetValue(key, out var service))
                return (T)service;
            
            Debug.LogError($"{key} not registered with {GetType().Name}");
            throw new InvalidOperationException();
        }
        
        /// <summary>
        /// Попытается получить сервис из текущего сервис локатора
        /// </summary>
        /// <param name="service">Сервис</param>
        /// <typeparam name="T">Тип сервиса</typeparam>
        /// <returns>Удалось получить сервис</returns>
        public bool TryGet<T>(out T service) where T : IService
        {
            var key = typeof(T).Name;
            var result = _services.TryGetValue(key, out var value);
            service = (T)value;
            return result;
        }

        /// <summary>
        /// Проверяет регистрацию сервиса в текущеме сервис локаторе
        /// </summary>
        /// <typeparam name="T">Тип сервиса</typeparam>
        /// <returns>Сервис зарегистрироваван</returns>
        public bool IsRegistered<T>() where T : IService
        {
            var key = typeof(T).Name;
            return _services.TryGetValue(key, out var service);
        }

        /// <summary>
        /// Регистрирует сервис в текущем сервис локаторе
        /// </summary>
        /// <typeparam name="T">Тип сервиса </typeparam>
        /// <param name="service">Экземпляр сервиса</param>
        public void Register<T>(T service) where T : IService
        {
            var key = typeof(T).Name;
            
            if (_services.ContainsKey(key))
            {
                Debug.LogError(
                    $"Attempted to register service of type {key} which is already registered with the {GetType().Name}.");
                return;
            }

            _services.Add(key, service);
        }
        
        /// <summary>
        /// Пытается зарегистрировать сервис в текущем сервис локаторе
        /// </summary>
        /// <typeparam name="T">Тип сервиса</typeparam>
        /// <param name="service">Экземпляр сервиса</param>
        public bool TryRegister<T>(T service) where T : IService
        {
            var key = typeof(T).Name;
            
            if (_services.ContainsKey(key))
            {
                return false;
            }

            _services.Add(key, service);
            return true;
        }

        /// <summary>
        /// Убирает сервис из текущего сервис локатора
        /// </summary>
        /// <typeparam name="T">Тип сервиса</typeparam>
        public void Unregister<T>() where T : IService
        {
            var key = typeof(T).Name;
            if (!_services.ContainsKey(key))
            {
                Debug.LogError(
                    $"Attempted to unregister service of type {key} which is not registered with the {GetType().Name}.");
                return;
            }

            _services.Remove(key);
        }
    }
}