using DotVVM.Framework.ViewModel;
using DotvvmAcademy.CourseFormat;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.Web.Pages.Default
{
    public class DefaultViewModel : SiteViewModel
    {
        private readonly LessonRenderer lessonRenderer;
        private readonly CourseWorkspace workspace;

        public DefaultViewModel(CourseWorkspace workspace, LessonRenderer lessonRenderer)
        {
            this.workspace = workspace;
            this.lessonRenderer = lessonRenderer;
        }

        [Bind(Direction.ServerToClientFirstRequest)]
        public LessonDetail FirstLesson { get; set; }

        [Bind(Direction.ServerToClientFirstRequest)]
        public List<LessonDetail> Lessons { get; set; }

        public override async Task Load()
        {
            var variant = await workspace.LoadVariant(LanguageMoniker);
            var lessonTasks = variant.Lessons.Select(l => workspace.LoadLesson(LanguageMoniker, l));
            var lessons = await Task.WhenAll(lessonTasks);
            Lessons = lessons.Select(l =>
            {
                var rendered = lessonRenderer.Render(l);
                return new LessonDetail
                {
                    FirstStep = rendered.Source.Steps.FirstOrDefault(),
                    Html = rendered.Html,
                    ImageUrl = rendered.ImageUrl,
                    Moniker = rendered.Source.Moniker,
                    Name = rendered.Name
                };
            })
            .ToList();
            FirstLesson = Lessons.FirstOrDefault();
            await base.Load();
        }

        protected override async Task<IEnumerable<string>> GetAvailableLanguageMonikers()
        {
            var root = await workspace.LoadRoot();
            return root.Variants;
        }
    }
}