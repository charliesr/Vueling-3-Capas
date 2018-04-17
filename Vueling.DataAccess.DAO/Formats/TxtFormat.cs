﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Vueling.Common.Logic;
using Vueling.Common.Logic.Model;
using Vueling.Common.Logic.Utils;

namespace Vueling.DataAccess.DAO
{
    public class TxtFormat<T> : IFormat<T> where T : IVuelingModelObject
    {
        private readonly IVuelingLogger _log = ConfigurationUtils.LoadLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public T Add(T entity)
        {
            var content = string.Empty;
            Type type;
            try
            {
                _log.Debug("Añadiendo un/a nuevo/a " + typeof(T).Name);
                var assembly = Assembly.Load("Vueling.Common.Logic");
                type = assembly.GetType(typeof(T).FullName);
                var methodToString = type.GetMethod("ToString");
                object[] propValues = new object[type.GetProperties().Length];
                for (int i = 0; i < type.GetProperties().Length; i++)
                {
                    propValues[i] = type.GetProperties()[i].GetValue(entity);
                }
                var classInstance = Activator.CreateInstance(type, propValues);
                content = (string)methodToString.Invoke(classInstance, null);
                File.AppendAllText(ConfigurationUtils.GetFilePath<T>(DataTypeAccess.txt), content + "\r\n");
                return GetByGUID((Guid)typeof(T).GetProperty("Guid").GetValue(entity));
            }
            catch (ArgumentException ex)
            {
                _log.Error(ex);
                throw;
            }
            catch (NotSupportedException ex)
            {
                _log.Error(ex);
                throw;
            }
            catch (TargetInvocationException ex)
            {
                _log.Error(ex);
                throw;
            }
            catch (FileNotFoundException ex)
            {
                _log.Error(ex);
                throw;
            }

        }

        public T GetByGUID(Guid guid)
        {
            try
            {
                _log.Debug("Select " + typeof(T).Name + "con Guid " + guid.ToString());
                var entityString = string.Empty;
                using (TextReader reader = new StreamReader(ConfigurationUtils.GetFilePath<T>(DataTypeAccess.txt)))
                {
                    StringBuilder word = new StringBuilder();
                    while (reader.Peek() > -1)
                    {
                        word.Append((char)reader.Read());
                        if ((char)reader.Peek() != ',') continue;
                        if (guid.ToString() == word.ToString())
                        {
                            entityString = guid.ToString() + reader.ReadLine();
                        }
                        else
                        {
                            reader.ReadLine();
                        }
                    }
                }
                if (entityString == string.Empty) return default(T);
                var propValues = entityString.Split(',');
                object entity;
                entity = Activator.CreateInstance(typeof(T), propValues);
                return (T)entity;
            }
            catch (ArgumentException ex)
            {
                _log.Error(ex);
                throw;
            }
            catch (NotSupportedException ex)
            {
                _log.Error(ex);
                throw;
            }
            catch (TargetInvocationException ex)
            {
                _log.Error(ex);
                throw;
            }

        }

        public List<T> GetAll()
        {
            try
            {
                _log.Debug("Obtenemos todos los/las " + typeof(T).Name);
                var groupOfEntity = new List<T>();
                using (TextReader reader = new StreamReader(ConfigurationUtils.GetFilePath<T>(DataTypeAccess.txt)))
                {
                    while (reader.Peek() > -1)
                    {
                        var line = reader.ReadLine();
                        var propValues = line.Split(',');
                        var entity = Activator.CreateInstance(typeof(T), propValues);
                        groupOfEntity.Add((T)entity);

                    }
                    return groupOfEntity;
                }
            }
            catch (FileNotFoundException ex)
            {
                _log.Error(ex);
                throw;
            }
            catch (ArgumentNullException ex)
            {
                _log.Error(ex);
                throw;
            }
            catch (PathTooLongException ex)
            {
                _log.Error(ex);
                throw;
            }
            catch (DirectoryNotFoundException ex)
            {
                _log.Error(ex);
                throw;
            }
            catch (UnauthorizedAccessException ex)
            {
                _log.Error(ex);
                throw;
            }
            catch (NotSupportedException ex)
            {
                _log.Error(ex);
                throw;
            }
            catch (SecurityException ex)
            {
                _log.Error(ex);
                throw;
            }
        }

        public DataTypeAccess GetFormat()
        {
            return DataTypeAccess.txt;
        }

        public bool Update(Guid guid, T entity)
        {
            throw new NotImplementedException();
        }

        public bool DeleteByGuid(Guid guid)
        {
            throw new NotImplementedException();
        }
    }
}
