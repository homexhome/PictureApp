namespace PictureApp.CustomControls;

public class LoginEntry : Entry
{
    public static BindableProperty CornerRadiusProperty =
        BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(LoginEntry), 0);

    public static BindableProperty BorderThicknessProperty =
        BindableProperty.Create(nameof(BorderThickness), typeof(int), typeof(LoginEntry), 0);

    public static BindableProperty PaddingProperty =
        BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(LoginEntry), new Thickness());

    public static BindableProperty BorderColorProperty =
        BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(LoginEntry), Colors.Transparent);

    public static BindableProperty CustomHeightProperty =
        BindableProperty.Create(nameof(CustomHeight), typeof(int), typeof(LoginEntry), 0);

    public int CornerRadius
    {
        get => (int)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    public int BorderThickness
    {
        get => (int)GetValue(BorderThicknessProperty);
        set => SetValue(BorderThicknessProperty, value);
    }

    public Color BorderColor
    {
        get => (Color)GetValue(BorderColorProperty);
        set => SetValue(BorderColorProperty, value);
    }

    public Thickness Padding
    {
        get => (Thickness)GetValue(PaddingProperty);
        set => SetValue(PaddingProperty, value);
    }

    public int CustomHeight
    {
        get => (int)GetValue(CustomHeightProperty);
        set => SetValue(CustomHeightProperty, value);
    }
}
