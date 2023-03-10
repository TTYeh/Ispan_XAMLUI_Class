using Ispan_XAMLUI_MauiApp.Models;
namespace Ispan_XAMLUI_MauiApp.Views;

public partial class CustomerSystem : ContentPage
{
	List<CCustomer> Mycustomers = new List<CCustomer>();
    private int currentId { get; set; }

    private CustomerViewModels vm = new CustomerViewModels();
    public CustomerSystem()
	{
        
		InitializeComponent();
        this.currentId = 0;
        this.Loaded += CustomerSystem_Loaded;
    }

    private void CustomerSystem_Loaded(object sender, EventArgs e)
    {
        // 清除快取資料
        claerCache();
        // 載入範例資料
        #region data
        Mycustomers.Add(new CCustomer()
        {
            id = 1,
            name = "John",
            phone = "12345678",
            address = "Taipei",
            email = "ass@bss.com"
        });
        Mycustomers.Add(new CCustomer()
        {
            id = 2,
            name = "Mike",
            phone = "246810",
            address = "Park",
            email = "fff@bss.com"
        });
        Mycustomers.Add(new CCustomer()
        {
            id = 3,
            name = "Japan",
            phone = "1357911",
            address = "Ispan",
            email = "ddd@bss.com"
        });
        #endregion
    }

    private void btnFirst_Clicked(object sender, EventArgs e)
	{
        // Get First Data
        currentId = 0;
        Display();
    }

	private void btnPrevious_Clicked(object sender, EventArgs e)
	{
        if (currentId - 1 >= 0) currentId -= 1;
        Display();
    }

	private void btnNext_Clicked(object sender, EventArgs e)
	{
        if (currentId + 1 < Mycustomers.Count - 1) currentId += 1;
        Display();
    }

	private void btnLast_Clicked(object sender, EventArgs e)
	{
        currentId = Mycustomers.Count - 1;
        Display();
    }
    public void Display()
    {
        if (currentId < 0) return;
        txtId.Text = Mycustomers[currentId].id.ToString();
        txtName.Text = Mycustomers[currentId].name;
        txtAddress.Text = Mycustomers[currentId].address;
        txtEmail.Text = Mycustomers[currentId].email;
        txtPhone.Text = Mycustomers[currentId].phone;
    }

	private void btnQuery_Clicked(object sender, EventArgs e)
	{
        App app = Application.Current as App;
        claerCache();
        Navigation.PushAsync(new CustomerQuery());
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        App app = Application.Current as App;
        if (!string.IsNullOrEmpty(app.Queryword))
        {
            findCustomer(app.Queryword);
        }
        else if (app.selectIndex >= 0)
        {
            currentId = app.selectIndex;
            Display();
        }
    }

    private void findCustomer(string keyword)
    {
        //var result = Mycustomers.Where(x => x.name.Contains(keyword) ||
        //x.address.Contains(keyword) ||
        //x.email.Contains(keyword) ||
        //x.phone.Contains(keyword)).FirstOrDefault();
        //currentId = result.id;
        //Display();
        // 用linq產生
        
        for (int i = 0; i < Mycustomers.Count; i++)
        {
            if (Mycustomers[i].name.Contains(keyword)||
                Mycustomers[i].address.Contains(keyword) ||
                Mycustomers[i].email.Contains(keyword) ||
                Mycustomers[i].phone.Contains(keyword))
            {
                currentId = i;
                Display();
                break;
            }
        }

    }

    private void btnList_Clicked(object sender, EventArgs e)
	{
        App app = Application.Current as App;
        claerCache();
        app.customerList = Mycustomers;
        Navigation.PushAsync(new CustomerList());
    }
    private static void claerCache()
    {
        App app = Application.Current as App;
        app.Queryword = "";
        app.selectIndex = -1;
    }
}