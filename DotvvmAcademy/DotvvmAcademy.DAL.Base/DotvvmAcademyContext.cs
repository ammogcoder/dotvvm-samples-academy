﻿using DotvvmAcademy.DAL.Base.Entities;
using DotvvmAcademy.DAL.Base.Providers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace DotvvmAcademy.DAL.Base
{
    public class DotvvmAcademyContext
    {
        private readonly IServiceProvider serviceProvider;

        public DotvvmAcademyContext(IServiceProvider serviceProvider)
        {
            Set<ILesson>();
            this.serviceProvider = serviceProvider;
        }

        public IQueryable<TEntity> Set<TEntity>()
            where TEntity : class, IEntity
        {
            var entityProvider = serviceProvider.GetService<IEntityProvider<TEntity>>();
            return entityProvider.GetQueryable();
        }
    }
}