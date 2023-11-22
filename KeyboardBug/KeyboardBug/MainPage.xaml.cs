using CommunityToolkit.Mvvm.ComponentModel;

namespace KeyboardBug;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
    private PickerItem selecteItem;
    public PickerItem SelecteItem
    {
        get { return selecteItem; }
        set { SetProperty(ref selecteItem, value); }
    }
}

public class PickerItem  : ObservableObject
{
    public int ItemValue { get; set; }
	private string itemTitle; 
    public string ItemTitle
    {
        get { return itemTitle; }
        set { SetProperty(ref itemTitle, value); }
    }
    public bool IsDefaultValue { get; set; }
}


