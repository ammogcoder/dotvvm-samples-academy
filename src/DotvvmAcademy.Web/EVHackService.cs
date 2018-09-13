﻿using DotVVM.Framework.Compilation;
using DotVVM.Framework.Controls.Infrastructure;
using DotvvmAcademy.CourseFormat;
using DotvvmAcademy.Meta;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;

namespace DotvvmAcademy.Web
{
    public class EVHackService
    {
        public async Task<DotvvmView> BuildView(EVPresenter.PresenterContext context)
        {
            var step = await GetStep(context);
            var viewModelCompilation = await GetViewModelCompilation(context, step);
            var viewModelAssemblyPath = EmitToTemp(context, viewModelCompilation);
            var viewModelAssembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(viewModelAssemblyPath);
            var viewCompilation = await GetViewCompilation(context, step, viewModelAssembly, viewModelCompilation);
            var viewAssembly = EmitToMemory(viewCompilation);
            var className = $"{Path.GetFileNameWithoutExtension(step.EmbeddedView.Path)}EVControlBuilder";
            var builder = (IControlBuilder)viewAssembly.CreateInstance($"DotvvmAcademy.Course.{className}");
            var controlFactory = context.DotvvmContext.Services.GetRequiredService<IControlBuilderFactory>();
            return (DotvvmView)builder.BuildControl(controlFactory, context.DotvvmContext.Services);
        }

        private Assembly EmitToMemory(CSharpCompilation compilation)
        {
            using (var stream = new MemoryStream())
            {
                var result = compilation.Emit(stream);
                if (!result.Success)
                {
                    throw new NotSupportedException("Could not compile ViewModel assembly.");
                }
                stream.Position = 0;
                return AssemblyLoadContext.Default.LoadFromStream(stream);
            }
        }

        private string EmitToTemp(EVPresenter.PresenterContext context, CSharpCompilation compilation)
        {
            var path = $"{Path.GetTempPath()}/{compilation.AssemblyName}.dll";
            var result = compilation.Emit(path);
            if (!result.Success)
            {
                throw new NotSupportedException("Coult not emit compilation to disk.");
            }

            return path;
        }

        private (string languageMoniker, string lessonMoniker, string stepMoniker) GetParameters(EVPresenter.PresenterContext context)
        {
            if (!context.DotvvmContext.Parameters.TryGetValue("Language", out var languageMoniker)
                || !context.DotvvmContext.Parameters.TryGetValue("Lesson", out var lessonMoniker)
                || !context.DotvvmContext.Parameters.TryGetValue("Step", out var stepMoniker))
            {
                throw new NotSupportedException("EVPresenter cannot be used with this route.");
            }

            return ((string)languageMoniker, (string)lessonMoniker, (string)stepMoniker);
        }

        private async Task<Step> GetStep(EVPresenter.PresenterContext context)
        {
            (var languageMoniker, var lessonMoniker, var stepMoniker) = GetParameters(context);
            var workspace = context.DotvvmContext.Services.GetRequiredService<CourseWorkspace>();
            var step = await workspace.LoadStep(languageMoniker, lessonMoniker, stepMoniker);
            if (step.EmbeddedView == null)
            {
                throw new NotSupportedException("Step does not have EmbeddedView.");
            }
            return step;
        }

        private async Task<CSharpCompilation> GetViewCompilation(
            EVPresenter.PresenterContext context, 
            Step step, 
            Assembly viewModelAssembly, 
            CSharpCompilation viewModelCompilation)
        {
            var environment = context.DotvvmContext.Services.GetRequiredService<ICourseEnvironment>();
            var viewSource = await environment.Read(step.EmbeddedView.Path);
            var builder = context.DotvvmContext.Services.GetRequiredService<EVResolvedTreeBuilder>();
            builder.AdditionalAssembly = viewModelAssembly;
            var viewCompiler = context.DotvvmContext.Services.GetRequiredService<EVViewCompiler>();
            viewCompiler.AdditionalReference = viewModelCompilation.ToMetadataReference();
            var className = $"{Path.GetFileNameWithoutExtension(step.EmbeddedView.Path)}EVControlBuilder";
            var viewCompilation = viewCompiler.CreateCompilation($"EVView_{Guid.NewGuid()}");
            (var descriptor, var factory) = viewCompiler.CompileView(
                sourceCode: viewSource,
                fileName: step.EmbeddedView.Path,
                compilation: viewCompilation,
                namespaceName: "DotvvmAcademy.Course",
                className: className);
            viewCompilation = factory();
            return viewCompilation;
        }

        private async Task<CSharpCompilation> GetViewModelCompilation(EVPresenter.PresenterContext context, Step step)
        {
            var environment = context.DotvvmContext.Services.GetRequiredService<ICourseEnvironment>();
            var dependencies = await Task.WhenAll(step.EmbeddedView.Dependencies.Select(async d => await environment.Read(d)));
            var id = Guid.NewGuid();
            var viewModelCompilation = CSharpCompilation.Create(
                assemblyName: $"EVViewModel_{id}",
                syntaxTrees: dependencies.Select(d => CSharpSyntaxTree.ParseText(d)),
                references: new[]
                {
                        RoslynReference.FromName("mscorlib"),
                        RoslynReference.FromName("netstandard"),
                        RoslynReference.FromName("System.Private.CoreLib"),
                        RoslynReference.FromName("System.Runtime"),
                        RoslynReference.FromName("System.ValueTuple"),
                        RoslynReference.FromName("System.Collections"),
                        RoslynReference.FromName("System.Reflection"),
                        RoslynReference.FromName("System.Linq"),
                        RoslynReference.FromName("System.Linq.Expressions"),
                        RoslynReference.FromName("System.ComponentModel.Annotations"),
                        RoslynReference.FromName("System.ComponentModel.DataAnnotations"),
                        RoslynReference.FromName("DotVVM.Framework"),
                        RoslynReference.FromName("DotVVM.Core"),
                },
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
            return viewModelCompilation;
        }
    }
}