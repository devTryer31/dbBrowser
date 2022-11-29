using dbBrowser.Data.Model;
using dbBrowser.ViewModels.Base;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Data.Entity;
using System.Linq;

namespace dbBrowser.ViewModels
{
    public class DiagramsViewModel : BaseViewModel
    {
        private readonly UniversityDataBaseContainer _db;
        private PlotModel _piePlotModel;
        private PlotModel _barPlotModel;
        private PlotModel _barPlotModel2Ser;
        private PlotModel _resultPiePlotModel;

        public PlotModel PiePlotModel
        {
            get => _piePlotModel;
            set => Set(ref _piePlotModel, value);
        }

        public PlotModel BarPlotModel
        {
            get => _barPlotModel;
            set => Set(ref _barPlotModel, value);
        }

        public PlotModel BarPlotModel2Series
        {
            get => _barPlotModel2Ser;
            set => Set(ref _barPlotModel2Ser, value);
        }

        public PlotModel ResultPiePlotModel
        {
            get => _resultPiePlotModel;
            set => Set(ref _resultPiePlotModel, value);
        }

        public DiagramsViewModel(UniversityDataBaseContainer db)
        {
            _db = db;
            UpdateAllDiagrams();
        }

        public void UpdateAllDiagrams()
        {
            PiePlotModel = BuildPiePlot();
            BarPlotModel = BuildBarPlot1Series();
            BarPlotModel2Series = BuildBarPlot2Series();
            ResultPiePlotModel = GenerateResultPieModel();
        }

        private PlotModel GenerateResultPieModel()
        {
            var plotModel = new PlotModel
            {
                Title = "Распределение студентов по группам"
            };
            var seriesP1 = new PieSeries { StrokeThickness = 2.0, InsideLabelPosition = 0.8, AngleSpan = 360, StartAngle = 0 };

            var ages = _db.StudyGroups.Include(sg => sg.Students)
                .Select(sg => new { sg.Title, sg.Students.Count });

            seriesP1.Slices = ages.AsEnumerable().Select(g => new PieSlice(g.Title, g.Count)).ToList();

            plotModel.Series.Add(seriesP1);

            return plotModel;
        }

        private PlotModel BuildBarPlot2Series()
        {
            var plotModel = new PlotModel
            {
                Title = "Сравнение студентов с соц. категориями и без по группам",
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.BottomLeft,
                LegendOrientation = LegendOrientation.Vertical,
                LegendBorderThickness = 0
            };

            var withCategoryColumnSeries = new ColumnSeries { Title = "C соц. категорией", StrokeColor = OxyColors.BlanchedAlmond, StrokeThickness = 1, FillColor = OxyColors.Blue };
            var noCategoryColumnSeries = new ColumnSeries { Title = "Без соц. категории", StrokeColor = OxyColors.BlanchedAlmond, StrokeThickness = 1, FillColor = OxyColors.Bisque };
            var categoryAxis = new CategoryAxis { Position = AxisPosition.Bottom };
            var valueAxis = new LinearAxis { Position = AxisPosition.Left, MinimumPadding = 0, MaximumPadding = 0.05, AbsoluteMinimum = 0 };

            var bdInfo = _db.StudyGroups.Include(sg => sg.Students)
                .Select(sg => new
                {
                    sg.Title,
                    PrivilagedCount = sg.Students.Count(s => s.Privileges.Any()),
                    NoPrivilaged = sg.Students.Count(s => !s.Privileges.Any())
                });

            foreach (var info in bdInfo)
            {
                withCategoryColumnSeries.Items.Add(new ColumnItem(info.PrivilagedCount));
                noCategoryColumnSeries.Items.Add(new ColumnItem(info.NoPrivilaged));
                categoryAxis.Labels.Add(info.Title);
            }

            plotModel.Series.Add(withCategoryColumnSeries);
            plotModel.Series.Add(noCategoryColumnSeries);
            plotModel.Axes.Add(categoryAxis);
            plotModel.Axes.Add(valueAxis);

            return plotModel;
        }


        private PlotModel BuildBarPlot1Series()
        {
            var plotModel = new PlotModel
            {
                Title = "Количество групп на факультетах",
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.BottomCenter,
                LegendOrientation = LegendOrientation.Vertical,
                LegendBorderThickness = 0
            };

            var series = new ColumnSeries { Title = "Факультет", StrokeColor = OxyColors.Black, StrokeThickness = 1, FillColor = OxyColors.Blue };
            var categoryAxis = new CategoryAxis { Position = AxisPosition.Bottom };
            var valueAxis = new LinearAxis { Position = AxisPosition.Left, MinimumPadding = 0, MaximumPadding = 0.05, AbsoluteMinimum = 0 };

            var facInfo = _db.Faculties.Include(f => f.StudyGroups)
                .Select(f => new { f.Title, f.StudyGroups.Count });

            foreach (var info in facInfo)
            {
                series.Items.Add(new ColumnItem(info.Count));
                categoryAxis.Labels.Add(info.Title);
            }

            plotModel.Series.Add(series);
            plotModel.Axes.Add(categoryAxis);
            plotModel.Axes.Add(valueAxis);

            return plotModel;
        }

        private PlotModel BuildPiePlot()
        {
            var plotModel = new PlotModel { Title = "Сколько лет студентам" };
            var seriesP1 = new PieSeries { StrokeThickness = 2.0, InsideLabelPosition = 0.8, AngleSpan = 360, StartAngle = 0 };

            var ages = _db.Students.GroupBy(s => DbFunctions.DiffYears(s.Birthday, DateTime.Now));

            seriesP1.Slices = ages.AsEnumerable().Select(g => new PieSlice(g.Key.ToString(), g.Count())).ToList();

            plotModel.Series.Add(seriesP1);

            return plotModel;
        }
    }
}
