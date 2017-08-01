﻿using DotvvmAcademy.Lessons;
using DotvvmAcademy.Services;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;

namespace DotvvmAcademy.Cache
{
    public class LessonsCache : KeyValueItemCacheBase<Dictionary<int, LessonBase>>
    {
        public LessonsCache(IHostingEnvironment hostingEnvironment)
        {
            var provider = new AllLessonProvider(hostingEnvironment);
            SetValue<LessonBase>(provider.CreateLessons());
        }

        public Dictionary<int, LessonBase> Get()
        {
            return GetValue<LessonBase>();
        }
    }
}