﻿using DotvvmAcademy.DAL.Base.Entities;
using DotvvmAcademy.DAL.Base.Providers;
using DotvvmAcademy.DAL.FileSystem.EntityIndex;
using DotvvmAcademy.DAL.FileSystem.Services;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.DAL.FileSystem.Providers
{
    public class FileSystemLessonProvider : ILessonProvider
    {

        public FileSystemLessonProvider(LessonIndex )
        {
        }

        public IQueryable<ILesson> GetQueryable()
        {
            return index.Items.Select(async i => await GetLesson(i)).Select(t => t.Result).AsQueryable();
        }

        private async Task<ILesson> GetLesson(LessonIndexEntity indexEntity)
        {
            var config = (await crawler.GetLessons(indexEntity.Path))[indexEntity.FileIndex];
        }
    }
}