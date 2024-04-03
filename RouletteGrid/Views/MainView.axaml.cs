using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
        var grid = this.FindControl<Grid>("RouletteGrid");

        for (int i = 0; i < 3; i++)
            grid.RowDefinitions.Add(new RowDefinition(new GridLength(BASE_WIDTH + 5)));


        for (int i = 0; i < 13; i++)
            grid.ColumnDefinitions.Add(new ColumnDefinition(new GridLength(BASE_WIDTH + 5)));

        grid.RowDefinitions.Add(new RowDefinition(new GridLength(BASE_WIDTH + 10)));
        grid.RowDefinitions.Add(new RowDefinition(new GridLength(BASE_WIDTH + 10)));

        var canvasOfNr0 = new Canvas();
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
            FontSize = 24
        };

        Canvas.SetTop(rectOfNr0, 0);
        Canvas.SetLeft(rectOfNr0, 0);
        Canvas.SetTop(textBlockOfNr0, ((BASE_WIDTH + 5) * 3) / 4);
        Canvas.SetLeft(textBlockOfNr0, BASE_WIDTH / 4);

        canvasOfNr0.Children.Add(rectOfNr0);
        canvasOfNr0.Children.Add(textBlockOfNr0);

        Grid.SetColumn(canvasOfNr0, 0);
        Grid.SetRow(canvasOfNr0, 0);

        grid.Children.Add(canvasOfNr0);


        // Generate numbers from 1 to 36 on board grid
        int count = 1;
        for (int row = 0; row < 3; row++)
        {
            int beginNumber = 3 - row;
            for (int col = 1; col < 13; col++)
            {
                var canvas = new Canvas();

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
                    FontSize = 24
                };

                Canvas.SetTop(rect, 0);
                Canvas.SetLeft(rect, 0);
                Canvas.SetTop(textBlock, BASE_WIDTH / 4);
                Canvas.SetLeft(textBlock, BASE_WIDTH / 4);

                canvas.Children.Add(rect);
                canvas.Children.Add(textBlock);

                Grid.SetRow(canvas, row);
                Grid.SetColumn(canvas, col);

                grid.Children.Add(canvas);

                count++;
                beginNumber += 3;
            }
        }

        for (int i = 0; i < 3; i++)
        {
            string textStr = "1st12";
            if (i == 1)
                textStr = "2nd12";
            else if (i == 2)
                textStr = "3rd12";

            var canvas = new Canvas();
            var rect = new Rectangle
            {
                Width = BASE_WIDTH * 4 + 16,
                Height = (BASE_WIDTH),
                Fill = Brushes.Black,
                Stroke = Brushes.White,
                StrokeThickness = 0.5
            };

            var text = new TextBlock
            {
                Text = textStr,
                Foreground = Brushes.White,
                FontSize = 24
            };

            Canvas.SetTop(rect, 0);
            Canvas.SetLeft(rect, 0);
            Canvas.SetTop(text, BASE_WIDTH / 5);
            Canvas.SetLeft(text, BASE_WIDTH * 1.6);

            canvas.Children.Add(rect);
            canvas.Children.Add(text);

            Grid.SetColumn(canvas, i * 4 + 1);
            Grid.SetRow(canvas, 3);

            grid.Children.Add(canvas);
        }

        for (int i = 0; i < 6; i++)
        {
            string textStr = string.Empty;
            if (i == 0)
                textStr = "1 to 18";
            else if (i == 1)
                textStr = "even";
            else if (i == 2)
                textStr = "";
            else if (i == 3)
                textStr = "";
            else if (i == 4)
                textStr = "odd";
            else if (i == 5)
                textStr = "19 to 36";

            var canvas = new Canvas();
            var rect = new Rectangle
            {
                Width = BASE_WIDTH * 2 + 6,
                Height = (BASE_WIDTH),
                Fill = i == 3 ? Brushes.Red : Brushes.Black,
                Stroke = Brushes.White,
                StrokeThickness = 0.5
            };


           

            Canvas.SetTop(rect, 0);
            Canvas.SetLeft(rect, 0);
            canvas.Children.Add(rect);

            if (textStr != string.Empty)
            {
                var text = new TextBlock
                {
                    Text = textStr,
                    Foreground = Brushes.White,
                    FontSize = 24
                };
                Canvas.SetTop(text, BASE_WIDTH / 5);
                Canvas.SetLeft(text, BASE_WIDTH / 3.2);
                canvas.Children.Add(text);
            }

            Grid.SetColumn(canvas, i * 2 + 1);
            Grid.SetRow(canvas, 4);

            grid.Children.Add(canvas);
        }
    }
}
