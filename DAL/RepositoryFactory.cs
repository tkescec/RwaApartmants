﻿using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class RepositoryFactory
    {
        private static readonly string _dataSource = ConfigurationManager.AppSettings["DataSource"];
        private static IRepository _repository;


        public static IRepository GetRepository()
        {
            try
            {
                Type type = Type.GetType(_dataSource);
                _repository = (IRepository)Activator.CreateInstance(type);
            }
            catch (Exception)
            {
                _repository = new DbRepository();
            }

            return _repository;

        }
    }
}
