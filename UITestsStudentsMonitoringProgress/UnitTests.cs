using Avalonia.Controls;
using Avalonia.Threading;
using Avalonia.VisualTree;
using StudentsMonitoringProgress;

namespace UITestsStudentsMonitoringProgress;

public class UnitTests
{
    public UnitTests()
    {
        var app = new App();
        app.Initialize();
    }
    [Fact]
    public async Task TestListBoxBinding()
    {
        var window = AvaloniaApp.GetMainWindow();
        var list = window.GetVisualDescendants().OfType<ListBox>().First();
        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            window.Show();
            list.SelectedIndex = 0;
        });

        await Task.Delay(200);

        Assert.NotNull(list);
        Assert.Equal(0, list.SelectedIndex);
        Assert.NotEmpty(list.Items);
    }
    
    [Fact]
    public async Task TestGridBinding()
    {
        // Arrange
        var window = AvaloniaApp.GetMainWindow();
        var grid = window.GetVisualDescendants().OfType<Grid>().First(x => (x.Name != null) && x.Name.Equals("SrGrid"));

        // Act
        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            window.Show();
        });

        await Task.Delay(200);

        // Assert
        Assert.NotNull(grid);
        Assert.NotEmpty(grid.ColumnDefinitions);
        Assert.Equal(9, grid.ColumnDefinitions.Count);
    }
}