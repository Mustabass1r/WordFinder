using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class CustomPanelButton : Panel
{
    private Image panelImage;
    private string labelText;

    private bool isMouseOver;

    private string panelImagePath;
    private Font labelFont = SystemFonts.DefaultFont; // Default font
    private Color labelTextColor = Color.White; // Default text color
    private float labelFontSize = 11f; // Default font size
    private Color panelColor = Color.FromArgb(0xDE, 0x09, 0x00); // Default panel color (DE0900)
    private Color borderColor = Color.Black; // Default border color

    public string PanelImagePath
    {
        get { return panelImagePath; }
        set
        {
            panelImagePath = value;
            LoadImage();
            Invalidate(); // Redraw the panel when the image is set
        }
    }

    public string LabelText
    {
        get { return labelText; }
        set
        {
            labelText = value;
            Invalidate(); // Redraw the panel when the label text is set
        }
    }

    public Font LabelFont
    {
        get { return labelFont; }
        set
        {
            labelFont = value;
            Invalidate(); // Redraw the panel when the font is set
        }
    }

    public Color LabelTextColor
    {
        get { return labelTextColor; }
        set
        {
            labelTextColor = value;
            Invalidate(); // Redraw the panel when the text color is set
        }
    }

    public float LabelFontSize
    {
        get { return labelFontSize; }
        set
        {
            labelFontSize = value;
            // Update the font with the new size
            labelFont = new Font(labelFont.FontFamily, labelFontSize, labelFont.Style);
            Invalidate(); // Redraw the panel when the font size is set
        }
    }

    public Color PanelColor
    {
        get { return panelColor; }
        set
        {
            panelColor = value;
            Invalidate(); // Redraw the panel when the panel color is set
        }
    }

    public Color BorderColor
    {
        get { return borderColor; }
        set
        {
            borderColor = value;
            Invalidate(); // Redraw the panel when the border color is set
        }
    }

    public CustomPanelButton(string imagePath, string labelText)
    {
        SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);

        // Set the image path and label text when creating the instance
        PanelImagePath = imagePath;
        LabelText = labelText;

        // Attach event handlers
        MouseEnter += CustomPanelButton_MouseEnter;
        MouseLeave += CustomPanelButton_MouseLeave;
        MouseClick += CustomPanelButton_MouseClick;
    }

    private void CustomPanelButton_MouseEnter(object sender, EventArgs e)
    {
        isMouseOver = true;
        ResizePanel();
        Invalidate(); // Redraw the panel when the mouse enters
    }

    private void CustomPanelButton_MouseLeave(object sender, EventArgs e)
    {
        isMouseOver = false;
        ResizePanel();
        Invalidate(); // Redraw the panel when the mouse leaves
    }

    private void CustomPanelButton_MouseClick(object sender, MouseEventArgs e)
    {
        // Add your click event handling code here
        // For example, you can raise a Click event or perform some action
    }

    private void ResizePanel()
    {
        // Adjust the size based on the hover state
        if (isMouseOver)
        {
            Width = (int)(Width * 1.02);
            Height = (int)(Height * 1.02);
        }
        else
        {
            Width = (int)(Width / 1.02);
            Height = (int)(Height / 1.02);
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        // Create a rounded rectangle path
        using (GraphicsPath roundedRectanglePath = CreateRoundedRectanglePath(new Rectangle(0, 0, Width - 1, Height - 1), 10)) // Adjust the corner radius as needed
        {
            // Set the background color within the rounded rectangle
            using (Region backgroundRegion = new Region(roundedRectanglePath))
            using (Brush backgroundBrush = new SolidBrush(panelColor))
            {
                e.Graphics.FillRegion(backgroundBrush, backgroundRegion);
            }

            if (panelImage != null)
            {
                // Adjust the size of the image
                int imageSize = Math.Min(Width - 25, Height - 45);
                int imageX = ((Width - imageSize) / 2);
                int imageY = 5;

                // Draw the image
                e.Graphics.DrawImage(panelImage, new Rectangle(imageX, imageY, imageSize, imageSize));

                // Draw the rounded rectangle border
                using (Pen borderPen = new Pen(borderColor, 2)) // Adjust the pen width as needed
                {
                    e.Graphics.DrawPath(borderPen, roundedRectanglePath);
                }
            }

            // Draw the text label below the image
            using (StringFormat sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;

                using (Font font = new Font(labelFont.FontFamily, labelFontSize, labelFont.Style))
                {
                    TextRenderer.DrawText(e.Graphics, labelText, font, new Rectangle(5, Height - 25, Width - 10, 20), labelTextColor, TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
                }
            }
        }
    }

    private GraphicsPath CreateRoundedRectanglePath(Rectangle rectangle, int cornerRadius)
    {
        GraphicsPath path = new GraphicsPath();

        // Top-left corner
        path.AddArc(rectangle.Left, rectangle.Top, cornerRadius * 2, cornerRadius * 2, 180, 90);

        // Top-right corner
        path.AddArc(rectangle.Right - cornerRadius * 2, rectangle.Top, cornerRadius * 2, cornerRadius * 2, 270, 90);

        // Bottom-right corner
        path.AddArc(rectangle.Right - cornerRadius * 2, rectangle.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);

        // Bottom-left corner
        path.AddArc(rectangle.Left, rectangle.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);

        path.CloseFigure();

        return path;
    }


    private void LoadImage()
    {
        if (!string.IsNullOrEmpty(panelImagePath))
        {
            try
            {
                // Attempt to load the image from the file path
                panelImage = Image.FromFile(panelImagePath);
            }
            catch (Exception ex)
            {
                // Handle any potential exceptions (e.g., file not found, invalid image format)
                Console.WriteLine($"Error loading image: {ex.Message}");
                panelImage = null;
            }
        }
        else
        {
            panelImage = null;
        }
    }
    public event EventHandler ButtonClicked;

    // Add a property to specify the target form to open
    public Form TargetForm { get; set; }

    // ... (Your existing code)

    protected override void OnClick(EventArgs e)
    {
        base.OnClick(e);

        // Raise the ButtonClicked event
        ButtonClicked?.Invoke(this, EventArgs.Empty);

        // Close the current form
        Form currentForm = FindForm();
        currentForm?.Hide();

        // Open the specified form
        if (TargetForm != null)
        {
            TargetForm.Show();
        }
    }

}
