using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using System;
using System.Linq;

namespace RouletteGrid.Views;

public partial class MainView : UserControl
{
    private const int BASE_WIDTH = 55;
    private const int BASE_HEIGHT = 55;
    private int[] redNumbers = { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 };
    public MainView()
    {
        InitializeComponent();
        GenerateRouletteGrid();
    }
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void GenerateRouletteGrid()
    {
        //Define rows and columns
        var grid = this.FindControl<Grid>("RouletteGrid");
        var firstDataGrid = this.FindControl<Grid>("FirstDataGrid");
        var secondDataGrid = this.FindControl<Grid>("SecondDataGrid");

        for (int i = 0; i < 3; i++)
        {
            grid.RowDefinitions.Add(new RowDefinition(new GridLength(BASE_WIDTH + 5)));
        }

        for (int i = 0; i < 14; i++)
        {
            grid.ColumnDefinitions.Add(new ColumnDefinition(new GridLength(BASE_WIDTH + 5)));
        }


        firstDataGrid.RowDefinitions.Add(new RowDefinition(new GridLength(BASE_WIDTH + 5)));
        for(int i = 0; i < 3; i++)
        {
            firstDataGrid.ColumnDefinitions.Add(new ColumnDefinition(new GridLength((BASE_WIDTH + 5) * 4)));
        }

        secondDataGrid.RowDefinitions.Add(new RowDefinition(new GridLength(BASE_WIDTH + 5)));
        for (int i = 0; i < 6; i++)
        {
            secondDataGrid.ColumnDefinitions.Add(new ColumnDefinition(new GridLength((BASE_WIDTH + 5) * 2)));
        }

        // Generate numbers from 0 to 36 on board grid
        GenerateGridNumbers(grid);

        //Generate other data showing grids
        GenerateFirstDataGrid(firstDataGrid);
        GenerateSecondDataGrid(secondDataGrid);
    }

    private void GenerateSecondDataGrid(Grid grid)
    {
        for (int i = 0; i < 6; i++)
        {
            string textStr = string.Empty;
            if (i == 0)
                textStr = "1 to 18";
            else if (i == 1)
                textStr = "Even";
            else if (i == 2)
                textStr = "";
            else if (i == 3)
                textStr = "";
            else if (i == 4)
                textStr = "Odd";
            else if (i == 5)
                textStr = "19 to 36";

            var rect = new Rectangle
            {
                Width = BASE_WIDTH * 2 + 5,
                Height = (BASE_WIDTH),
                Fill = i == 3 ? Brushes.Red : Brushes.Black,
                Stroke = Brushes.White,
                StrokeThickness = 0.5
            };

            Grid.SetColumn(rect, i);
            Grid.SetRow(rect, 0);
            grid.Children.Add(rect);

            if (textStr != string.Empty)
            {
                var text = new TextBlock
                {
                    Text = textStr,
                    Foreground = Brushes.White,
                    FontSize = 24,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Grid.SetColumn(text, i);
                Grid.SetRow(text, 0);
                grid.Children.Add(text);
            }
        }
    }

    private void GenerateFirstDataGrid(Grid grid)
    {
        for (int i = 0; i < 3; i++)
        {
            string textStr = "1st12";
            if (i == 1)
                textStr = "2nd12";
            else if (i == 2)
                textStr = "3rd12";

            var rect = new Rectangle
            {
                Width = (BASE_WIDTH + 5) * 4,
                Height = (BASE_WIDTH),
                Fill = Brushes.Black,
                Stroke = Brushes.White,
                StrokeThickness = 0.5
            };

            var text = new TextBlock
            {
                Text = textStr,
                Foreground = Brushes.White,
                FontSize = 24,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            Grid.SetColumn(rect, i);
            Grid.SetRow(rect, 0);
            Grid.SetColumn(text, i);
            Grid.SetRow(text, 0);

            grid.Children.Add(rect);
            grid.Children.Add(text);
        }
    }

    private void GenerateGridNumbers(Grid grid)
    {
        var rectOfNr0 = new Rectangle
        {
            Width = BASE_WIDTH,
            Height = (BASE_WIDTH + 5) * 3,
            Fill = Brushes.Green,
            Stroke = Brushes.Green,
            StrokeThickness = 1
        };

        var textBlockOfNr0 = new TextBlock
        {
            Text = "0",
            Foreground = Brushes.White,
            FontSize = 24,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };

        Grid.SetColumn(rectOfNr0, 0);
        Grid.SetRow(rectOfNr0, 1);
        Grid.SetColumn(textBlockOfNr0, 0);
        Grid.SetRow(textBlockOfNr0, 1);

        grid.Children.Add(rectOfNr0);
        grid.Children.Add(textBlockOfNr0);

        int count = 1;
        for (int row = 0; row < 3; row++)
        {
            int beginNumber = 3 - row;
            for (int col = 1; col < 13; col++)
            {
                var rect = new Rectangle
                {
                    Width = BASE_WIDTH,
                    Height = BASE_HEIGHT,
                    MaxHeight = BASE_HEIGHT,
                    MaxWidth = BASE_WIDTH,
                    Fill = redNumbers.Contains(beginNumber) ? Brushes.Red : Brushes.Black,
                    Stroke = Brushes.Green,
                    StrokeThickness = 1
                };

                var textBlock = new TextBlock
                {
                    Text = beginNumber.ToString(),
                    Foreground = redNumbers.Contains(beginNumber) ? Brushes.Black : Brushes.White,
                    FontSize = 24, 
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                };

                Grid.SetRow(rect, row);
                Grid.SetColumn(rect, col);
                Grid.SetRow(textBlock, row);
                Grid.SetColumn(textBlock, col);

                grid.Children.Add(rect);
                grid.Children.Add(textBlock);

                count++;
                beginNumber += 3;
            }
        }

        for (int i = 0; i < 3; i++)
        {
            var rect = new Rectangle
            {
                Width = BASE_WIDTH,
                Height = (BASE_WIDTH),
                Fill = Brushes.Black,
                Stroke = Brushes.White,
                StrokeThickness = 0.5
            };

            var text = new TextBlock
            {
                Text = "2to1",
                Foreground = Brushes.White,
                FontSize = 24,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            var rotateTransform = new RotateTransform { Angle = 90 };
            text.RenderTransform = rotateTransform;

            Grid.SetColumn(rect, 13);
            Grid.SetRow(rect, i);
            Grid.SetColumn(text, 13);
            Grid.SetRow(text, i);

            grid.Children.Add(rect);
            grid.Children.Add(text);
        }
    }
}
